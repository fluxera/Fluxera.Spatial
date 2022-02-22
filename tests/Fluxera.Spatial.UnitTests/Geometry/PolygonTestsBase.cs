namespace Fluxera.Spatial.UnitTests.Geometry
{
	using System;
	using System.Collections.Generic;
	using FluentAssertions;
	using NUnit.Framework;

	public abstract class PolygonTestsBase : TestsBase<Polygon>
	{
		[Test]
		public void ShouldDeserializeNoHoles()
		{
			Polygon expected = new Polygon(new LineString(new List<Position>
			{
				new Position(100.0, 0.0),
				new Position(101.0, 0.0),
				new Position(101.0, 1.0),
				new Position(100.0, 1.0),
				new Position(100.0, 0.0)
			}));
			Polygon actual = this.Deserialize("NoHoles");

			Console.WriteLine(expected);
			Console.WriteLine(actual);

			actual.Should().NotBe(Polygon.Empty);
			actual.Should().Be(expected);
		}

		[Test]
		public void ShouldDeserializeWithHoles()
		{
			Polygon expected = new Polygon(new List<LineString>
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
					new Position(100.8, 0.8),
					new Position(100.8, 0.2),
					new Position(100.2, 0.2),
					new Position(100.2, 0.8),
					new Position(100.8, 0.8)
				})
			});
			Polygon actual = this.Deserialize("WithHoles");

			Console.WriteLine(expected);
			Console.WriteLine(actual);

			actual.Should().NotBe(Polygon.Empty);
			actual.Should().Be(expected);
		}

		[Test]
		public void ShouldSerializeNoHoles()
		{
			Polygon polygon = new Polygon(new LineString(new List<Position>
			{
				new Position(100.0, 0.0),
				new Position(101.0, 0.0),
				new Position(101.0, 1.0),
				new Position(100.0, 1.0),
				new Position(100.0, 0.0),
			}));

			string expected = this.GetJson("NoHoles");
			string actual = this.Serialize(polygon);

			Console.WriteLine(expected);
			Console.WriteLine(actual);

			actual.Should().Be(expected);
		}

		[Test]
		public void ShouldSerializeWithHoles()
		{
			Polygon polygon = new Polygon(new List<LineString>
			{
				new LineString(new List<Position>
				{
					new Position(100.0, 0.0),
					new Position(101.0, 0.0),
					new Position(101.0, 1.0),
					new Position(100.0, 1.0),
					new Position(100.0, 0.0),
				}),
				new LineString(new List<Position>
				{
					new Position(100.8, 0.8),
					new Position(100.8, 0.2),
					new Position(100.2, 0.2),
					new Position(100.2, 0.8),
					new Position(100.8, 0.8),
				}),
			});

			string expected = this.GetJson("WithHoles");
			string actual = this.Serialize(polygon);

			Console.WriteLine(expected);
			Console.WriteLine(actual);

			actual.Should().Be(expected);
		}
	}
}
