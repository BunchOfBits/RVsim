using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RVsim
{
  internal class Latch
  {
    private readonly Port _d;
    private readonly Port _e;

    public Latch(string name, Port d, Port e)
    {
      _d = d;
      _e = e;

      Q = new Port(name, nameof(Q));
      Qn = new Port(name, nameof(Qn), 1);

      _d.PortChanged += Update;
      _e.PortChanged += Update;
    }

    public Port Q { get; }

    public Port Qn { get; }

    private void Update(object sender, PortChangedEventArgs args)
    {
      if (_e.Value != 0)
      {
        Q.Value = _d.Value;
        Qn.Value = _d.Value != 0u ? 0u : 1u;
      }
    }
  }
}
