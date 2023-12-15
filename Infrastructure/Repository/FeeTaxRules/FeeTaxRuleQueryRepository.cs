using Domain.Contract.Entity;
using Domain.Contract.Repository.FeeTaxRules;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.FeeTaxRules
{
    public class FeeTaxRuleQueryRepository : IFeeTaxRuleQueryRepository
    {
        private readonly DbContext _dbContext;
        public FeeTaxRuleQueryRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<FeeTaxRule>> GetFeeTaxRules()
        {
           return await _dbContext.Set<FeeTaxRule>().AsNoTracking().ToListAsync();
        }
    }
}
