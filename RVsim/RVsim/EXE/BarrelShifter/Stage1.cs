using System;

using PicoSim;

namespace RVsim.EXE.BarrelShifter;

internal class Stage1
{
  private readonly Port<uint> _d;
  private readonly Port<byte> _amount;
  private readonly Port<bool> _right;
  private readonly Port<bool> _arithmetic;

  public Port<uint> Q { get; }

  public Stage1(string name, Port<uint> d, Port<byte> amount, Port<bool> right, Port<bool> arithmetic)
  {
    _d = d;
    _amount = amount;
    _right = right;
    _arithmetic = arithmetic;

    Q = new Port<uint>($"{name}.{nameof(Q)}");

    _d.PortChanged += Setup;
    _amount.PortChanged += Setup;
    _right.PortChanged += Setup;
    _arithmetic.PortChanged += Setup;
  }

  private void Setup(object sender, EventArgs e)
  {
    const bool right = true;
    const bool left = false;
    const bool shift16 = true;
    const bool shift0 = false;
    const bool logical = false;
    const bool arithmetic = true;

    switch (_arithmetic.Value, _right.Value, (_amount.Value & 0x10) != 0)
    {
      case (_, _, shift0):
        Q.Value = _d.Value;
        break;
      case (_, left, shift16):
        Q.Value = _d.Value << 16;
        break;
      case (logical, right, shift16):
        Q.Value = _d.Value >> 16;
        break;
      case (arithmetic, right, shift16):
        var d = _d.Value >> 16;

        if ((_d.Value & 0x8000_0000) != 0)
        {
          d |= 0xFFFF_0000;
        }

        Q.Value = d;

        break;
    }
  }
}
