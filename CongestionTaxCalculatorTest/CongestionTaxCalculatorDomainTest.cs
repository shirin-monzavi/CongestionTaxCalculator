using Domain;
using Domain.Contract.Entity;
using Domain.Contract.Vehicle;

namespace CongestionTaxCalculatorTest
{
    public class CongestionTaxCalculatorDomainTest
    {
        private CongestionTaxCalculator sut;
        public CongestionTaxCalculatorDomainTest()
        {
            sut = new CongestionTaxCalculator(new List<FeeTaxRule>()
            {
                new FeeTaxRule()
                {
                    Amount=8,
                    FromDateTime= new DateTime(2013, 08, 08, 06, 0, 0, 0),
                    ToDateTime= new DateTime(2013, 08, 08, 06, 29, 0, 0),
                },

                new FeeTaxRule()
                {
                    Amount=13,
                    FromDateTime= new DateTime(2013, 08, 08, 06, 30, 0, 0),
                    ToDateTime= new DateTime(2013, 08, 08, 06, 59, 0, 0),
                },

                new FeeTaxRule()
                {
                    Amount=18,
                    FromDateTime= new DateTime(2013, 08, 08, 07, 0, 0, 0),
                    ToDateTime= new DateTime(2013, 08, 08, 07, 59, 0, 0),
                },

                new FeeTaxRule()
                {
                    Amount=13,
                    FromDateTime= new DateTime(2013, 08, 08, 08, 0, 0, 0),
                    ToDateTime= new DateTime(2013, 08, 08, 08, 29, 0, 0),
                },

                new FeeTaxRule()
                {
                    Amount=8,
                    FromDateTime= new DateTime(2013, 08, 08, 08, 30, 0, 0),
                    ToDateTime= new DateTime(2013, 08, 08, 14, 59, 0, 0),
                },

                new FeeTaxRule()
                {
                    Amount=13,
                    FromDateTime= new DateTime(2013, 08, 08, 15, 0, 0, 0),
                    ToDateTime= new DateTime(2013, 08, 08, 15, 29, 0, 0),
                },

                new FeeTaxRule()
                {
                    Amount=18,
                    FromDateTime= new DateTime(2013, 08, 08, 15, 30, 0, 0),
                    ToDateTime= new DateTime(2013, 08, 08, 16, 59, 0, 0),
                },

                new FeeTaxRule()
                {
                    Amount=13,
                    FromDateTime= new DateTime(2013, 08, 08, 17, 0, 0, 0),
                    ToDateTime= new DateTime(2013, 08, 08, 17, 59, 0, 0),
                },

                new FeeTaxRule()
                {
                    Amount=8,
                    FromDateTime= new DateTime(2013, 08, 08, 18, 0, 0, 0),
                    ToDateTime= new DateTime(2013, 08, 08, 18, 29, 0, 0),
                },

                new FeeTaxRule()
                {
                    Amount=0,
                    FromDateTime= new DateTime(2013, 08, 08, 18, 30, 0, 0),
                    ToDateTime= new DateTime(2013, 08, 08, 05, 59, 0, 0),
                },
            },
            new MaxfeeTax()
            {
                Id = 1,
                MaxAmount = 60,
                IsActive = true
            }
            );
        }

        [Theory]
        [MemberData(nameof(Data))]
        public void CongestionTaxCalculator_Should_Return_Proper_Total_Fee(
            object vehicle,
            List<DateTime> periods,
            int expectedResult)
        {
            //Arrenge

            //Act
            var actual = sut.CalculatCongestionTax(vehicle as IVehicle, periods);

            //Assert
            Assert.Equal(expectedResult, actual);
        }


        public static IEnumerable<object[]> Data =>
            new List<object[]>(){

                 new object[] {
                     new Motorcycle(),new List<DateTime> {
                         new DateTime(2013,02,02)
                     },0
                 },
                  new object[] {
                     new Tractor(),new List<DateTime> {
                         new DateTime(2013,02,02)
                     },0
                 },
                  new object[] {
                     new Car(),new List<DateTime> {
                         new DateTime(2013,01,06)
                     },0
                 },
                  new object[] {
                     new Car(),new List<DateTime> {
                         new DateTime(2013,01,05)
                     },0
                 },
                  new object[] {
                     new Car(),
                      new List<DateTime> {
                         new DateTime(2013,07,08)
                     },0
                 },
                  new object[] {
                     new Car(),new List<DateTime> {
                         new DateTime(2013, 08, 08, 06, 20, 0, 0)
                     },8
                 },
                  new object[] {
                     new Car(),new List<DateTime> {
                         new DateTime(2013, 08, 08, 06, 45, 0, 0)
                     },13
                 },
                  new object[] {
                     new Car(),new List<DateTime> {
                         new DateTime(2013, 08, 08, 15, 45, 0, 0)
                     },18
                 },
                  new object[] {
                    new Car(),new List<DateTime>()
                    {
                        new DateTime(2013, 08, 08, 15, 45, 0, 0),
                        new DateTime(2013, 08, 08, 16, 10, 0, 0),
                        new DateTime(2013, 08, 08, 18, 10, 0, 0),
                        new DateTime(2013, 08, 08, 8, 10, 0, 0),
                        new DateTime(2013, 08, 08, 7, 10, 0, 0),
                        new DateTime(2013, 08, 08, 6, 10, 0, 0)
                    },60
                 },
                  new object[] {
                    new Car(),new List<DateTime>()
                    {
                        new DateTime(2013, 08, 08, 6, 0, 0, 0),
                        new DateTime(2013, 08, 08, 6, 30, 0, 0),
                        new DateTime(2013, 08, 08, 7, 0, 0, 0),
                    },18
                 }
            };
    }
}