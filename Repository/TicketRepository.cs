using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<Ticket>> GetAllTicketAsync(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(i => i.Id).ToListAsync();
        }

        public async Task<Ticket> GetTicketAsync(Guid ticketId, bool trackChanges)
        {
            return await FindByCondition(x => x.Id.Equals(ticketId), trackChanges).SingleOrDefaultAsync();
        }

        public void UpdateTicket(Ticket ticket)
        {
            Update(ticket); 
        }
    }
}
