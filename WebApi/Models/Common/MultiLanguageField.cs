using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using WebApi.Helpers;

namespace WebApi.Models.Common
{
    public class MultiLanguageField
    {
        protected bool Equals(MultiLanguageField other)
        {
            return this.Uz == other.Uz && this.Ru == other.Ru && this.Eng == other.Eng && this.Cyrl == other.Cyrl;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;

            return this.Equals((MultiLanguageField)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Uz, this.Ru, this.Eng, this.Cyrl);
        }

        /// <summary>
        /// In Uzbek
        /// </summary>
        [JsonPropertyName("uz")]
        public string Uz { get; set; } = default!;

        /// <summary>
        /// In Russian
        /// </summary>
        [JsonPropertyName("ru")]
        public string Ru { get; set; } = default!;

        /// <summary>
        /// In English
        /// </summary>
        [JsonPropertyName("en")]
        public string Eng { get; set; } = default!;

        /// <summary>
        /// In Cyrl
        /// </summary>
        [JsonPropertyName("cyrl"), JsonIgnore, AllowNull]
        public string? Cyrl { get; set; } = default!;

        public static implicit operator MultiLanguageField(string data) => new MultiLanguageField()
        {
            Ru = data,
            Uz = data,
            Eng = data,
            Cyrl = data
        };

        public static bool operator ==(MultiLanguageField a, string b)
        {
            return a.Ru == b || a.Eng == b || a.Uz == b || a.Cyrl == b;
        }

        public static bool operator !=(MultiLanguageField a, string b)
        {
            return a.Ru != b && a.Eng != b && a.Uz != b && a.Cyrl != b;
        }

        public override string ToString()
        {
            return SerializerHelper.ToJsonString(this);
        }
    }
}