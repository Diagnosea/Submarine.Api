using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Diagnosea.Migrator.Extensions;
using Diagnosea.Migrator.Settings;
using Diagnosea.Submarine.Abstractions.Enums;
using Diagnosea.Submarine.Domain.Abstractions.Builders;
using Diagnosea.Submarine.Domain.Abstractions.Extensions;
using Diagnosea.Submarine.Domain.License.Entities;
using Diagnosea.Submarine.Domain.User.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;

namespace Diagnosea.Migrator
{
    public class Program
    {
        private const string SettingsFileName = "appsettings.json";

        public static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(SettingsFileName)
                .Build();

            var databaseSettings = configuration.GetSettings<DatabaseSettings>();

            var database = new SubmarineDatabaseBuilder()
                .WithConnectionString(databaseSettings.ConnectionString)
                .WithConvention(new EnumRepresentationConvention(BsonType.String))
                .Build();

            var userCollection = database.GetEntityCollection<UserEntity>();
            var licenseCollection = database.GetEntityCollection<LicenseEntity>();
            
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword("password");
            var isValidPassword = BCrypt.Net.BCrypt.Verify("password", hashedPassword);
            Console.WriteLine($"Generated Valid Password: {isValidPassword}");

            var user = new UserEntity
            {
                Id = Guid.NewGuid(),
                EmailAddress = "joshua.crowe@outlook.com",
                Password = hashedPassword,
                FriendlyName = "Joshua Crowe",
                UserName = "InsertBeforeFlight",
                Roles = new List<UserRole> {UserRole.Standard}
            };

            await userCollection.InsertOneAsync(user);

            var productKeyBytes = Encoding.UTF8.GetBytes("productKey");
            var productKey = Convert.ToBase64String(productKeyBytes);
            var hashedProductKey = BCrypt.Net.BCrypt.HashPassword(productKey);

            var product = new LicenseProductEntity
            {
                Name = "Submarine",
                Key = hashedProductKey,
                Expiration = DateTime.UtcNow.AddYears(1)
            };

            var licenseKeyBytes = Encoding.UTF8.GetBytes("licenseKey");
            var licenseKey = Convert.ToBase64String(licenseKeyBytes);
            var hashedLicenseKey = BCrypt.Net.BCrypt.HashPassword(licenseKey);

            var license = new LicenseEntity
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Key = hashedLicenseKey,
                Products = new List<LicenseProductEntity> { product }
            };

            await licenseCollection.InsertOneAsync(license);
            
            Console.WriteLine($"Inserted User '{user.Id}' with License '{license.Id}'");
        }
    }
}