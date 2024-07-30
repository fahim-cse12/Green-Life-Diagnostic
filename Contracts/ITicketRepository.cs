using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ITicketRepository
    {
        Task<IEnumerable<Ticket>> GetAllTicketAsync(bool trackChanges);
        Task<Ticket> GetTicketAsync(Guid ticketId, bool trackChanges);
        public Task<Ticket> FindTicketsByConditionAsync(Expression<Func<Ticket, bool>> condition, bool trackChanges);
        
        void CreateTicket(Ticket ticket);
        
        void DeleteTicket(Ticket ticket);
        void UpdateTicket(Ticket ticket);
    }
}
