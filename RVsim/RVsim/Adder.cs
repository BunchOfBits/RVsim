namespace RVsim
{
  internal class Adder
  {
    private readonly Port _a;
    private readonly Port _b;

    public Adder(string name, Port a, Port b)
    {
      _a = a;
      _b = b;

      Sum = new Port(name, nameof(Sum));

      _a.PortChanged += Update;
      _b.PortChanged += Update;
    }

    public Port Sum { get; }

    private void Update(object sender, PortChangedEventArgs args)
    {
      Sum.Value = _a.Value + _b.Value;
    }
  }
}
