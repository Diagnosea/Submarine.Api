﻿using System;

namespace Diagnosea.Submarine.Domain.License.Entities
{
    public class LicenseProductEntity
    {
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public DateTime Expiration { get; set; }
    }
}