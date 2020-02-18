using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StucturedConsoleApp.Features.commit
{
    public class DwTransferHandler : AsyncRequestHandler<DwTransfer>
    {
        protected override Task Handle(DwTransfer request, CancellationToken cancellationToken)
        {
            Console.WriteLine(request.vilken);
            return Task.FromResult(Unit.Value);
        }


    }
}
