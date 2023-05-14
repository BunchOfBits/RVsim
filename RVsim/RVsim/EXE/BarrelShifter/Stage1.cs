using System;

using PicoSim;

namespace RVsim.EXE.BarrelShifter
{
  internal class Stage1
  {
    private readonly Port<uint> _d;
    private readonly Port<byte> _amount;
    private readonly Port<bool> _right;
    private readonly Port<bool> _arithmetic;

    public Stage1(string name, Port<uint> d, Port<byte> amount, Port<bool> right, Port<bool> arithmetic)
    {
      _d = d;
      _amount = amount;
      _right = right;
      _arithmetic = arithmetic;

      Q = new Port<uint>(name, nameof(Q));

      _d.PortChanged += Update;
      _amount.PortChanged += Update;
      _right.PortChanged += Update;
      _arithmetic.PortChanged += Update;
    }

    public Port<uint> Q { get; }

    private void Update(object sender, EventArgs e)
    {
      var select = (_right.Value ? 1 : 0) + ((_amount.Value & 0x10) != 0 ? 2 : 0);
      uint q;

      switch(select)
      {
        case 0: // No shift, left
          q = _d.Value;
          break;
        case 1: // No shift, right
          q = _d.Value;
          break;
        case 2: // Shift, left
          q = _d.Value << 16;
          break;
        case 3: // Shift, right
          q = _d.Value >> 16;

          if (((_d.Value & 0x8000_0000) != 0) && _arithmetic.Value)
          {
            q |= 0x8000_0000;
          }
          break;
        default:
          throw new InvalidOperationException();
      }

      Q.Value = q;
    }
  }
}
