﻿using System;

namespace PicoSim;

public class Sink<T>
  where T : unmanaged, IComparable<T>, IConvertible
{
  private readonly Port<T> _port;

  public T Value
  {
    get
    {
      return _port.Value;
    }
  }

  public Sink(Port<T> port)
  {
    _port = port;
  }
}
