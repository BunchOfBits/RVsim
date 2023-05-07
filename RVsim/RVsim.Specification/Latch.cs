using NUnit.Framework;

namespace RVsim.Specification
{
  public class Latch
  {
    private Source _d;
    private Source _e;
    private RVsim.Latch _dut;
    private Sink _q;
    private Sink _qn;

    [SetUp]
    public void Setup()
    {
      _d = new Source("D");
      _e = new Source("E");

      _dut = new RVsim.Latch("L1", _d.Port, _e.Port);

      _q = new Sink(_dut.Q);
      _qn = new Sink(_dut.Qn);
    }

    [Test]
    public void CheckThatLatchHoldsWhenDisabled()
    {
      _d.Set(1u);
      _d.Set(0u);

      Assert.That(_q.Value, Is.EqualTo(0));
      Assert.That(_qn.Value, Is.Not.EqualTo(0));
    }

    [Test]
    public void CheckThatLatchFollowsWhenEnabled()
    {
      _e.Set(1u);
      _d.Set(1u);

      Assert.That(_q.Value, Is.Not.EqualTo(0));
      Assert.That(_qn.Value, Is.EqualTo(0));

      _d.Set(0u);

      Assert.That(_q.Value, Is.EqualTo(0));
      Assert.That(_qn.Value, Is.Not.EqualTo(0));
    }

    [Test]
    public void CheckThatLatchHoldsLastValueBeforeDisable()
    {
      _e.Set(1u);
      _d.Set(1u);
      _e.Set(0u);
      _d.Set(0u);

      Assert.That(_q.Value, Is.Not.EqualTo(0));
      Assert.That(_qn.Value, Is.EqualTo(0));
    }
  }
}
