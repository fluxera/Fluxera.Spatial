namespace Fluxera.Spatial.UnitTests.Geometry
{
	using System;
	using System.Collections.Generic;
	using FluentAssertions;
	using NUnit.Framework;

	public abstract class GeometryCollectionTestsBase : TestsBase<GeometryCollection>
	{
		[Test]
		public void ShouldDeserialize()
		{
			Point point = new Point(new Position(100.0, 0.0));
			LineString lineString = new LineString(new List<Position>
			{
				new Position(101.0, 0.0),
				new Position(102.0, 1.0)
			});

			GeometryCollection expected = new GeometryCollection(point, lineString);
			GeometryCollection actual = this.Deserialize("Default");

			Console.WriteLine(expected);
			Console.WriteLine(actual);

			actual.Should().NotBe(Point.Empty);
			actual.Should().Be(expected);
		}

		[Test]
		public void ShouldSerialize()
		{
			Point point = new Point(new Position(100.0, 0.0));
			LineString lineString = new LineString(new List<Position>
			{
				new Position(101.0, 0.0),
				new Position(102.0, 1.0)
			});

			GeometryCollection collection = new GeometryCollection(point, lineString);

			string expected = this.GetJson("Default");
			string actual = this.Serialize(collection);

			Console.WriteLine(expected);
			Console.WriteLine(actual);

			actual.Should().Be(expected);
		}
	}
}
