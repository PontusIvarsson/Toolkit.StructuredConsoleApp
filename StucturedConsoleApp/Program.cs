using MediatR;
using StructureMap;
using StructureMap.Pipeline;
using StucturedConsoleApp.Core;
using StucturedConsoleApp.Features.add;
using StucturedConsoleApp.Features.commit;
using System;
using System.Diagnostics;
using System.IO;
using static StucturedConsoleApp.Features.add.Add;

namespace StucturedConsoleApp
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            args = Console.ReadLine().Split(' ');

            var container = ConfigureContainer();

            ConsoleRunner c = new ConsoleRunner(container);
            //c.AddTraceListner(new TextWriterTraceListener(new WrappingWriter(Console.Out)));
            await c.Run(args);
        }



        private static Container ConfigureContainer()
        {
            //mediator
            var container = new Container(cfg =>
            {
                cfg.Scan(scanner =>
                {
                    scanner.AssemblyContainingType<Commit>();
                    scanner.ConnectImplementationsToTypesClosing(typeof(IRequestHandler<,>));
                    scanner.ConnectImplementationsToTypesClosing(typeof(INotificationHandler<>));
                });

                //Pipeline
                //cfg.For(typeof(IPipelineBehavior<,>)).Add(typeof(RequestPreProcessorBehavior<,>));
                //cfg.For(typeof(IPipelineBehavior<,>)).Add(typeof(RequestPostProcessorBehavior<,>));
                //cfg.For(typeof(IPipelineBehavior<,>)).Add(typeof(GenericPipelineBehavior<,>));
                //cfg.For(typeof(IRequestPreProcessor<>)).Add(typeof(GenericRequestPreProcessor<>));
                //cfg.For(typeof(IRequestPostProcessor<,>)).Add(typeof(GenericRequestPostProcessor<,>));
                //cfg.For(typeof(IRequestPostProcessor<,>)).Add(typeof(ConstrainedRequestPostProcessor<,>));

                //Constrained notification handlers
                //cfg.For(typeof(INotificationHandler<>)).Add(typeof(ConstrainedPingedHandler<>));
                cfg.For(typeof(AddHandler)).Add(typeof(AddHandler));
                cfg.For(typeof(DwTransfer)).Add(typeof(DwTransfer));


                // This is the default but let's be explicit. At most we should be container scoped.
                cfg.For<IMediator>().LifecycleIs<TransientLifecycle>().Use<Mediator>();
                cfg.For<ServiceFactory>().Use<ServiceFactory>(ctx => ctx.GetInstance);

                //ConsoleRunner
                cfg.For<ICommandModelBinder>().Use<CommandModelBinder>();
                cfg.For<IStringCommandParser>().Use<StringCommandParser>();
                cfg.For<ICommandReflector>().Use<CommandReflector>();
            });



            return container;
        }


    }
}
