using PicoSim;

namespace RVsim.Parts
{
  public class FlipFlop : Register<bool>
  {
    public FlipFlop(string name, Port<bool> d, Port<bool> e, Port<bool> clk, Sensitivity sensitivity)
      : base(name, d, e, clk, sensitivity)
    {
      Qn = new Port<bool>(name, nameof(Qn), true);
    }

    public FlipFlop(string name, Port<bool> d, Port<bool> clk, Sensitivity sensitivity)
      : this(name, d, new Port<bool>(string.Empty, true), clk, sensitivity)
    {
    }

    public FlipFlop(string name, Port<bool> d, Port<bool> clk)
      : this(name, d, new Port<bool>(string.Empty, true), clk, Sensitivity.PositiveEdge)
    {
    }

    public Port<bool> Qn { get; }

    protected override void OnUpdate(object sender, PortChangingEventArgs<bool> args)
    {
      Qn.Value = !Q.Value;
    }
  }
}
