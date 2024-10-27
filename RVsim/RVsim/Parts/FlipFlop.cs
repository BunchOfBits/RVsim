using System;
using PicoSim;

namespace RVsim.Parts;

public class FlipFlop : Register<bool>
{
  public Port<bool> Qn { get; }

  public FlipFlop(string name, Port<bool> d, Port<bool> e, Port<bool> clk, Sensitivity sensitivity = Sensitivity.PositiveEdge)
    : base(name, d, e, clk, sensitivity)
  {
    Qn = new Port<bool>(name, nameof(Qn), true);

    Q.PortChanged += Setup;
  }

  public FlipFlop(string name, Port<bool> d, Port<bool> clk, Sensitivity sensitivity = Sensitivity.PositiveEdge)
    : this(name, d, new Port<bool>(string.Empty, true), clk, sensitivity)
  {
  }

  protected void Setup(object sender, EventArgs args)
  {
    Qn.Value = !Q.Value;
  }
}