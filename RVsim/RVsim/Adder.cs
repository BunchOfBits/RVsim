namespace RVsim
{
  internal class Adder
  {
    private readonly Port _a;
    private readonly Port _b;

    public Adder(Port a, Port b)
    {
      _a = a;
      _b = b;

      _a.PortChanged += Update;
      _b.PortChanged += Update;
    }

    public Port Sum { get; } = new Port(nameof(Sum));

    private void Update(object sender, PortChangedEventArgs args)
    {
      Sum.Value = _a.Value + _b.Value;
    }
  }
}
