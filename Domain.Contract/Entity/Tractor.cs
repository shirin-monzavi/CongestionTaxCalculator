using Domain.Contract.Vehicle;

namespace Domain.Contract.Entity
{
    public class Tractor : IVehicle
    {
        public string GetName()
        {
           return nameof(Tractor);
        }
    }
}
