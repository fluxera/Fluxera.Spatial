namespace Fluxera.Spatial.JsonNet
{
	using System;
	using JetBrains.Annotations;
	using Newtonsoft.Json;
	using Newtonsoft.Json.Linq;

	/// <inheritdoc />
	[PublicAPI]
	public sealed class PointConverter : JsonConverter<Point>
	{
		/// <inheritdoc />
		public override void WriteJson(JsonWriter writer, Point value, JsonSerializer serializer)
		{
			writer.WriteStartObject();

			writer.WritePropertyName("type");
			writer.WriteValue("Point");

			writer.WritePropertyName("coordinates");
			writer.WritePosition(value.Coordinates);

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
					string type = item["type"].Value<string>();
					if(type == "Point")
					{
						if(item.ContainsKey("coordinates"))
						{
							JToken coordinates = item["coordinates"];

							Position position = coordinates.CreateReader().ReadPosition();
							return new Point(position);
						}
					}
				}
			}

			return Point.Empty;
		}
	}
}
