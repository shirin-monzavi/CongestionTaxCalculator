using Domain.Contract;
using Domain.Contract.Enum;
using Domain.Contract.Vehicle;

namespace Domain
{
    public class CongestionTaxCalculator : ICongestionTaxCalculator
    {
        public int CalculatCongestionTax(IVehicle vehicle, List<DateTime> periods)
        {
            bool isExemptVehicle = IsExemptVehicle(vehicle.GetName().ToLower());

            if (isExemptVehicle)
            {
                return 0;
            }

            return 1;

        }

        private bool IsExemptVehicle(string name)
        {
            foreach (var exemptVehicle in Enum.GetValues(typeof(ExemptVehicles)))
            {
                if (name == exemptVehicle.ToString()!.ToLower())
                {
                    return true;
                }
            }

            return false;
        }
    }
}