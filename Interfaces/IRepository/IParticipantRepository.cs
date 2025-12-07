using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Domain.Models.DTOs;
using GerenciadorEventos.Domain.Models.Entities;

namespace GerenciadorEventos.Interfaces.IRepository
{
    public interface IParticipantRepository
    {
        Task<Participant> Create(Participant participant);
        Task<Participant> Delete(Participant participant);
        Task<Participant> Update(Participant participantToUpdate, Participant newParticipant);
        Task<Participant?> GetById(int id);
        Task<IEnumerable<Participant>> GetAllParticipants();
        Task<bool> ParticipantExists(int id);
        Task<bool> ParticipantExists(string email);
    }
}