namespace Fluxera.Spatial.UnitTests.Geometry
{
	using System;
	using System.Collections.Generic;
	using FluentAssertions;
	using NUnit.Framework;

	public abstract class MultiPolygonTestsBase : TestsBase<MultiPolygon>
	{
		[Test]
		public void ShouldDeserialize()
		{
			Polygon polygon0 = new Polygon(new LineString(new List<Position>
			{
				new Position(102.0, 2.0),
				new Position(103.0, 2.0),
				new Position(103.0, 3.0),
				new Position(102.0, 3.0),
				new Position(102.0, 2.0)
			}));

			Polygon polygon1 = new Polygon(new List<LineString>
			{
				new LineString(new List<Position>
				{
					new Position(100.0, 0.0),
					new Position(101.0, 0.0),
					new Position(101.0, 1.0),
					new Position(100.0, 1.0),
					new Position(100.0, 0.0)
				}),
				new LineString(new List<Position>
				{
					new Position(108.0, 8.0),
					new Position(108.0, 2.0),
					new Position(102.0, 2.0),
					new Position(102.0, 8.0),
					new Position(108.0, 8.0)
				})
			});

			MultiPolygon expected = new MultiPolygon(polygon0, polygon1);
			MultiPolygon actual = this.Deserialize("Default");

			Console.WriteLine(expected);
			Console.WriteLine(actual);

			actual.Should().NotBe(MultiPolygon.Empty);
			actual.Should().Be(expected);
		}

		[Test]
		public void ShouldSerialize()
		{
			Polygon polygon0 = new Polygon(new LineString(new List<Position>
			{
				new Position(102.0, 2.0),
				new Position(103.0, 2.0),
				new Position(103.0, 3.0),
				new Position(102.0, 3.0),
				new Position(102.0, 2.0)
			}));

			Polygon polygon1 = new Polygon(new List<LineString>
			{
				new LineString(new List<Position>
				{
					new Position(100.0, 0.0),
					new Position(101.0, 0.0),
					new Position(101.0, 1.0),
					new Position(100.0, 1.0),
					new Position(100.0, 0.0)
				}),
				new LineString(new List<Position>
				{
					new Position(108.0, 8.0),
					new Position(108.0, 2.0),
					new Position(102.0, 2.0),
					new Position(102.0, 8.0),
					new Position(108.0, 8.0)
				})
			});

			MultiPolygon multiPolygon = new MultiPolygon(polygon0, polygon1);

			string expected = this.GetJson("Default");
			string actual = this.Serialize(multiPolygon);

			Console.WriteLine(expected);
			Console.WriteLine(actual);

			actual.Should().Be(expected);
		}
	}
}
