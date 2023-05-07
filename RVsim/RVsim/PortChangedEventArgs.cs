using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RVsim
{
  internal class PortChangedEventArgs : EventArgs
  {
    public uint Port { get; set; }
  }
}
