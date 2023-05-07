﻿using System;

namespace RVsim
{
  public class Source
  {
    public Source(string name)
    {
      Port = new Port(name);
    }
    public Port Port { get; }

    public void Set(uint p)
    {
      Console.WriteLine($"{Port.Name} = {p}");

      Port.Value = p;
    }
  }
}
