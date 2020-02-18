using MediatR;
using StucturedConsoleApp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StucturedConsoleApp.Features.commit
{
    public class DwTransfer : IRequest
    {
        public string vilken { get; set; }

        public int valfri { get; set; }

        public int? intevalfri { get; set; }
    }
}
