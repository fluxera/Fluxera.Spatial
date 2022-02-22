namespace Fluxera.Spatial.SystemTextJson
{
	using System.Text.Json;

	internal static class Utf8JsonWriterExtensions
	{
		public static void WritePosition(this Utf8JsonWriter writer, Position position)
		{
			writer.WriteStartArray();

			writer.WriteNumberValue((decimal)position.Longitude);
			writer.WriteNumberValue((decimal)position.Latitude);
			if(position.Altitude.HasValue)
			{
				writer.WriteNumberValue((decimal)position.Altitude.Value);
			}

			writer.WriteEndArray();
		}
	}
}
