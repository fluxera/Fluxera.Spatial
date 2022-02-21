namespace Fluxera.Spatial.UnitTests
{
	using System.Collections.Generic;
	using FluentAssertions;
	using NUnit.Framework;

	[TestFixture]
	public class LineStringTests
	{
		[Test]
		public void ShouldBeLinearRing()
		{
			IList<Point> points = new List<Point>
			{
				new Point(new Position(8.8057381, 53.0760221)),
				new Point(new Position(8.8354164, 53.0664328)),
				new Point(new Position(8.7896234, 53.0957640)),
				new Point(new Position(8.8556994, 53.1103679)),
				new Point(new Position(8.8057381, 53.0760221))
			};

			LineString lineString = new LineString(points);

			lineString.IsLinearRing.Should().BeTrue();
		}
	}
}
