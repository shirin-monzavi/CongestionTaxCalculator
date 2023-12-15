using Domain.Contract.Vehicle;

namespace Domain.Contract.Entity
{
    public class Car : IVehicle
    {
        public string GetName()
        {
            return nameof(Car);
        }
    }
}
