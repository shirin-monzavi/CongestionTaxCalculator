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

            var samePeriods = new List<DateTime>();

            foreach (var item in periods)
            {
                if (checkWeekends(item) || checkJulyMonth(item)) return totalFee;

                foreach (var refItem in periods.Where(p => periods.IndexOf(p) > periods.IndexOf(item)))
                {
                    var interval = subtractDateTime(item, refItem);

                    if (interval.Hours <= 1 && !samePeriods.Contains(refItem))
                    {
                        samePeriods.Add(refItem);
                        samePeriods.Add(item);

                        var itemFee = calculateTaxBasedOnPeriods(item);
                        var refIemFee = calculateTaxBasedOnPeriods(refItem);

                        if (tempFee < itemFee || tempFee < refIemFee)
                        {
                            if (itemFee < refIemFee)
                                tempFee = refIemFee;
                            else
                                tempFee = itemFee;
                        }
                    }
                }

                totalFee = totalFee + tempFee;
                tempFee = 0;

                if (!samePeriods.Contains(item))
                {
                    totalFee = totalFee + calculateTaxBasedOnPeriods(item);
                }

                if (totalFee > _maxfeeTax.MaxAmount)
                {
                    totalFee = _maxfeeTax.MaxAmount;
                    break;
                }
            }

            return totalFee;
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