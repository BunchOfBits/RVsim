using System;

using PicoSim;

using RVsim.Parts;

namespace RVsim.EXE.BarrelShifter
{
  public class BarrelShifter
  {
    private readonly Port<uint> _d;
    private readonly Port<byte> _amount;
    private readonly Port<bool> _right;
    private readonly Port<bool> _arithmetic;
    private readonly Port<bool> _clk;

    private readonly Stage1 _stage1;
    private readonly Register<uint> _register;

    private readonly Stage2 _stage2;
    private readonly Stage3 _stage3;
    private readonly Stage4 _stage4;
    private readonly Stage5 _stage5;

    public BarrelShifter(string name, Port<uint> d, Port<byte> amount, Port<bool> right, Port<bool> arithmetic, Port<bool> clk)
    {
      _d = d;
      _amount = amount;
      _right = right;
      _arithmetic = arithmetic;
      _clk = clk;

      _stage1 = new Stage1($"{name}.Stage1", _d, _amount, _right, _arithmetic);
      _register = new Register<uint>($"{name}.Stage1.Register", _stage1.Q, _clk);

      Q = new Port<uint>(name, nameof(Q));

      _register.Q.PortChanged += Update;
    }

    public Port<uint> Q { get; }

    private void Update(object sender, EventArgs e)
    {
      Q.Value = _register.Q.Value;
    }
  }
}
