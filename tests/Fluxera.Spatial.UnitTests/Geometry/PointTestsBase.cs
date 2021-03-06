namespace Fluxera.Spatial.UnitTests.Geometry
{
	using System;
	using FluentAssertions;
	using NUnit.Framework;

	public abstract class PointTestsBase : TestsBase<Point>
	{
		[Test]
		public void ShouldDeserialize()
		{
			Point expected = new Point(new Position(100.0, 0.0));
			Point actual = this.Deserialize("WithoutAltitude");

			Console.WriteLine(expected);
			Console.WriteLine(actual);

			actual.Should().NotBe(Point.Empty);
			actual.Should().Be(expected);
		}

		[Test]
		public void ShouldDeserializeWithAltitude()
		{
			Point expected = new Point(new Position(100.0, 0.0, 200.0));
			Point actual = this.Deserialize("WithAltitude");

			Console.WriteLine(expected);
			Console.WriteLine(actual);

			actual.Should().NotBe(Point.Empty);
			actual.Should().Be(expected);
		}

		[Test]
		public void ShouldSerialize()
		{
			Point point = new Point(new Position(100.0, 0.0));

			string expected = this.GetJson("WithoutAltitude");
			string actual = this.Serialize(point);

			Console.WriteLine(expected);
			Console.WriteLine(actual);

			actual.Should().Be(expected);
		}

		[Test]
		public void ShouldSerializeWithAltitude()
		{
			Point point = new Point(new Position(100.0, 0.0, 200.0));

			string expected = this.GetJson("WithAltitude");
			string actual = this.Serialize(point);

			Console.WriteLine(expected);
			Console.WriteLine(actual);

			actual.Should().Be(expected);
		}
	}
}
