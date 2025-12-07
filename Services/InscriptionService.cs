using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GerenciadorEventos.Domain.Models.DTOs;
using GerenciadorEventos.Domain.Services;
using GerenciadorEventos.Interfaces.IRepository;
using GerenciadorEventos.Interfaces.IService;
using GerenciadorEventos.Models;

namespace GerenciadorEventos.Services
{
    public class InscriptionService : IInscriptionService
    {

        private readonly IInscriptionRepository _inscriptionRepository;
        private readonly IUserService _userService;
        private readonly IEventService _eventService;
        private readonly IPaymentService _paymentService;

        public InscriptionService(IInscriptionRepository inscriptionRepository, IUserService userService, IEventService eventService, IPaymentService paymentService)
        {
            _inscriptionRepository = inscriptionRepository;
            _userService = userService;
            _eventService = eventService;
            _paymentService = paymentService;
        }

        public async Task<Inscription> AddInscription(InscriptionDTO inscriptionDTO)
        {
            if (inscriptionDTO is null)
                throw new ArgumentNullException("Inscription Dto is null");

            await ForeignKeyValidation(inscriptionDTO);

            await EventHasAvaiableVacancies(inscriptionDTO.EventId);

            var inscription = new Inscription
            {
                Status = inscriptionDTO.Status,
                InscriptionDate = inscriptionDTO.InscriptionDate,
                UserId = inscriptionDTO.UserId,
                EventId = inscriptionDTO.EventId,
                PaymentId = inscriptionDTO.PaymentId
            };

            var inscriptionCreated = await _inscriptionRepository.Create(inscription);

            //TODO LOGICA PARA REDUZIR A QUANTIDADE DE VAGAS DISPONIVEIS
            //TODO LOGICA PARA PERMITIR A INSCRIÇÃO APENAS SE O EVENTO TIVER VAGAS DISPONIVEIS
            //TODO LOGICA PARA PERMITIR A INSCRIÇÃO APENAS QUANDO O PAGAMENTO TIVER O STATUS DE COMPLETO
            

            return inscriptionCreated;
            
        }

        public async Task<IEnumerable<Inscription>> GetAllInscriptions()
        {
            return await _inscriptionRepository.GetAllInscriptions();
        }

        public async Task<Inscription?> GetById(int inscriptionId)
        {
            return await _inscriptionRepository.GetById(inscriptionId);
        }

        public async Task<Inscription> RemoveInscription(int id)
        {
            var inscription = await _inscriptionRepository.GetById(id);

            if (inscription is null)
                throw new KeyNotFoundException("Could not find Inscription Related to that Id");

            var inscriptionDeleted = await _inscriptionRepository.Remove(inscription);

            return inscriptionDeleted;
        }

        public async Task<Inscription> UpdateInscription(int id, InscriptionDTO inscriptionDTO)
        {
            var inscriptionToUpdate = await _inscriptionRepository.GetById(id);

            if (inscriptionToUpdate is null)
                throw new KeyNotFoundException("Could not find Inscription Related to that Id");

            if (inscriptionDTO is null)
                throw new ArgumentNullException("InscriptionDto Received is null");

            await ForeignKeyValidation(inscriptionDTO);

            var inscription = new Inscription
            {
                Status = inscriptionDTO.Status,
                InscriptionDate = inscriptionDTO.InscriptionDate,
                UserId = inscriptionDTO.UserId,
                EventId = inscriptionDTO.EventId,
                PaymentId = inscriptionDTO.PaymentId
            };

            var updatedInscription = await _inscriptionRepository.Update(inscriptionToUpdate, inscription);

            return updatedInscription;
        }

        public async Task ForeignKeyValidation(InscriptionDTO inscriptionDTO)
        {
            if (! await _userService.UserExists(inscriptionDTO.UserId.ToString()))
                throw new KeyNotFoundException("There is no user relataded to that UserId");

            if (!await _eventService.EventExists(inscriptionDTO.EventId))
                throw new KeyNotFoundException("There is no event related to that EventId");

            if (!await _paymentService.PaymentExists(inscriptionDTO.PaymentId))
                throw new KeyNotFoundException("There is payment related to that PaymentId");
        }

        private async Task EventHasAvaiableVacancies(int id)
        {
            var @event = await _eventService.GetById(id);

            if (@event is null)
                throw new KeyNotFoundException("Could not find event related to that Id");

            if (@event.AvaiableVacancies == 0)
                throw new ArgumentException("There is no Avaiable Vacancies for that event!");

        }

    }
}