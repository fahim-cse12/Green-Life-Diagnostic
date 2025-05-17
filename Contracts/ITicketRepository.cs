using Entities.Models;
using System.Linq.Expressions;

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
