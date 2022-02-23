namespace Fluxera.Spatial.SystemTextJson
{
	using System.Text.Json;
	using JetBrains.Annotations;

	/// <summary>
	///     See: https://github.com/dotnet/docs/blob/main/docs/standard/serialization/system-text-json-converters-how-to.md
	/// </summary>
	[PublicAPI]
	public static class JsonSerializerOptionsExtensions
	{
		public static void UseSpatial(this JsonSerializerOptions options)
		{
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
