namespace Fluxera.Spatial.JsonNet
{
	using Newtonsoft.Json;

	public static class JsonSerializerSettingsExtensions
	{
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
