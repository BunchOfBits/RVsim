using System;

namespace RVsim
{
  public class PortChangingEventArgs<T> : EventArgs
  {
    public T NewValue { get; set; }
  }
}
