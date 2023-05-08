using System;

namespace PicoSim
{
  public class PortChangingEventArgs<T> : EventArgs
  {
    public T NewValue { get; set; }
  }
}
