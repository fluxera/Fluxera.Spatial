namespace Fluxera.Spatial.UnitTests.Geometry
{
	using System;
	using System.Collections.Generic;
	using FluentAssertions;
	using NUnit.Framework;

	public abstract class MultiPointTestsBase : TestsBase<MultiPoint>
	{
		[Test]
		public void ShouldDeserialize()
		{
			IList<Point> points = new List<Point>
			{
				new Point(new Position(100.0, 0.0)),
				new Point(new Position(101.0, 1.0))
			};

			MultiPoint expected = new MultiPoint(points);
			MultiPoint actual = this.Deserialize("Default");

			Console.WriteLine(expected);
			Console.WriteLine(actual);

			actual.Should().NotBe(MultiPoint.Empty);
			actual.Should().Be(expected);
		}

		[Test]
		public void ShouldSerialize()
		{
			IList<Point> points = new List<Point>
			{
				new Point(new Position(100.0, 0.0)),
				new Point(new Position(101.0, 1.0))
			};

			MultiPoint multiPoint = new MultiPoint(points);

			string expected = this.GetJson("Default");
			string actual = this.Serialize(multiPoint);

			Console.WriteLine(expected);
			Console.WriteLine(actual);

			actual.Should().Be(expected);
		}
	}
}
