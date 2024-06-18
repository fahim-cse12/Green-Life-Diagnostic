using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ITicketRepository
    {
        Task<IEnumerable<Ticket>> GetAllTicketAsync(bool trackChanges);
        Task<Ticket> GetTicketAsync(Guid ticketId, bool trackChanges);
        void CreateTicket(Ticket ticket);
        Task<IEnumerable<Ticket>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        void DeleteTicket(Ticket ticket);
    }
}
