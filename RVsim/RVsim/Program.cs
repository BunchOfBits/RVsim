using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PicoSim;

using RVsim.EXE.BarrelShifter;

namespace RVsim
{
  public class Program
  {
    private Source<uint> _d;
    private Source<byte> _amount;
    private Source<bool> _right;
    private Source<bool> _arithmetic;
    private Source<bool> _clk;

    private BarrelShifter _dut;

    private Sink<uint> _q;

    public static void Main()
    {
      new Program().Run();
    }

    public void Run()
    {
      _d = new Source<uint>("D");
      _amount = new Source<byte>("Amount");
      _right = new Source<bool>("Right");
      _arithmetic = new Source<bool>("Arithmetic");
      _clk = new Source<bool>("Clk");

      _dut = new BarrelShifter("BarrelShifter", _d.Port, _amount.Port, _right.Port, _arithmetic.Port, _clk.Port);

      _q = new Sink<uint>(_dut.Q);

      _d.Set(65536);
      _amount.Set(16);
      _right.Set(true);
      _arithmetic.Set(false);

      _clk.Set(true);
    }
  }
}
