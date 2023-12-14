using Domain.Contract;
using Domain.Contract.Enum;
using Domain.Contract.Vehicle;

namespace Domain
{
    public class CongestionTaxCalculator : ICongestionTaxCalculator
    {
        public int CalculatCongestionTax(IVehicle vehicle, List<DateTime> periods)
        {
            bool isExemptVehicle = this.isExemptVehicle(vehicle.GetName().ToLower());

            if (isExemptVehicle) return 0;

            foreach (DateTime period in periods)
            {
                bool isWeekends = checkWeekends(period);
                bool isJulyMonth = checkJulyMonth(period);

                if (isWeekends || isJulyMonth) return 0;
            }

            return 1;
        }

        private bool isExemptVehicle(string name)
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

        private bool checkWeekends(DateTime date)
        {
            if (date.DayOfWeek == DayOfWeek.Saturday ||
                date.DayOfWeek == DayOfWeek.Sunday)
                return true;

            return false;
        }

        private bool checkJulyMonth(DateTime date)
        {
            if (date.Month == (int)Months.July)
                return true;

            return false;
        }
    }
}