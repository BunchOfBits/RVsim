using System;

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

      _a.PortChanged += UpdateA;
      _b.PortChanged += UpdateB;
    }

    public Port<uint> Sum { get; }

    private void UpdateA(object sender, EventArgs args)
    {
      Update(_a.Value, _b.Value);
    }

    private void UpdateB(object sender, EventArgs args)
    {
      Update(_a.Value, _b.Value);
    }

    private void Update(uint a, uint b)
    {
      Sum.Value = a + b;
    }
  }
}
