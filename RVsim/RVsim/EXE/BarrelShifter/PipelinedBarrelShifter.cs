using System.Linq;
using PicoSim;
using RVsim.Parts;

namespace RVsim.EXE.BarrelShifter;

public class PipelinedBarrelShifter
{
  public PipelinedBarrelShifter(string name, Port<uint> d, Port<byte> amount, Port<bool> right, Port<bool> arithmetic, Port<bool> clk)
  {
    var amount1 = new Register<byte>($"{name}.Amount[1]", amount, clk);
    var right1 = new Register<bool>($"{name}.Right[1]", right, clk);
    var arithmetic1 = new Register<bool>($"{name}.Arithmetic[1]", arithmetic, clk);
    var stage1 = new Stage1($"{name}.Stage1", d, amount, right, arithmetic);
    var register1 = new Register<uint>($"{name}.Register[1]", stage1.Q, clk);

    var amount2 = new Register<byte>($"{name}.Amount[2]", amount1.Q, clk);
    var right2 = new Register<bool>($"{name}.Right[2]", right1.Q, clk);
    var arithmetic2 = new Register<bool>($"{name}.Arithmetic[2]", arithmetic1.Q, clk);
    var stage2 = new Stage2($"{name}.Stage2", register1.Q, amount1.Q, right1.Q, arithmetic1.Q);
    var register2 = new Register<uint>($"{name}.Register[2]", stage2.Q, clk);

    var amount3 = new Register<byte>($"{name}.Amount[3]", amount2.Q, clk);
    var right3 = new Register<bool>($"{name}.Right[3]", right2.Q, clk);
    var arithmetic3 = new Register<bool>($"{name}.Arithmetic[3]", arithmetic2.Q, clk);
    var stage3 = new Stage3($"{name}.Stage3", register2.Q, amount2.Q, right2.Q, arithmetic2.Q);
    var register3 = new Register<uint>($"{name}.Register[3]", stage3.Q, clk);

    var amount4 = new Register<byte>($"{name}.Amount[4]", amount3.Q, clk);
    var right4 = new Register<bool>($"{name}.Right[4]", right3.Q, clk);
    var arithmetic4 = new Register<bool>($"{name}.Arithmetic[4]", arithmetic3.Q, clk);
    var stage4 = new Stage4($"{name}.Stage4", register3.Q, amount3.Q, right3.Q, arithmetic3.Q);
    var register4 = new Register<uint>($"{name}.Register[4]", stage4.Q, clk);

    var stage5 = new Stage5($"{name}.Stage5", register4.Q, amount4.Q, right4.Q, arithmetic4.Q);
    var register5 = new Register<uint>($"{name}.Register[5]", stage5.Q, clk);

    Q = register5.Q;
  }

  public Port<uint> Q { get; }

}