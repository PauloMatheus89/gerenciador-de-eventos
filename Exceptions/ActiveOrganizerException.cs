using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorEventos.Exceptions
{
    public class ActiveOrganizerException : ApplicationException
    {
        public ActiveOrganizerException() : base("Register could not be deleted because its associated with an active Organizer entity.")
        {
            
        }
    }
}