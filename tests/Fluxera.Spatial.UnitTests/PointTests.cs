namespace Fluxera.Spatial.UnitTests
{
	using System;
	using NUnit.Framework;

	[TestFixture]
	public class PointTests
	{
		[Test]
		public void Should()
		{
			Point point = new Point(new Position(4, 4));

			Console.WriteLine(point.GetHashCode());
			Console.WriteLine(point);
		}
	}
}
