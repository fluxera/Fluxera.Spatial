namespace Fluxera.Spatial.JsonNet.UnitTests
{
	using System;
	using System.Collections.Generic;
	using FluentAssertions;
	using Newtonsoft.Json;
	using NUnit.Framework;

	[TestFixture]
	public class LineStringTests : TestsBase<LineString>
	{
		[Test]
		public void ShouldDeserializeNotClosed()
		{
			IList<Point> points = new List<Point>
			{
				new Point(new Position(8.8057381, 53.0760221)),
				new Point(new Position(8.8354164, 53.0664328)),
				new Point(new Position(8.7896234, 53.0957640)),
				new Point(new Position(8.8556994, 53.1103679))
			};

			LineString expected = new LineString(points);
			LineString actual = JsonConvert.DeserializeObject<LineString>(this.GetJson("NotClosed"));

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
				new Point(new Position(8.8057381, 53.0760221)),
				new Point(new Position(8.8354164, 53.0664328)),
				new Point(new Position(8.7896234, 53.0957640)),
				new Point(new Position(8.8556994, 53.1103679))
			};

			LineString lineString = new LineString(points);

			string expected = this.GetJson("NotClosed");
			string actual = JsonConvert.SerializeObject(lineString);

			Console.WriteLine(expected);
			Console.WriteLine(actual);

			actual.Should().Be(expected);
		}
	}
}
