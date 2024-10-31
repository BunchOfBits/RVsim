using PicoSim;
using RVsim.Parts;

namespace RVsim.Runner;

public class Program
{
  public static void Main(string[] args)
  {
    var d = new Source<uint>("D");
    var clk = new Source<bool>("Clk");

    var regWeakRef = new WeakReference(new Register<uint>("Register", d.Port, clk.Port));
    var sim = (regWeakRef.Target as Register<uint>)?.Q;

    GC.Collect(2, GCCollectionMode.Forced, true, true);

    Console.WriteLine($"{regWeakRef.IsAlive} {sim?.Value}");

    Console.WriteLine("Press any key to finish program");
    Console.ReadKey();
  }
}