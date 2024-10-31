using PicoSim;

namespace RVsim.EXE.BarrelShifter;

public class BarrelShifter
{
  public BarrelShifter(string name, Port<uint> d, Port<byte> amount, Port<bool> right, Port<bool> arithmetic)
  {
    var stage1 = new Stage1($"{name}.Stage1", d, amount, right, arithmetic);
    var stage2 = new Stage2($"{name}.Stage2", stage1.Q, amount, right, arithmetic);
    var stage3 = new Stage3($"{name}.Stage3", stage2.Q, amount, right, arithmetic);
    var stage4 = new Stage4($"{name}.Stage4", stage3.Q, amount, right, arithmetic);
    var stage5 = new Stage5($"{name}.Stage5", stage4.Q, amount, right, arithmetic);

    Q = stage5.Q;
  }

  public Port<uint> Q { get; }
}
