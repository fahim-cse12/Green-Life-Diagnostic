using Contracts;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class InvestigationRepository : RepositoryBase<Investigation>, IInvestigationRepository
    {
        public InvestigationRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        { }
    }
}
