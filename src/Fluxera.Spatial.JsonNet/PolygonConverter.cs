namespace Fluxera.Spatial.JsonNet
{
	using System;
	using System.Collections.Generic;
	using JetBrains.Annotations;
	using Newtonsoft.Json;
	using Newtonsoft.Json.Linq;

	/// <inheritdoc />
	[PublicAPI]
	public sealed class PolygonConverter : JsonConverter<Polygon>
	{
		/// <inheritdoc />
		public override void WriteJson(JsonWriter writer, Polygon value, JsonSerializer serializer)
		{
			writer.WriteStartObject();

			writer.WritePropertyName("type");
			writer.WriteValue("Polygon");

			writer.WritePropertyName("coordinates");
			writer.WriteStartArray();

			foreach(LineString lineString in value.Coordinates)
			{
				writer.WriteStartArray();

				foreach(Position position in lineString.Coordinates)
				{
					writer.WritePosition(position);
				}

				writer.WriteEndArray();
			}

			writer.WriteEndArray();

			writer.WriteEndObject();
		}

		/// <inheritdoc />
		public override Polygon ReadJson(JsonReader reader, Type objectType, Polygon existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			if(reader.TokenType == JsonToken.StartObject)
			{
				JObject item = JObject.Load(reader);

				if(item.TryGetValue("type", out JToken typeToken))
				{
					string type = typeToken.Value<string>();
					if(type == "Polygon")
					{
						if(item.TryGetValue("coordinates", out JToken coordinatesToken))
						{
							if(coordinatesToken.Type == JTokenType.Array)
							{
								JArray outerArray = (JArray)coordinatesToken;

								IList<LineString> lineStrings = new List<LineString>();
								foreach(JToken arrayToken in outerArray)
								{
									JArray innerArray = (JArray)arrayToken;

									IList<Position> positions = new List<Position>();
									foreach(JToken token in innerArray)
									{
										Position position = token.CreateReader().ReadPosition();
										positions.Add(position);
									}

									LineString lineString = new LineString(positions);
									lineStrings.Add(lineString);
								}

								return new Polygon(lineStrings);
							}
						}
					}
				}
			}

			return Polygon.Empty;
		}
	}
}
