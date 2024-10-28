using System;
using System.Collections.Generic;
using System.Linq;

namespace PicoSim;

internal class Scheduler
{
  private readonly Dictionary<PortBase, object> _init = [];
  private Dictionary<PortBase, object> _now = [];
  private Dictionary<PortBase, object> _future = [];

  public static Scheduler Instance { get; private set; } = new Scheduler();

  private Scheduler()
  {
  }

  internal void Initialize<T>(Port<T> port)
    where T : unmanaged, IComparable<T>, IConvertible
  {
    _init[port] = port.Value;
  }

  internal void Schedule<T>(Port<T> port, T value)
    where T : unmanaged, IComparable<T>, IConvertible
  {
    // Do NOT check old/new value equality here and skip adding in case of
    // equality in a vain attempt to speed up simulation !!!
    //
    // "Now" may still contain assignments for "port" further down the line,
    // which may need to be overriden again with the "new" value
    //
    // Consider for example a 2-bit ripple carry adder, with two inputs, A and B,
    // sum output S and carry output C. The result should be 00 with a carry.
    // At T = 0, assign 01 to A, and when all consequences have been processed,
    // assign 11 to B at T = 2. Assume al ports are 0 at startup. Index 0 is
    // the least significant bit.
    //
    //     T = 0        T = 1        T = 2        T = 3        T = 4
    // A[0] := 1 => S[0] := 1
    // A[1] := 0 => -
    //                           B[0] := 1 => S[0] := 0
    //                                        C[0] := 1 => S[1] := 0
    //                                                     C[1] := 1
    //                           B[1] := 1 => S[1] := 1
    //
    // If, at T = 3, you would decide not to schedule S[1] := 0 for T = 4 as a
    // consequence of C[0] := 1 because S[1] is still 0 at that moment, you
    // would ignore S[1] := 1 at T = 3, and would end up with 10 with a carry
    // as a result, instead of 00 with a carry
    _future[port] = value;
  }

  internal void Run()
  {
    if (_init.Count > 0)
    {
      Console.WriteLine($"{nameof(Initialize)}: {(string.Join(", ", _init.Select(kvp => $"{kvp.Key.Name} := {kvp.Value}")))}");

      foreach (var kvp in _init)
      {
        kvp.Key.Set(kvp.Value);
      }

      _init.Clear();

      Console.WriteLine($"{nameof(Initialize)}: ---");
    }

    do
    {
      (_now, _future) = (_future, _now);

      Console.WriteLine($"{nameof(Run)}: {(string.Join(", ", _now.Select(kvp => $"{kvp.Key.Name} := {kvp.Value}")))}");

      foreach (var kvp in _now)
      {
        kvp.Key.Set(kvp.Value);
      }

      _now.Clear();
    }
    while (_future.Count > 0);

    Console.WriteLine($"{nameof(Run)}: ---");
  }
}
