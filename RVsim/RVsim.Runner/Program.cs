using PicoSim;

using RVsim.EXE.BarrelShifter;

namespace RVsim.Runner
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var d = new Source<uint>("D");
      var amount = new Source<byte>("Amount");
      var right = new Source<bool>("Right");
      var arithmetic = new Source<bool>("Arithmetic");
      var clk = new Source<bool>("Clk");

      var sim = new BarrelShifter("Shift", d.Port, amount.Port, right.Port, arithmetic.Port, clk.Port);

      GC.Collect(2, GCCollectionMode.Forced, true, true);
      Thread.Sleep(2000);
    }
  }
}
