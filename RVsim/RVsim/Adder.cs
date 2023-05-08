using System;

using PicoSim;

namespace RVsim
{
  public class Adder
  {
    private readonly Port<uint> _a;
    private readonly Port<uint> _b;

    public Adder(string name, Port<uint> a, Port<uint> b)
    {
      _a = a;
      _b = b;

      Sum = new Port<uint>(name, nameof(Sum));

      _a.PortChanged += Update;
      _b.PortChanged += Update;
    }

    public Port<uint> Sum { get; }

    private void Update(object sender, EventArgs args)
    {
      Sum.Value = _a.Value + _b.Value;
    }
  }
}
