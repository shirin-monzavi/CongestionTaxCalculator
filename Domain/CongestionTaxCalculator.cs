using Domain.Contract;
using Domain.Contract.Entity;
using Domain.Contract.Enum;
using Domain.Contract.Vehicle;

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
            int tempFee = 0, totalFee = 0;

            if (isExemptVehicle(vehicle.GetName().ToLower())) return totalFee;

            var firstTime = periods[0];

            foreach (DateTime period in periods)
            {
                if (checkWeekends(period) || checkJulyMonth(period)) return totalFee;

                var interval = subtractDateTime(firstTime, period);

                if (interval.Hours <= 1)
                {
                    var preiodFee = calculateTaxBasedOnPeriods(period);
                    if (tempFee < preiodFee)
                        tempFee = preiodFee;

                    continue;
                }

                totalFee = totalFee + calculateTaxBasedOnPeriods(period);

                if (totalFee > _maxfeeTax.MaxAmount)
                {
                    totalFee = _maxfeeTax.MaxAmount;
                    break;
                }

                totalFee = totalFee + tempFee;

                tempFee = 0;
            }

            return totalFee + tempFee;
        }

        private TimeSpan subtractDateTime(DateTime firstDate, DateTime secondDate)
        {
            if (DateTime.Compare(firstDate, secondDate) <= 0)
            {
                return secondDate - firstDate;
            }

            return firstDate - secondDate;
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