using PicoSim;

namespace RVsim.EXE.BarrelShifter
{
  public class BarrelShifter
  {
    public BarrelShifter(string name, Port<uint> d, Port<byte> amount, Port<bool> right, Port<bool> arithmetic, Port<bool> clk)
    {
      var stage1 = new Stage1($"{name}.Stage1", clk, d, amount, right, arithmetic);
      var stage2 = new Stage2($"{name}.Stage2", clk, stage1.Q, stage1.Amount, stage1.Right, stage1.Arithmetic);
      var stage3 = new Stage3($"{name}.Stage3", clk, stage2.Q, stage2.Amount, stage2.Right, stage2.Arithmetic);
      var stage4 = new Stage4($"{name}.Stage4", clk, stage3.Q, stage3.Amount, stage3.Right, stage3.Arithmetic);
      var stage5 = new Stage5($"{name}.Stage5", clk, stage4.Q, stage4.Amount, stage4.Right, stage4.Arithmetic);

      Q = stage5.Q;
    }

    public Port<uint> Q { get; }
  }
}
