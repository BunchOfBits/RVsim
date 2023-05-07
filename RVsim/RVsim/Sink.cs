using System;

namespace RVsim
{
  public class Sink
  {
    private readonly Port _port;

    public uint Value
    {
      get
      {
        return _port.Value;
      }
    }

    public Sink(Port port)
    {
      _port = port;

      _port.PortChanged += (s, e) => { Console.WriteLine($"{_port.Name} = {e.NewValue}"); };
    }
  }
}
