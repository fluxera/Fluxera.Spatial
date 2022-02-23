namespace Fluxera.Spatial.MongoDB
{
	using global::MongoDB.Bson.IO;

	internal static class BsonWriterExtensions
	{
		public static void WritePosition(this IBsonWriter writer, Position position)
		{
			writer.WriteStartArray();

			writer.WriteDouble(position.Longitude);
			writer.WriteDouble(position.Latitude);
			if(position.Altitude.HasValue)
			{
				writer.WriteDouble(position.Altitude.Value);
			}

			writer.WriteEndArray();
		}
	}
}
