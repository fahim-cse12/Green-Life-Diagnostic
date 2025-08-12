using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Repository
{
    public class TicketRepository : RepositoryBase<Ticket>, ITicketRepository
    {
        public TicketRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        { }

        public void CreateTicket(Ticket ticket)
        {
            Create(ticket);
        }

        public void DeleteTicket(Ticket ticket)
        {
            Delete(ticket); 
        }

        public async Task<Ticket> FindTicketsByConditionAsync(Expression<Func<Ticket, bool>> condition, bool trackChanges)
        {
            return await FindByCondition(condition, trackChanges).OrderByDescending(i=> i.UniqueId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Ticket>> GetAllTicketAsync(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(i => i.Id).ToListAsync();
        }

        public async Task<Ticket> GetTicketAsync(Guid ticketId, bool trackChanges)
        {
            return await FindByCondition(x => x.Id.Equals(ticketId), trackChanges).SingleOrDefaultAsync();
        }

        public IQueryable<Ticket> GetTicketsByCondition(Expression<Func<Ticket, bool>> condition, bool trackChanges)
        {
            return FindByCondition(condition, trackChanges);
        }
      
        public void UpdateTicket(Ticket ticket)
        {
            Update(ticket); 
        }
    }
}
