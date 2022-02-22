namespace Fluxera.Spatial.UnitTests.Geometry
{
	using System;
	using System.Collections.Generic;
	using FluentAssertions;
	using NUnit.Framework;

	public abstract class MultiLineStringTestsBase : TestsBase<MultiLineString>
	{
		[Test]
		public void ShouldDeserialize()
		{
			LineString lineString0 = new LineString(new List<Point>
			{
				new Point(new Position(8.8057381, 53.0760221)),
				new Point(new Position(8.8354164, 53.0664328)),
				new Point(new Position(8.7896234, 53.0957640)),
				new Point(new Position(8.8556994, 53.1103679))
			});

			LineString lineString1 = new LineString(new List<Point>
			{
				new Point(new Position(8.8354164, 53.0664328)),
				new Point(new Position(8.7896234, 53.0957640)),
				new Point(new Position(8.8556994, 53.1103679)),
				new Point(new Position(8.8057381, 53.0760221))
			});

			MultiLineString expected = new MultiLineString(lineString0, lineString1);
			MultiLineString actual = this.Deserialize("Default");

			Console.WriteLine(expected);
			Console.WriteLine(actual);

			actual.Should().NotBe(MultiLineString.Empty);
			actual.Should().Be(expected);
		}

		[Test]
		public void ShouldSerialize()
		{
			LineString lineString0 = new LineString(new List<Point>
			{
				new Point(new Position(8.8057381, 53.0760221)),
				new Point(new Position(8.8354164, 53.0664328)),
				new Point(new Position(8.7896234, 53.0957640)),
				new Point(new Position(8.8556994, 53.1103679))
			});

			LineString lineString1 = new LineString(new List<Point>
			{
				new Point(new Position(8.8354164, 53.0664328)),
				new Point(new Position(8.7896234, 53.0957640)),
				new Point(new Position(8.8556994, 53.1103679)),
				new Point(new Position(8.8057381, 53.0760221))
			});

			MultiLineString multiLineString = new MultiLineString(lineString0, lineString1);

			string expected = this.GetJson("Default");
			string actual = this.Serialize(multiLineString);

			Console.WriteLine(expected);
			Console.WriteLine(actual);

			actual.Should().Be(expected);
		}
	}
}
