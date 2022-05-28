namespace Fluxera.Spatial.JsonNet
{
	using JetBrains.Annotations;
	using Newtonsoft.Json;

	/// <summary>
	///     Extension methods for the <see cref="JsonSerializerSettings" /> type.
	/// </summary>
	[PublicAPI]
	public static class JsonSerializerSettingsExtensions
	{
		/// <summary>
		///     Configures the serializer to use the spatial type converters.
		/// </summary>
		/// <param name="settings"></param>
		public static void UseSpatial(this JsonSerializerSettings settings)
		{
			settings.Converters.Add(new PointConverter());
			settings.Converters.Add(new MultiPointConverter());
			settings.Converters.Add(new LineStringConverter());
			settings.Converters.Add(new MultiLineStringConverter());
			settings.Converters.Add(new PolygonConverter());
			settings.Converters.Add(new MultiPolygonConverter());
			settings.Converters.Add(new GeometryCollectionConverter());
		}
	}
}
