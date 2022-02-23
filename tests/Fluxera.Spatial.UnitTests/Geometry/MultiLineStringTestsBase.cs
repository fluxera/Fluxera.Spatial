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
				new Point(new Position(100.0, 0.0)),
				new Point(new Position(101.0, 1.0))
			});

			LineString lineString1 = new LineString(new List<Point>
			{
				new Point(new Position(102.0, 2.0)),
				new Point(new Position(103.0, 3.0))
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
				new Point(new Position(100.0, 0.0)),
				new Point(new Position(101.0, 1.0))
			});

			LineString lineString1 = new LineString(new List<Point>
			{
				new Point(new Position(102.0, 2.0)),
				new Point(new Position(103.0, 3.0))
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
