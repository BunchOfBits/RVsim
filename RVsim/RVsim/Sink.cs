using System;

namespace RVsim
{
  internal class Sink
  {
    private readonly Port _port;

    public Sink(Port port)
    {
      _port = port;

      _port.PortChanged += (s, e) => { Console.WriteLine($"{_port.Name} = {_port.Value}"); };
    }
  }
}
