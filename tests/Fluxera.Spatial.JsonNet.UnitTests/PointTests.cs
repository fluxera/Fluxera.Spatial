namespace Fluxera.Spatial.JsonNet.UnitTests
{
	using System;
	using FluentAssertions;
	using Newtonsoft.Json;
	using NUnit.Framework;

	[TestFixture]
	public class PointTests : TestsBase<Point>
	{
		[Test]
		public void ShouldDeserialize()
		{
			Point expected = new Point(new Position(8.8057381, 53.0760221));
			Point actual = JsonConvert.DeserializeObject<Point>(this.GetJson("WithoutAltitude"));

			Console.WriteLine(expected);
			Console.WriteLine(actual);

			actual.Should().NotBe(Point.Empty);
			actual.Should().Be(expected);
		}

		[Test]
		public void ShouldDeserializeWithAltitude()
		{
			Point expected = new Point(new Position(8.8057381, 53.0760221, 105.5));
			Point actual = JsonConvert.DeserializeObject<Point>(this.GetJson("WithAltitude"));

			Console.WriteLine(expected);
			Console.WriteLine(actual);

			actual.Should().NotBe(Point.Empty);
			actual.Should().Be(expected);
		}

		[Test]
		public void ShouldSerialize()
		{
			Point point = new Point(new Position(8.8057381, 53.0760221));

			string expected = this.GetJson("WithoutAltitude");
			string actual = JsonConvert.SerializeObject(point);

			Console.WriteLine(expected);
			Console.WriteLine(actual);

			actual.Should().Be(expected);
		}

		[Test]
		public void ShouldSerializeWithAltitude()
		{
			Point point = new Point(new Position(8.8057381, 53.0760221, 105.5));

			string expected = this.GetJson("WithAltitude");
			string actual = JsonConvert.SerializeObject(point);

			Console.WriteLine(expected);
			Console.WriteLine(actual);

			actual.Should().Be(expected);
		}
	}
}
