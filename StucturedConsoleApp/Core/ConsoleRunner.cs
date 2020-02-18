using MediatR;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StucturedConsoleApp.Core
{
    class ConsoleRunner
    {
        public ConsoleRunner(IContainer container)
        {
            _container = container;
            _mediator = _container.GetInstance<IMediator>();
        }

        public void AddTraceListner(TraceListener traceListener)
        {
            Debug.Listeners.Add(traceListener);
        }

        IContainer _container;
        IMediator _mediator;

        public async Task Run(string[] args)
        {
            try
            {
                IRequest request = _container.GetInstance<ICommandModelBinder>().Bind(args);
                await _mediator.Send(request);
            }
            catch (StructuredArgumentErrorException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.ReadKey();
        }
    }
}
