using Domain.Contract.Vehicle;

namespace Domain.Entity
{
    public class Motorcycle : IVehicle
    {
        public string GetName()
        {
            return nameof(Motorcycle);
        }
    }
}
