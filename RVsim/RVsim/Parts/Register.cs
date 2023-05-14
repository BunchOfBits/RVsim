using System;

using PicoSim;

namespace RVsim.Parts
{
  public class Register<T>
    where T : IComparable
  {
    private readonly Port<T> _d;
    private readonly Port<bool> _e;
    private readonly Port<bool> _clk;
    private readonly Sensitivity _sensitivity;

    public Register(string name, Port<T> d, Port<bool> e, Port<bool> clk, Sensitivity sensitivity)
    {
      _d = d;
      _e = e;
      _clk = clk;
      _sensitivity = sensitivity;

      Q = new Port<T>(name, nameof(Q));

      _clk.PortChanging += Update;
    }

    public Register(string name, Port<T> d, Port<bool> clk, Sensitivity sensitivity)
      : this(name, d, new Port<bool>(string.Empty, true), clk, sensitivity)
    {
    }

    public Register(string name, Port<T> d, Port<bool> clk)
      : this(name, d, new Port<bool>(string.Empty, true), clk, Sensitivity.PositiveEdge)
    {
    }

    public Port<T> Q { get; }

    private void Update(object sender, PortChangingEventArgs<bool> args)
    {
      if (_e.Value)
      {
        var edge = _sensitivity == Sensitivity.PositiveEdge;

        if (_clk.Value == !edge && args.NewValue == edge)
        {
          Q.Value = _d.Value;
          OnUpdate(sender, args);
        }
      }
    }

    protected virtual void OnUpdate(object sender, PortChangingEventArgs<bool> args)
    {
    }
  }
}
