using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorEventos.Exceptions
{
    public class ActiveInscriptionException : ApplicationException
    {
        public ActiveInscriptionException() : base($"This User has Active Inscriptions, so it could not be deleted.")
        {
            
        }
    }
}