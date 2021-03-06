﻿using Diagnosea.Submarine.Abstractions.Interchange.Responses.Tank;
using Diagnosea.Submarine.Domain.Tank.Dtos;

namespace Diagnosea.Submarine.Api.Abstractions.Interchange.Tank
{
    public static class TankWaterLevelDtoExtensionTests
    {
        public static TankWaterLevelResponse ToResponse(this TankWaterLevelListDto tankWaterLevelList)
        {
            return new TankWaterLevelResponse
            {
                Metric = tankWaterLevelList.Metric,
                Quantity = tankWaterLevelList.Quantity
            };
        }
    }
}