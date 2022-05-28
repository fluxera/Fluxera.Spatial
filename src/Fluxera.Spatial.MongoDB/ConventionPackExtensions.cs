namespace Fluxera.Spatial.MongoDB
{
	using global::MongoDB.Bson.Serialization.Conventions;
	using JetBrains.Annotations;

	/// <summary>
	///     Extension methods for the <see cref="ConventionPack" /> type.
	/// </summary>
	[PublicAPI]
	public static class ConventionPackExtensions
	{
		/// <summary>
		///     Configures the serializer to use the spatial serializers.
		/// </summary>
		/// <param name="pack"></param>
		/// <returns></returns>
		public static ConventionPack UseSpatial(this ConventionPack pack)
		{
			pack.Add(new SpatialConvention());

			return pack;
		}
	}
}
