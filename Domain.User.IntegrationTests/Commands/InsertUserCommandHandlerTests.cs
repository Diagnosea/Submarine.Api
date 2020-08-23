using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Diagnosea.Submarine.Domain.Abstractions.Extensions;
using Diagnosea.Submarine.Domain.User.Commands.InsertUser;
using Diagnosea.Submarine.Domain.User.Entities;
using Diagnosea.Submarine.Domain.User.Enums;
using MongoDB.Driver;
using NUnit.Framework;

namespace Diagnosae.Submarine.Domain.User.IntegrationTests.Commands
{
    [TestFixture]
    public class InsertUserCommandHandlerTests : UserIntegrationTests
    {
        private IMongoCollection<UserEntity> _userCollection;
        private InsertUserCommandHandler _classUnderTest;

        [SetUp]
        public void SetUp()
        {
            _userCollection = Database.GetEntityCollection<UserEntity>();
            _classUnderTest = new InsertUserCommandHandler(Database);
        }

        public class Handle : InsertUserCommandHandlerTests
        {
            [Test]
            public async Task GivenInsertUserCommand_InsertsUserIntoMongoDatabase()
            {
                // Arrange
                var cancellationToken = new CancellationToken();
                
                                
                var insertUserCommand = new InsertUserCommand
                {
                    Id = Guid.NewGuid(),
                    EmailAddress = "john.smith@gmail.com",
                    Password = "30=5902i0jfe-q0dj-0",
                    UserName = "Johnoo2398",
                    Roles = new List<UserRole> {UserRole.StandardUser}
                };
                
                // Act
                var result = await _classUnderTest.Handle(insertUserCommand, cancellationToken);

                var user = await _userCollection
                    .Find(x => x.Id == insertUserCommand.Id)
                    .FirstOrDefaultAsync(cancellationToken);
                
                Assert.Multiple(() =>
                {
                    Assert.That(result, Is.Not.Null);
                    Assert.That(user.EmailAddress, Is.EqualTo(insertUserCommand.EmailAddress));
                    Assert.That(user.Password, Is.EqualTo(insertUserCommand.Password));
                    Assert.That(user.UserName, Is.EqualTo(insertUserCommand.UserName));
                    Assert.That(user.FriendlyName, Is.Null);
                });
            }
        }
    }
}