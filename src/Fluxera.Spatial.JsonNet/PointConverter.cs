namespace Fluxera.Spatial.JsonNet
{
	using System;
	using System.Linq;
	using Newtonsoft.Json;
	using Newtonsoft.Json.Linq;

	internal sealed class PointConverter : JsonConverter<Point>
	{
		/// <inheritdoc />
		public override void WriteJson(JsonWriter writer, Point value, JsonSerializer serializer)
		{
			writer.WriteStartObject();

			writer.WritePropertyName("type");
			writer.WriteValue("Point");

			writer.WritePropertyName("coordinates");
			serializer.Converters
				.Single(x => x is PositionConverter)
				.WriteJson(writer, value.Coordinates, serializer);

			writer.WriteEndObject();
		}

		/// <inheritdoc />
		public override Point ReadJson(JsonReader reader, Type objectType, Point existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			if(reader.TokenType == JsonToken.StartObject)
			{
				JObject item = JObject.Load(reader);

				if(item.ContainsKey("type"))
				{
					string type = item["type"]!.Value<string>()!;
					if(type == "Point")
					{
						if(item.ContainsKey("coordinates"))
						{
							JToken jToken = item["coordinates"]!;

							Position position = (Position)serializer.Converters
								.Single(x => x is PositionConverter)
								.ReadJson(jToken.CreateReader(), typeof(Position), Position.Empty, serializer)!;

							return new Point(position);
						}
					}
				}
			}

			return Point.Empty;
		}
	}
}
