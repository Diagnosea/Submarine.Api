﻿using System.Linq;
using Diagnosea.Submarine.Domain.Tank.Dtos;
using Diagnosea.Submarine.Domain.Tank.Entities.TankWater;

namespace Diagnosea.Submarine.Domain.Tank.Extensions
{
    public static class TankWaterStubExtensions
    {
        public static TankWaterDto ToDto(this TankWaterStub tankWater)
        {
            return new TankWaterDto
            {
                WaterId = tankWater.WaterId,
                Levels = tankWater.Levels? 
                    .Select(tankWaterLevel => tankWaterLevel.ToDto())
                    .ToList(),
                Stage = tankWater.Stage
            };
        }
    }
}