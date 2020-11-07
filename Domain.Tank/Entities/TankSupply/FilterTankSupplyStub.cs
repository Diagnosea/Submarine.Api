using System.Collections.Generic;
using Diagnosea.Submarine.Abstractions.Enums.Tank;

namespace Domain.Tank.Entities.TankSupply
{
    public class FilterTankSupplyStub : TankSupplyStub
    {
        public IList<TankSupplyStub> Supplies { get; set; }

        public FilterTankSupplyStub()
        {
            Component = TankSupplyComponent.Filter;
        }
    }
}