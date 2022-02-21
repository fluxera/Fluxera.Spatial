namespace Fluxera.Spatial.UnitTests
{
	using FluentAssertions;
	using NUnit.Framework;

	[TestFixture]
	public class PositionTests
	{
		[Test]
		public void ShouldBeEqualWithCtorWithAltitude()
		{
			Position pos1 = new Position(3, 7, 1000);
			Position pos2 = new Position(3, 7, 1000);

			pos1.Equals(pos2).Should().BeTrue();
			(pos1 == pos2).Should().BeTrue();
		}

		[Test]
		public void ShouldBeEqualWithCtorWithAltitudeWithMinimalDeviation()
		{
			Position pos1 = new Position(3, 7, 1000.00000000001);
			Position pos2 = new Position(3, 7, 1000);

			pos1.Equals(pos2).Should().BeTrue();
			(pos1 == pos2).Should().BeTrue();
		}

		[Test]
		public void ShouldBeEqualWithCtorWithoutAltitude()
		{
			Position pos1 = new Position(3, 7);
			Position pos2 = new Position(3, 7);

			pos1.Equals(pos2).Should().BeTrue();
			(pos1 == pos2).Should().BeTrue();
		}

		[Test]
		public void ShouldBeEqualWithCtorWithoutAltitudeWithMinimalDeviation()
		{
			Position pos1 = new Position(3.00000000001, 7);
			Position pos2 = new Position(3, 7);

			pos1.Equals(pos2).Should().BeTrue();
			(pos1 == pos2).Should().BeTrue();
		}

		[Test]
		public void ShouldBeEqualWithDefaultCtor()
		{
			Position pos1 = new Position();
			Position pos2 = new Position();

			pos1.Equals(pos2).Should().BeTrue();
			(pos1 == pos2).Should().BeTrue();
		}

		[Test]
		public void ShouldNotBeEqualWithCtorWithAltitude()
		{
			Position pos1 = new Position(3, 7, 1000);
			Position pos2 = new Position(3, 7, 2000);

			pos1.Equals(pos2).Should().BeFalse();
			(pos1 == pos2).Should().BeFalse();
		}

		[Test]
		public void ShouldNotBeEqualWithCtorWithAltitudeWithMinimalDeviation()
		{
			Position pos1 = new Position(3, 7, 1000.0000000001);
			Position pos2 = new Position(3, 7, 1000);

			pos1.Equals(pos2).Should().BeFalse();
			(pos1 == pos2).Should().BeFalse();
		}

		[Test]
		public void ShouldNotBeEqualWithCtorWithoutAltitude()
		{
			Position pos1 = new Position(3, 8);
			Position pos2 = new Position(3, 7);

			pos1.Equals(pos2).Should().BeFalse();
			(pos1 == pos2).Should().BeFalse();
		}

		[Test]
		public void ShouldNotBeEqualWithCtorWithoutAltitudeWithMinimalDeviation()
		{
			Position pos1 = new Position(3.0000000001, 7);
			Position pos2 = new Position(3, 7);

			pos1.Equals(pos2).Should().BeFalse();
			(pos1 == pos2).Should().BeFalse();
		}
	}
}
