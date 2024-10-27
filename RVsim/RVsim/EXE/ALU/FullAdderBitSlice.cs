using System;

using PicoSim;

namespace RVsim.EXE.ALU;

public class FullAdderBitSlice
{
  private readonly Port<bool> _a;
  private readonly Port<bool> _b;
  private readonly Port<bool> _ci;

  public Port<bool> Sum { get; }
  public Port<bool> Co { get; }

  public FullAdderBitSlice(string name, Port<bool> A, Port<bool> B, Port<bool> Ci)
  {
    _a = A;
    _b = B;
    _ci = Ci;

    Sum = new Port<bool>($"{name}.{nameof(Sum)}");
    Co = new Port<bool>($"{name}.{nameof(Co)}");

    A.PortChanged += Setup;
    B.PortChanged += Setup;
    Ci.PortChanged += Setup;
  }

  private void Setup(object sender, EventArgs e)
  {
    var aPlusB = _a.Value ^ _b.Value;
    var sum = aPlusB ^ _ci.Value;
    var co = (_a.Value && _b.Value) || (aPlusB && _ci.Value);

    Sum.Value = sum;
    Co.Value = co;
  }
}