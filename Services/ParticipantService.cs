using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;
using GerenciadorEventos.Domain.Models.DTOs;
using GerenciadorEventos.Domain.Models.Entities;
using GerenciadorEventos.Interfaces.IRepository;
using GerenciadorEventos.Interfaces.IService;

namespace GerenciadorEventos.Services
{
    public class ParticipantService : IParticipantService
    {
        private readonly IParticipantRepository _partipantRepository;

        public ParticipantService(IParticipantRepository participantRepository)
        {
            _partipantRepository = participantRepository;
        }

        public async Task<Participant> AddParticipant(ParticipantDto participantDto)
        {
            if (participantDto is null)
                throw new ArgumentNullException(nameof(participantDto), "Trying to add a Participant with null value");

            if (await _partipantRepository.ParticipantExists(participantDto.Email!))
                throw new DuplicateNameException("There is alerady a participant with this email in the database");

            var newParticipant = new Participant
            {
                Email = participantDto.Email,
                Name = participantDto.Name
            };

            var participant = await _partipantRepository.Create(newParticipant);

            return participant;
        }

        public async Task<Participant> Delete(int id)
        {
            var participantToDelete = await _partipantRepository.GetById(id);

            if (participantToDelete is null)
                throw new ArgumentNullException(nameof(participantToDelete), "Trying to Delete a Participant with null value");

            if (! await _partipantRepository.ParticipantExists(id))
                throw new KeyNotFoundException("There is no participant with this email in the database");

            var participant = await _partipantRepository.Delete(participantToDelete);

            return participant;
        }

        public async Task<Participant> Update(int id, ParticipantDto participantDto)
        {
            var participantToUpdate = await _partipantRepository.GetById(id);

            if(participantToUpdate is null)
                throw new KeyNotFoundException("There is no participant with this email in the database");

            if (participantDto is null)
                throw new ArgumentException(nameof(participantDto), "Trying to update a participant with null values");

            //Verifica se o Email que queremos atualizar, já existe no database
            if (participantDto.Email != participantToUpdate.Email && await _partipantRepository.ParticipantExists(participantDto.Email!))
                throw new DuplicateNameException("There is already a Participant with this email in the database");

            var newParticipant = new Participant
            {
                Email = participantDto.Email,
                Name = participantDto.Name
            };

            var participant = await _partipantRepository.Update(participantToUpdate, newParticipant);

            return participant;
  
        }

        public async Task<Participant?> GetParticipant(int id)
        {
            return await _partipantRepository.GetById(id);
        }

        public async Task<IEnumerable<Participant>> GetAllParticipants()
        {
            return await _partipantRepository.GetAllParticipants();
        }

        
    }
}