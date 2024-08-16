using System.Text.Json;
using WebApi.Extensions;

namespace WebApi.Helpers;

public static class SerializerHelper
{
    public static string ToJsonString(object? data)
    {
        return ToJsonString(data, JsonSerializerOptions.Default);
    }

    // ReSharper disable once MemberCanBePrivate.Global
    public static string ToJsonString(object? data, JsonSerializerOptions options)
    {
        return JsonSerializer.Serialize(data, options);
    }

    public static T? FromJsonString<T>(string? content)
    {
        return FromJsonString<T>(content, JsonSerializerOptions.Default);
    }

    // ReSharper disable once MemberCanBePrivate.Global
    public static T? FromJsonString<T>(string? content, JsonSerializerOptions options)
    {
        return content.IsNullOrEmpty() ? default : JsonSerializer.Deserialize<T>(content!);
    }
}
