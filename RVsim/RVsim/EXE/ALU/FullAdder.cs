using System;
using System.Linq;
using PicoSim;

namespace RVsim.EXE.ALU;

public class FullAdder<T>
  where T : unmanaged, IComparable<T>, IConvertible
{
  public Port<T> Sum { get; }
  public Port<bool> Co { get; }

  public FullAdder(string name, Port<T> A, Port<T> B, Port<bool> Ci)
  {
    var aBits = new Splitter<T>($"{name}.{nameof(A)}.Splitter", A);
    var bBits = new Splitter<T>($"{name}.{nameof(B)}.Splitter", B);
    var slices = new FullAdderBitSlice[A.Size()];

    for (var i = 0; i < slices.Length; i++)
    {
      slices[i] = new FullAdderBitSlice($"{name}.Slice[{i}]", aBits.Q[i], bBits.Q[i], i == 0 ? Ci : slices[i - 1].Co);
    }

    Sum = new Joiner<T>($"{name}.{nameof(Sum)}.Joiner", slices.Select(s => s.Sum).ToArray()).Q;
    Co = slices[^1].Co;
  }
}
