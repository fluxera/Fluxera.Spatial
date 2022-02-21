namespace Fluxera.Spatial.UnitTests
{
	using System;
	using NUnit.Framework;

	[TestFixture]
	public class MultiPointTests
	{
		[Test]
		public void Should()
		{
			MultiPoint multiPoint = new MultiPoint(new Position[]
			{
				new Position(4, 4),
				new Position(5, 5),
			});

			Console.WriteLine(multiPoint.GetHashCode());
			Console.WriteLine(multiPoint);
		}
	}
}
