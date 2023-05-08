using PicoSim;

namespace RVsim
{
  public class FlipFlop
  {
    private readonly Port<bool> _d;
    private readonly Port<bool> _clk;

    public FlipFlop(string name, Port<bool> d, Port<bool> clk)
    {
      _d = d;
      _clk = clk;

      Q = new Port<bool>(name, nameof(Q));
      Qn = new Port<bool>(name, nameof(Qn), true);

      _clk.PortChanging += Update;
    }

    public Port<bool> Q { get; }

    public Port<bool> Qn { get; }

    private void Update(object sender, PortChangingEventArgs<bool> args)
    {
      if (_clk.Value == false && args.NewValue == true)
      {
        Q.Value = _d.Value;
        Qn.Value = !_d.Value;
      }
    }
  }
}
