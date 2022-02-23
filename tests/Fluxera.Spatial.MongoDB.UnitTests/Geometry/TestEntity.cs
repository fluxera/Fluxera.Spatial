namespace Fluxera.Spatial.MongoDB.UnitTests.Geometry
{
	public class TestEntity<T> where T : struct, IGeometry
	{
		public TestEntity(T property)
		{
			this.Property = property;
		}

		public T Property { get; }
	}
}
