namespace Fluxera.Spatial.JsonNet
{
	using System;
	using System.Collections.Generic;
	using JetBrains.Annotations;
	using Newtonsoft.Json;
	using Newtonsoft.Json.Linq;

	/// <inheritdoc />
	[PublicAPI]
	public sealed class MultiLineStringConverter : JsonConverter<MultiLineString>
	{
		/// <inheritdoc />
		public override void WriteJson(JsonWriter writer, MultiLineString value, JsonSerializer serializer)
		{
			writer.WriteStartObject();

			writer.WritePropertyName("type");
			writer.WriteValue("MultiLineString");

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
		public override MultiLineString ReadJson(JsonReader reader, Type objectType, MultiLineString existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			if(reader.TokenType == JsonToken.StartObject)
			{
				JObject item = JObject.Load(reader);

				if(item.ContainsKey("type"))
				{
					string type = item["type"].Value<string>();
					if(type == "MultiLineString")
					{
						if(item.ContainsKey("coordinates"))
						{
							JToken jToken = item["coordinates"];
							if(jToken.Type == JTokenType.Array)
							{
								JArray outerArray = (JArray)jToken;

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

								return new MultiLineString(lineStrings);
							}
						}
					}
				}
			}

			return MultiLineString.Empty;
		}
	}
}
