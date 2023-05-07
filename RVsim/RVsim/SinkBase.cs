namespace RVsim
{
  public abstract class SinkBase
  {
    private readonly Port _port;

    public uint Value
    {
      get
      {
        return _port.Value;
      }
    }

    public SinkBase(Port port)
    {
      _port = port;
    }
  }
}
