namespace Fluxera.Spatial.UnitTests.Geometry
{
	using System;
	using System.Collections.Generic;
	using FluentAssertions;
	using NUnit.Framework;

	[TestFixture]
	public abstract class LineStringTestsBase : TestsBase<LineString>
	{
		[Test]
		public void ShouldDeserializeNotClosed()
		{
			IList<Point> points = new List<Point>
			{
				new Point(new Position(100.0, 0.0)),
				new Point(new Position(101.0, 1.0))
			};

			LineString expected = new LineString(points);
			LineString actual = this.Deserialize("NotClosed");

			Console.WriteLine(expected);
			Console.WriteLine(actual);

			actual.Should().NotBe(LineString.Empty);
			actual.Should().Be(expected);
		}

		[Test]
		public void ShouldSerializeNotClosed()
		{
			IList<Point> points = new List<Point>
			{
				new Point(new Position(100.0, 0.0)),
				new Point(new Position(101.0, 1.0))
			};

			LineString lineString = new LineString(points);

			string expected = this.GetJson("NotClosed");
			string actual = this.Serialize(lineString);

			Console.WriteLine(expected);
			Console.WriteLine(actual);

			actual.Should().Be(expected);
		}
	}
}
