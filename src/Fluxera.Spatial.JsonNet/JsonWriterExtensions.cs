namespace Fluxera.Spatial.JsonNet
{
	using Newtonsoft.Json;

	internal static class JsonWriterExtensions
	{
		public static void WritePosition(this JsonWriter writer, Position position)
		{
			writer.WriteStartArray();

			writer.WriteValue(position.Longitude);
			writer.WriteValue(position.Latitude);
			if(position.Altitude.HasValue)
			{
				writer.WriteValue(position.Altitude.Value);
			}

			writer.WriteEndArray();
		}
	}
}
