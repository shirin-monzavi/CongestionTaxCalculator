using Domain.Contract.Vehicle;

namespace Domain.Entity
{
    public class Tractor : IVehicle
    {
        public string GetName()
        {
           return nameof(Tractor);
        }
    }
}
