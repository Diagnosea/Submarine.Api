using System;
using System.Collections.Generic;

namespace Diagnosea.Submarine.Domain.Tank.Dtos
{
    public class TankDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public TankWaterDto Water { get; set; }
        public IList<TankLivestockDto> Livestock { get; set; }
        public IList<TankSupplyDto> Supplies { get; set; }
    }
}