using Domain.Contract.Entity;

namespace Domain.Contract.Repository.FeeTaxRules
{
    public interface IFeeTaxRuleQueryRepository
    {
        Task<IEnumerable<FeeTaxRule>> GetFeeTaxRules();
    }
}
