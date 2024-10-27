using System;

using PicoSim;

namespace RVsim.Parts;

public class Latch
{
  private readonly Port<bool> _d;
  private readonly Port<bool> _e;

  public Latch(string name, Port<bool> d, Port<bool> e)
  {
    _d = d;
    _e = e;

    Q = new Port<bool>(name, nameof(Q));
    Qn = new Port<bool>(name, nameof(Qn), true);

    _d.PortChanged += UpdateD;
    _e.PortChanged += UpdateE;
  }

  public Port<bool> Q { get; }

  public Port<bool> Qn { get; }

  private void UpdateD(object sender, EventArgs args)
  {
    Update(_d.Value, _e.Value);
  }

  private void UpdateE(object sender, EventArgs args)
  {
    Update(_d.Value, _e.Value);
  }
  
  private void Update(bool d, bool e)
  {
    if (e == true)
    {
      Q.Value = d;
      Qn.Value = !d;
    }
  }
}