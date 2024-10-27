using System;
using System.Globalization;
using System.Linq;

namespace PicoSim;

public class Splitter<T>
  where T : unmanaged, IComparable<T>, IConvertible
{
  private readonly Port<T> _d;

  public Port<bool>[] Q { get; }

  public Splitter(string name, Port<T> D)
  {
    _d = D;

    Q = Enumerable.Range(0, _d.Size()).Select(i => new Port<bool>(name, $"{nameof(Q)}[{i}]")).ToArray();

    D.PortChanged += Setup;
  }

  private void Setup(object sender, EventArgs e)
  {
    for (var i = 0; i < _d.Size(); i++)
    {
      Q[i].Value = (_d.Value.ToUInt64(CultureInfo.InvariantCulture) & (1UL << i)) != 0;
    }
  }
}