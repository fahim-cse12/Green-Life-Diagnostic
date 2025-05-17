using Entities.Models;

namespace Contracts
{
    public interface IInvestigationRepository
    {
        Task<IEnumerable<Investigation>> GetAllInvestigationAsync(bool trackChanges);
        Task<Investigation> GetInvestigationAsync(Guid investigationId, bool trackChanges);
        void CreateInvestigation(Investigation investigation);
        void DeleteInvestigation(Investigation investigation);
        void UpdateInvestigation(Investigation investigation);
    }
}
