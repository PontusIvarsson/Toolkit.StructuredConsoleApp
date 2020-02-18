using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StucturedConsoleApp.Features.commit
{
    public class CommitHandler : AsyncRequestHandler<Commit>
    {
        protected override Task Handle(Commit request, CancellationToken cancellationToken)
        {
            Console.WriteLine("sdfa");
            return Task.FromResult(Unit.Value);
        }
    }
}
