namespace RVsim
{
  public class FlipFlop
  {
    private readonly Port _d;
    private readonly Port _clk;

    public FlipFlop(string name, Port d, Port clk)
    {
      _d = d;
      _clk = clk;

      Q = new Port(name, nameof(Q));
      Qn = new Port(name, nameof(Qn), 1);

      _d.PortChanged += Update;
      _clk.PortChanged += Update;
    }

    public Port Q { get; }

    public Port Qn { get; }

    private void Update(object sender, PortChangedEventArgs args)
    {
      if (_clk.Value == 0u && args.NewValue != 0u)
      {
        Q.Value = _d.Value;
        Qn.Value = _d.Value != 0u ? 0u : 1u;
      }
    }
  }
}
