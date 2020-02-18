using MediatR;
using StucturedConsoleApp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StucturedConsoleApp.Features.commit
{
    public class Commit : IRequest
    {
        public string Message { get; set; }
    }
}
