namespace Fluxera.Spatial.JsonNet.UnitTests
{
	using System;
	using System.Collections.Generic;
	using FluentAssertions;
	using Newtonsoft.Json;
	using NUnit.Framework;

	[TestFixture]
	public class MultiPointTests : TestsBase<MultiPoint>
	{
		[Test]
		public void ShouldDeserialize()
		{
			IList<Point> points = new List<Point>
			{
				new Point(new Position(8.8057381, 53.0760221)),
				new Point(new Position(8.8354164, 53.0664328)),
				new Point(new Position(8.7896234, 53.0957640)),
				new Point(new Position(8.8556994, 53.1103679))
			};

			MultiPoint expected = new MultiPoint(points);
			MultiPoint actual = JsonConvert.DeserializeObject<MultiPoint>(this.GetJson("Default"));

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
				new Point(new Position(8.8057381, 53.0760221)),
				new Point(new Position(8.8354164, 53.0664328)),
				new Point(new Position(8.7896234, 53.0957640)),
				new Point(new Position(8.8556994, 53.1103679))
			};

			MultiPoint multiPoint = new MultiPoint(points);

			string expected = this.GetJson("Default");
			string actual = JsonConvert.SerializeObject(multiPoint);

			Console.WriteLine(expected);
			Console.WriteLine(actual);

			actual.Should().Be(expected);
		}
	}
}
