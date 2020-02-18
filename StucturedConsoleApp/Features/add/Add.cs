using MediatR;
using System;

namespace StucturedConsoleApp.Features.add
{

    public class Add : IRequest
    {
        public Add()
        {
            String = "Defaultvalue";
        }

        public string String { get; set; }
        public int Int { get; set; }
        public DateTime DateTime { get; set; }
        public int? Nint { get; set; }

        public User User { get; set; }
    }

    public class User
    {
        public string Name { get; set; }
    }

    public class AddHandler : RequestHandler<Add>
    {
        protected override void Handle(Add request)
        {
            Console.WriteLine("yeeeeees{0}{1}{2}{3}", request.String, request.Int, request.DateTime, request.Nint);
        }
    }

}
