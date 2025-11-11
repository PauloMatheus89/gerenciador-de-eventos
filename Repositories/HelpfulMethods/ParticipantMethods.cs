using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorEventos.Domain.Models.Entities;
using GerenciadorEventos.Infrastructure.Databases;

namespace GerenciadorEventos.Repositories.HelpfulMethods
{
    public static class ParticipantMethods
    {
        public static bool ParticipantExists(DatabaseContext context, Participant participant)
        {
            if (participant != null && context.Participants.Any(a => a.Id == participant.Id))
                return true;

            return false;
        }

        public static void UpdateParticipant(DatabaseContext context, Participant? participant, Participant? participantToUpdate)
        {
            if (participant != null && participantToUpdate != null)
            {
                participantToUpdate.Name = participant.Name;
                participantToUpdate.Email = participant.Email;
                UserMethods.UpdateUserId(context, participantToUpdate, participant);

            }
            else if (participantToUpdate == null && participant != null)
            {
                if (ParticipantExists(context, participant))
                    context.Participants.Attach(participant);

            }


        }
    }
}