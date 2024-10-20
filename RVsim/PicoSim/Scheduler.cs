using System;
using System.Collections.Generic;
using System.Linq;

namespace PicoSim
{
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
      where T : unmanaged, IComparable<T>
    {
      _init[port] = port.Value;
    }

    internal void Schedule<T>(Port<T> port, T value)
      where T : unmanaged, IComparable<T>
    {
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
}