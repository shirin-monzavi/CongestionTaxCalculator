using Domain;
using Domain.Contract.Vehicle;
using Domain.Entity;

namespace CongestionTaxCalculatorTest
{
    public class CongestionTaxCalculatorDomainTest
    {
        [Theory]
        [MemberData(nameof(Data))]
        public void CongestionTaxCalculator_When_Vehicle_Is_Exempt_Or_IsHoliday_Should_Return_Zero_Total_Fee(
            object vehicle,
            List<DateTime> periods)
        {
            //Arrenge
            var sut = new CongestionTaxCalculator();
            //Act
            var actual = sut.CalculatCongestionTax(vehicle as IVehicle, periods);

            //Assert
            Assert.Equal(0, actual);
        }

       


        public static IEnumerable<object[]> Data =>
            new List<object[]>(){

                 new object[] { 
                     new Motorcycle(),new List<DateTime> { 
                         new DateTime(2013,02,02)
                     } 
                 },
                  new object[] {
                     new Tractor(),new List<DateTime> {
                         new DateTime(2013,02,02)
                     }
                 },
                  new object[] {
                     new Car(),new List<DateTime> {
                         new DateTime(2013,01,06)
                     }
                 },
                  new object[] {
                     new Car(),new List<DateTime> {
                         new DateTime(2013,01,05)
                     }
                 }
            };
    }
}