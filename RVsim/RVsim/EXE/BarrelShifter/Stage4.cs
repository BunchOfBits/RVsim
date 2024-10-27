using System;
using PicoSim;
using RVsim.Parts;

namespace RVsim.EXE.BarrelShifter;

internal class Stage4
{
  private readonly Port<uint> _combinatorial;

  private readonly Port<uint> _d;
  private readonly Port<byte> _amount;
  private readonly Port<bool> _right;
  private readonly Port<bool> _arithmetic;

  public Port<uint> Q { get; }
  public Port<byte> Amount { get; }
  public Port<bool> Right { get; }
  public Port<bool> Arithmetic { get; }

  public Stage4(string name, Port<bool> clk, Port<uint> d, Port<byte> amount, Port<bool> right, Port<bool> arithmetic)
  {
    _d = d;
    _amount = amount;
    _right = right;
    _arithmetic = arithmetic;

    _combinatorial = new Port<uint>($"{name}.{nameof(_combinatorial)}");

    Q = new Register<uint>($"{name}.Data", _combinatorial, clk).Q;
    Amount = new Register<byte>($"{name}.{nameof(Amount)}", _amount, clk).Q;
    Right = new Register<bool>($"{name}.{nameof(Right)}", _right, clk).Q;
    Arithmetic = new Register<bool>($"{name}.{nameof(Arithmetic)}", _arithmetic, clk).Q;

    _d.PortChanged += Setup;
    _amount.PortChanged += Setup;
    _right.PortChanged += Setup;
    _arithmetic.PortChanged += Setup;
  }

  private void Setup(object sender, EventArgs e)
  {
    const bool right = true;
    const bool left = false;
    const bool shift2 = true;
    const bool shift0 = false;
    const bool logical = false;
    const bool arithmetic = true;

    switch (_arithmetic.Value, _right.Value, (_amount.Value & 0x02) != 0)
    {
      case (_, _, shift0):
        _combinatorial.Value = _d.Value;
        break;
      case (_, left, shift2):
        _combinatorial.Value = _d.Value << 2;
        break;
      case (logical, right, shift2):
        _combinatorial.Value = _d.Value >> 2;
        break;
      case (arithmetic, right, shift2):
        var d = _d.Value >> 2;

        if ((_d.Value & 0x8000_0000) != 0)
        {
          d |= 0xC000_0000;
        }

        _combinatorial.Value = d;

        break;
    }
  }
}