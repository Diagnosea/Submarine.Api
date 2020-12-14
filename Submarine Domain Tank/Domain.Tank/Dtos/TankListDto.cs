using System;
using System.Collections.Generic;

namespace Diagnosea.Submarine.Domain.Tank.Dtos
{
    public class TankListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public TankWaterListDto WaterList { get; set; }
        public IList<TankLivestockListDto> Livestock { get; set; }
        public IList<TankSupplyListDto> Supplies { get; set; }
    }
}