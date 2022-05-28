namespace Fluxera.Spatial.JsonNet
{
	using System;
	using System.Collections.Generic;
	using JetBrains.Annotations;
	using Newtonsoft.Json;
	using Newtonsoft.Json.Linq;

	/// <inheritdoc />
	[PublicAPI]
	public sealed class LineStringConverter : JsonConverter<LineString>
	{
		/// <inheritdoc />
		public override void WriteJson(JsonWriter writer, LineString value, JsonSerializer serializer)
		{
			writer.WriteStartObject();

			writer.WritePropertyName("type");
			writer.WriteValue("LineString");

			writer.WritePropertyName("coordinates");
			writer.WriteStartArray();

			foreach(Position position in value.Coordinates)
			{
				writer.WritePosition(position);
			}

			writer.WriteEndArray();

			writer.WriteEndObject();
		}

		/// <inheritdoc />
		public override LineString ReadJson(JsonReader reader, Type objectType, LineString existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			if(reader.TokenType == JsonToken.StartObject)
			{
				JObject item = JObject.Load(reader);

				if(item.ContainsKey("type"))
				{
					string type = item["type"]!.Value<string>()!;
					if(type == "LineString")
					{
						if(item.ContainsKey("coordinates"))
						{
							JToken jToken = item["coordinates"]!;
							if(jToken.Type == JTokenType.Array)
							{
								JArray jArray = (JArray)jToken;

								IList<Position> positions = new List<Position>();
								foreach(JToken token in jArray)
								{
									Position position = token.CreateReader().ReadPosition();
									positions.Add(position);
								}

								return new LineString(positions);
							}
						}
					}
				}
			}

			return LineString.Empty;
		}
	}
}
