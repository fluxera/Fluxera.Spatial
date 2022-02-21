namespace Fluxera.Spatial.JsonNet
{
	using Newtonsoft.Json;

	public static class JsonSerializerSettingsExtensions
	{
		public static JsonSerializerSettings UseSpatial(this JsonSerializerSettings settings)
		{
			settings.Converters.Add(new PositionConverter());
			settings.Converters.Add(new PointConverter());
			settings.Converters.Add(new MultiPointConverter());
			settings.Converters.Add(new LineStringConverter());
			settings.Converters.Add(new MultiLineStringConverter());

			return settings;
		}
	}
}
