using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Diagnosea.Submarine.Abstractions.Enums;
using Diagnosea.Submarine.Domain.User.Commands.InsertUser;
using Diagnosea.Submarine.Domain.User.Entities;
using MongoDB.Driver;
using Moq;
using NUnit.Framework;

namespace Diagnosea.Submarine.Domain.User.UnitTests.Commands
{
    [TestFixture]
    public class InsertUserCommandHandlerTests
    {
        private Mock<IMongoDatabase> _mongoDatabase;
        private Mock<IMongoCollection<UserEntity>> _usercollection;
        private InsertUserCommandHandler _classUnderTest;

        [SetUp]
        public void SetUp()
        {
            _mongoDatabase = new Mock<IMongoDatabase>();
            _usercollection = new Mock<IMongoCollection<UserEntity>>();

            _mongoDatabase
                .Setup(x => x.GetCollection<UserEntity>("User", null))
                .Returns(_usercollection.Object);
            
            _classUnderTest = new InsertUserCommandHandler(_mongoDatabase.Object);
        }
        
        public class Handle : InsertUserCommandHandlerTests
        {
            [Test]
            public async Task GivenInsertUserCommand_InsertsUserIntoMongoCollection()
            {
                // Arrange
                var cancellationToken = new CancellationToken();
                
                var insertUserCommand = new InsertUserCommand
                {
                    Id = Guid.NewGuid(),
                    EmailAddress = "john.smith@gmail.com",
                    Password = "30=5902i0jfe-q0dj-0",
                    UserName = "Johnoo2398",
                    Roles = new List<UserRole> {UserRole.Standard}
                };

                // Act
                var result = await _classUnderTest.Handle(insertUserCommand, cancellationToken);
                
                // Assert
                Assert.That(result, Is.Not.Null);
                
                _usercollection.Verify(x => 
                    x.InsertOneAsync(It.Is<UserEntity>(user => VerifyUserEntity(user, insertUserCommand)), null, cancellationToken),
                    Times.Once);
            }

            private static bool VerifyUserEntity(UserEntity user, InsertUserCommand command)
            {
                return user.Id == command.Id &&
                       user.EmailAddress == command.EmailAddress &&
                       user.Password == command.Password &&
                       user.UserName == command.UserName &&
                       user.Roles.Contains(UserRole.Standard);
            }
        }
    }
}