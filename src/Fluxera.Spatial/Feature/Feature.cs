//// ReSharper disable once CheckNamespace

//namespace Fluxera.Spatial
//{
//	using System.Collections.Generic;
//	using JetBrains.Annotations;

//	/// <summary>
//	///     A <see cref="Feature{T}" /> object represents a spatially bounded thing.
//	/// </summary>
//	/// <remarks>
//	///     https://datatracker.ietf.org/doc/html/rfc7946#section-3.2
//	/// </remarks>
//	[PublicAPI]
//	public struct Feature<TGeometry>
//		where TGeometry : struct, IGeometry
//	{
//		public static readonly Feature<TGeometry> Empty = new Feature<TGeometry>();

//		/// <summary>
//		///     Creates a new instance of the <see cref="Feature{T}" /> type.
//		/// </summary>
//		public Feature()
//		{
//			this.Geometry = null;
//			this.Properties = new Dictionary<string, object>();
//			this.Identifier = null;
//		}

//		/// <summary>
//		///     Creates a new instance of the <see cref="Feature{T}" /> type.
//		/// </summary>
//		/// <param name="geometry"></param>
//		/// <param name="identifier"></param>
//		public Feature(TGeometry? geometry, string? identifier = null)
//		{
//			this.Geometry = geometry;
//			this.Properties = new Dictionary<string, object>();
//			this.Identifier = identifier;
//		}

//		/// <summary>
//		///     Gets the geometry object of the feature.
//		/// </summary>
//		public TGeometry? Geometry { get; }

//		/// <summary>
//		///     Gets the properties of the feature.
//		/// </summary>
//		public IDictionary<string, object> Properties { get; }

//		/// <summary>
//		///     Gets the commonly used identifier of the feature.
//		/// </summary>
//		public string? /*int*/ Identifier { get; }

//		/// <inheritdoc />
//		public override bool Equals(object obj)
//		{
//			if(obj is not Feature<TGeometry> feature)
//			{
//				return false;
//			}

//			return this == feature;
//		}

//		/// <inheritdoc />
//		public override int GetHashCode()
//		{
//			int hashCode = base.GetHashCode();

//			hashCode = (hashCode * 397) ^ (this.Identifier != null ? this.Identifier.GetHashCode() : 0);
//			hashCode = (hashCode * 397) ^ (this.Geometry != null ? this.Geometry.GetHashCode() : 0);
//			hashCode = (hashCode * 397) ^ (this.Properties != null ? this.Properties.GetHashCode() : 0);

//			return hashCode;
//		}

//		/// <inheritdoc />
//		public override string ToString()
//		{
//			return base.ToString();
//		}

//		/// <summary>
//		///     Determines whether the specified line strings are considered equal.
//		/// </summary>
//		public static bool operator ==(Feature<TGeometry> left, Feature<TGeometry> right)
//		{
//			// TODO
//			return false;
//		}

//		/// <summary>
//		///     Determines whether the specified line strings are considered not equal.
//		/// </summary>
//		public static bool operator !=(Feature<TGeometry> left, Feature<TGeometry> right)
//		{
//			return !(left == right);
//		}
//	}
//}


