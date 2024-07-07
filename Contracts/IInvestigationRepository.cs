using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IInvestigationRepository
    {
        Task<IEnumerable<Investigation>> GetAllInvestigationAsync(bool trackChanges);
        Task<Investigation> GetInvestigationAsync(Guid investigationId, bool trackChanges);
        void CreateInvestigation(Investigation investigation);
        void DeleteInvestigation(Investigation investigation);
    }
}
