namespace RVsim
{
  internal class Program
  {
    public static void Main()
    {
      var a = new Source("A");
      var b = new Source("B");
      var adder = new Adder(a.Port, b.Port);
      var _ = new Sink(adder.Sum);

      a.Set(1u);
      b.Set(4u);
    }
  }
}
