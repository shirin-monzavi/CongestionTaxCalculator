using Domain.Contract.Vehicle;

namespace Domain.Contract
{
    public interface ICongestionTaxCalculator
    {
        int CalculatCongestionTax(IVehicle vehicle, List<DateTime> periods);
    }
}
