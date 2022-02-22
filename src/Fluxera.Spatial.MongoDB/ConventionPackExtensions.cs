namespace Fluxera.Spatial.MongoDB
{
	using System;
	using global::MongoDB.Bson.IO;
	using global::MongoDB.Bson.Serialization;
	using global::MongoDB.Bson.Serialization.Conventions;
	using global::MongoDB.Bson.Serialization.Serializers;
	using JetBrains.Annotations;

	[PublicAPI]
	public static class ConventionPackExtensions
	{
		public static ConventionPack UseSpatial(this ConventionPack pack)
		{
			pack.Add(new SpatialConvention());

			return pack;
		}
	}

	[PublicAPI]
	public class SpatialConvention : ConventionBase, IMemberMapConvention
	{
		/// <inheritdoc />
		public void Apply(BsonMemberMap memberMap)
		{
			Type originalMemberType = memberMap.MemberType;
			Type memberType = Nullable.GetUnderlyingType(originalMemberType) ?? originalMemberType;

			IBsonSerializer serializer = null;

			if(memberType == typeof(Point))
			{
				serializer = new PointSerializer();
			}
			else if(memberType == typeof(MultiPoint))
			{
				serializer = new MultiPointSerializer();
			}
			else
			{
				throw new ArgumentOutOfRangeException();
			}

			memberMap.SetSerializer(serializer);
		}
	}

	public sealed class MultiPointSerializer : SerializerBase<MultiPoint>
	{
		/// <inheritdoc />
		public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, MultiPoint value)
		{
			base.Serialize(context, args, value);
		}

		/// <inheritdoc />
		public override MultiPoint Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
		{
			return base.Deserialize(context, args);
		}
	}

	public sealed class PointSerializer : SerializerBase<Point>
	{
		/// <inheritdoc />
		public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Point value)
		{
			IBsonWriter writer = context.Writer;

			writer.WriteStartDocument();

			writer.WriteName("type");
			writer.WriteString("Point");

			writer.WriteName("coordinates");
			writer.WritePosition(value.Coordinates);

			writer.WriteEndDocument();
		}

		/// <inheritdoc />
		public override Point Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
		{
			return base.Deserialize(context, args);
		}
	}
}
