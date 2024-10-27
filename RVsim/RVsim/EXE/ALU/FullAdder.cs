using System;
using System.Linq;
using PicoSim;

namespace RVsim.EXE.ALU;

public class FullAdder<T>
  where T : unmanaged, IComparable<T>, IConvertible
{
  private readonly Port<T> _sum;

  public Port<T> Sum { get; }
  public Port<bool> Co { get; }

  public FullAdder(string name, Port<T> A, Port<T> B, Port<bool> Ci)
  {
    Sum = new Port<T>($"{name}.{nameof(Sum)}");
    Co = new Port<bool>($"{name}.{nameof(Co)}");

    var aBits = new Splitter<T>($"{name}.{nameof(A)}.Splitter", A);
    var bBits = new Splitter<T>($"{name}.{nameof(B)}.Splitter", B);
    var slices = new FullAdderBitSlice[Sum.Size()];

    for (var i = 0; i < slices.Length; i++)
    {
      slices[i] = new FullAdderBitSlice($"{name}.Slice[{i}]", aBits.Q[i], bBits.Q[i], i == 0 ? Ci : slices[i - 1].Co);
    }

    _sum = new Joiner<T>($"{name}.{nameof(Sum)}.Joiner", slices.Select(s => s.Sum).ToArray()).Q;

    _sum.PortChanged += (_, _) => { Sum.Value = _sum.Value; };
    slices[^1].Co.PortChanged += (s, _) => { Co.Value = ((Port<bool>)s)!.Value; };
  }
}
