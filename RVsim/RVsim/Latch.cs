namespace RVsim
{
  public class Latch
  {
    private readonly Port _d;
    private readonly Port _e;

    public Latch(string name, Port d, Port e)
    {
      _d = d;
      _e = e;

      Q = new Port(name, nameof(Q));
      Qn = new Port(name, nameof(Qn), 1);

      _d.PortChanged += UpdateD;
      _e.PortChanged += UpdateE;
    }

    public Port Q { get; }

    public Port Qn { get; }

    private void UpdateD(object sender, PortChangedEventArgs args)
    {
      Update(args.NewValue, _e.Value);
    }

    private void UpdateE(object sender, PortChangedEventArgs args)
    {
      Update(_d.Value, args.NewValue);
    }
  
    private void Update(uint d, uint e)
    {
      if (e != 0)
      {
        Q.Value = d;
        Qn.Value = d != 0u ? 0u : 1u;
      }
    }
  }
}
