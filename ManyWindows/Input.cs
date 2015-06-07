using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManyWindows
{
    class Input
    {
        public Link[] OutgoingLinks;

        public Input(int LinksCount)
        {
            OutgoingLinks = new Link[LinksCount];
        }
    }
}
