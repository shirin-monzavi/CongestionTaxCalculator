using Domain.Contract.Vehicle;

namespace Domain.Entity
{
    public class Car : IVehicle
    {
        public string GetName()
        {
            return nameof(Car);
        }
    }
}
