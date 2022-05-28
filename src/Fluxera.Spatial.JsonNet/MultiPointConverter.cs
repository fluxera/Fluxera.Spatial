namespace Fluxera.Spatial.JsonNet
{
	using System;
	using System.Collections.Generic;
	using JetBrains.Annotations;
	using Newtonsoft.Json;
	using Newtonsoft.Json.Linq;

	/// <inheritdoc />
	[PublicAPI]
	public sealed class MultiPointConverter : JsonConverter<MultiPoint>
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
				writer.WritePosition(position);
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
							JToken coordinatesToken = item["coordinates"]!;
							if(coordinatesToken.Type == JTokenType.Array)
							{
								JArray positionsArray = (JArray)coordinatesToken;

								IList<Position> positions = new List<Position>();
								foreach(JToken token in positionsArray)
								{
									Position position = token.CreateReader().ReadPosition();
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
