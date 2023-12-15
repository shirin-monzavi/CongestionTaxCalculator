using Domain.Contract.Vehicle;

namespace Domain.Contract.Entity
{
    public class Motorcycle : IVehicle
    {
        public string GetName()
        {
            return nameof(Motorcycle);
        }
    }
}
