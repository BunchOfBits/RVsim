using System;

using PicoSim;

namespace RVsim.Parts
{
  public class Adder
  {
    private readonly Port<uint> _a;
    private readonly Port<uint> _b;
    private readonly Port<bool> _ci;

    public Adder(string name, Port<uint> a, Port<uint> b, Port<bool> ci)
    {
      _a = a;
      _b = b;
      _ci = ci;

      Sum = new Port<uint>(name, nameof(Sum));
      Co = new Port<bool>(name, nameof(Co));

      _a.PortChanged += Update;
      _b.PortChanged += Update;
      _ci.PortChanged += Update;
    }

    public Adder(string name, Port<uint> a, Port<uint> b)
      : this(name, a, b, new Port<bool>(string.Empty, false))
    {
    }

    public Port<uint> Sum { get; }

    public Port<bool> Co { get; }

    private void Update(object sender, EventArgs args)
    {
      var a = _a.Value;
      var b = _b.Value;
      var sum = a + b + (_ci.Value ? 1u : 0u);

      Sum.Value = sum;
      Co.Value = sum < a; // or sum < b, either works
    }
  }
}
