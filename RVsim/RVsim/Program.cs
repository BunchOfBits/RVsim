namespace RVsim
{
  internal class Program
  {
    public static void Main()
    {
      var a = new Source("A");
      var b = new Source("B");

      var adder = new Adder("A1", a.Port, b.Port);

      var sum = new Sink(adder.Sum);

      a.Set(1u);
      b.Set(4u);

      var d = new Source("D");
      var e = new Source("E");

      var latch = new Latch("L1", d.Port, e.Port);

      var q = new Sink(latch.Q);
      var qn = new Sink(latch.Qn);

      d.Set(1u);
      d.Set(0u);
      e.Set(1u);
      d.Set(1u);
      e.Set(0u);
      d.Set(1u);
      d.Set(0u);
    }
  }
}
