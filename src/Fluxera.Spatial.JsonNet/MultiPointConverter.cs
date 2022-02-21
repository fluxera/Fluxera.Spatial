namespace Fluxera.Spatial.JsonNet
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Newtonsoft.Json;
	using Newtonsoft.Json.Linq;

	internal sealed class MultiPointConverter : JsonConverter<MultiPoint>
	{
		/// <inheritdoc />
		public override void WriteJson(JsonWriter writer, MultiPoint value, JsonSerializer serializer)
		{
			writer.WriteStartObject();

			writer.WritePropertyName("type");
			writer.WriteValue("MultiPoint");

			writer.WritePropertyName("coordinates");
			writer.WriteStartArray();

			foreach(Position position in value.Coordinates)
			{
				serializer.Converters
					.Single(x => x is PositionConverter)
					.WriteJson(writer, position, serializer);
			}

			writer.WriteEndArray();

			writer.WriteEndObject();
		}

		/// <inheritdoc />
		public override MultiPoint ReadJson(JsonReader reader, Type objectType, MultiPoint existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			if(reader.TokenType == JsonToken.StartObject)
			{
				JObject item = JObject.Load(reader);

				if(item.ContainsKey("type"))
				{
					string type = item["type"]!.Value<string>()!;
					if(type == "MultiPoint")
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
									Position position = (Position)serializer.Converters
										.Single(x => x is PositionConverter)
										.ReadJson(token.CreateReader(), typeof(Position), Position.Empty, serializer)!;

									positions.Add(position);
								}

								return new MultiPoint(positions);
							}
						}
					}
				}
			}

			return MultiPoint.Empty;
		}
	}
}
