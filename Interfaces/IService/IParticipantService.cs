using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Domain.Models.DTOs;
using GerenciadorEventos.Domain.Models.Entities;

namespace GerenciadorEventos.Interfaces.IService
{
    public interface IParticipantService
    {
        Task<Participant> AddParticipant(ParticipantDto participantDto);
        Task<Participant> Delete(int id);
        Task<Participant> Update(int id,ParticipantDto participantDto);
        Task<Participant?> GetParticipant(int id);
        Task<IEnumerable<Participant>> GetAllParticipants();

    }
}