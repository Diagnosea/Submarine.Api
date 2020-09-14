using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using Moq.Language.Flow;

namespace Diagnosea.Submarine.UnitTestPack.Extensions
{
    public static class MediatorMockExtensions
    {
        public static ISetup<IMediator, Task<TResult>> SetupHandler<TRequest, TResult>(this Mock<IMediator> mock)
            where TRequest : IRequest<TResult>
        {
            return mock.Setup(m => m.Send(It.IsAny<TRequest>(), CancellationToken.None));
        }

        public static void VerifyHandler<TRequest, TResult>(this Mock<IMediator> mock, Expression<Func<TRequest, bool>> verify, Times times)
            where TRequest : IRequest<TResult>
        {
            mock.Verify(m => m.Send(It.Is(verify), CancellationToken.None), times);
        }
    }
}