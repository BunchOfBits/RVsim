using System;

namespace RVsim
{
  public class PortChangedEventArgs : EventArgs
  {
    public uint NewValue { get; set; }
  }
}
