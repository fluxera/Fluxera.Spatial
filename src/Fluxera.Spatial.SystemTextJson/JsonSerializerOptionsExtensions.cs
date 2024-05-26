namespace Fluxera.Spatial.SystemTextJson
{
	using System.Text.Json;
	using JetBrains.Annotations;

	/// <summary>
	///     Extension methods for the <see cref="JsonSerializerOptions" /> type.
	/// </summary>
	/// <remarks>
	///     See: https://github.com/dotnet/docs/blob/main/docs/standard/serialization/system-text-json-converters-how-to.md
	/// </remarks>
	[PublicAPI]
	public static class JsonSerializerOptionsExtensions
	{
		/// <summary>
		///     Configures the serializer to use the spatial type converters.
		/// </summary>
		/// <param name="options"></param>
		public static void UseSpatial(this JsonSerializerOptions options)
		{
			Guard.ThrowIfNull(options);

			options.Converters.Add(new PointConverter());
			options.Converters.Add(new MultiPointConverter());
			options.Converters.Add(new LineStringConverter());
			options.Converters.Add(new MultiLineStringConverter());
			options.Converters.Add(new PolygonConverter());
			options.Converters.Add(new MultiPolygonConverter());
			options.Converters.Add(new GeometryCollectionConverter());
		}
	}
}
