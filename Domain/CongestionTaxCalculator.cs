using Domain.Contract;
using Domain.Contract.Enum;
using Domain.Contract.Vehicle;
using Domain.Entity;

namespace Domain
{
    public class CongestionTaxCalculator : ICongestionTaxCalculator
    {
        #region Feilds
        private readonly List<FeeTaxRule> _feeTaxRules;
        private readonly MaxfeeTax _maxfeeTax;
        #endregion

        #region Constructor
        public CongestionTaxCalculator(List<FeeTaxRule> feeTaxRules, MaxfeeTax maxfeeTax)
        {
            _feeTaxRules = feeTaxRules;
            _maxfeeTax = maxfeeTax;
        }
        #endregion


        #region Public Methods
        public int CalculatCongestionTax(IVehicle vehicle, List<DateTime> periods)
        {
            int totalFee = 0;

            bool isExemptVehicle = this.isExemptVehicle(vehicle.GetName().ToLower());

            if (isExemptVehicle) return totalFee;

            foreach (DateTime period in periods)
            {
                bool isWeekends = checkWeekends(period);
                bool isJulyMonth = checkJulyMonth(period);

                if (isWeekends || isJulyMonth) return totalFee;

                totalFee = totalFee + calculateTaxBasedOnPeriods(period);

                if (totalFee > _maxfeeTax.MaxAmount)
                {
                    totalFee = _maxfeeTax.MaxAmount;
                    break;
                }
            }

            return totalFee;
        }
        #endregion


        #region Private Methods
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

        private int calculateTaxBasedOnPeriods(DateTime dateTime)
        {
            var getPeriods = _feeTaxRules
                 .Where(f =>
                               DateTime.Compare(f.FromDateTime, dateTime) <= 0 &&
                               DateTime.Compare(f.ToDateTime, dateTime) >= 0
                        );

            if (getPeriods.Any())
            {
                return getPeriods.First().Amount;
            }

            return 0;
        }
        #endregion

    }
}