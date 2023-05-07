using System;

namespace RVsim
{
  public class PortChangingEventArgs : EventArgs
  {
    public uint NewValue { get; set; }
  }
}
