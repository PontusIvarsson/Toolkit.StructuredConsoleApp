using MediatR;
using System.Collections.Generic;
using System.Linq;

namespace StucturedConsoleApp.Core
{
    public class GenericCommand : IRequest
    {
        public GenericCommand()
        {
            Arguments = new Dictionary<string, string>();
            NoneOptArguments = new List<string>();
        }
        public string CommandName { get { return NoneOptArguments.FirstOrDefault(); } private set {} }
        public Dictionary<string, string> Arguments { get; private set; }
        public List<string> NoneOptArguments { get; private set; }

    }
}
