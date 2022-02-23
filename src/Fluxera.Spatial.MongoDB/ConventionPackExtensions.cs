namespace Fluxera.Spatial.MongoDB
{
	using global::MongoDB.Bson.Serialization.Conventions;
	using JetBrains.Annotations;

	[PublicAPI]
	public static class ConventionPackExtensions
	{
		public static ConventionPack UseSpatial(this ConventionPack pack)
		{
			pack.Add(new SpatialConvention());

			return pack;
		}
	}
}
