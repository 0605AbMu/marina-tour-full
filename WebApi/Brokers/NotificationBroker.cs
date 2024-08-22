using System.Net.Http.Headers;
using System.Reflection;
using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using WebApi.Attributes;
using WebApi.Contracts.Notification;
using WebApi.Extensions;

namespace WebApi.Brokers;

public class NotificationBroker
{
     private readonly IHttpClientFactory _httpClientFactory;
     private readonly IMemoryCache _memoryCache;
     private readonly IConfigurationSection _configurationSection;

     private const string TokenCacheKey = "EskizToken";

     public NotificationBroker(IHttpClientFactory httpClientFactory, IMemoryCache memoryCache,
          IConfiguration configuration)
     {
          _httpClientFactory = httpClientFactory;
          _memoryCache = memoryCache;
          _configurationSection = configuration.GetSection("Eskiz");
     }

     async private Task<string?> GetToken()
     {
          return await _memoryCache.GetOrCreateAsync(TokenCacheKey,
               async entry =>
               {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60);

                    var email = _configurationSection.GetValue<string>("Email");
                    var password = _configurationSection.GetValue<string>("Password");

                    var data = new MultipartFormDataContent()
                    {
                         {
                              new StringContent(email!), "email"
                         },
                         {
                              new StringContent(password!), "password"
                         }
                    };

                    var response = await _httpClientFactory.CreateClient("NotificationBrokerHttpClient")
                         .PostAsync("auth/login", data);

                    response.EnsureSuccessStatusCode();

                    var jsonResponse = await response.Content.ReadFromJsonAsync<JsonElement>();

                    if (jsonResponse.TryGetProperty("data", out JsonElement element1) &&
                        element1.TryGetProperty("token", out JsonElement tokenElement) &&
                        tokenElement.GetString() is { } token &&
                        !token.IsNullOrEmpty())
                         return token;

                    throw new InvalidOperationException("Token is not consumed from Eskiz SMS service");
               });
     }

     private Task<HttpClient> GetHttpClient => GetToken().ContinueWith((task) =>
     {
          var httpClient = _httpClientFactory.CreateClient("NotificationBrokerHttpClient");
          httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", task.Result);
          return httpClient;
     });

     private MultipartFormDataContent PrepareFormData<T>(T data)
     {
          var properties = typeof(T).GetProperties().Where(x => x.HasAttribute<FormFieldAttribute>());
          var formData = new MultipartFormDataContent();

          foreach (var propertyInfo in properties)
          {
               var attr = propertyInfo.GetCustomAttribute<FormFieldAttribute>();
               formData.Add(new StringContent(propertyInfo.GetValue(data)?.ToString() ?? ""),
                    attr?.Name ?? propertyInfo.Name);
          }

          return formData;
     }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
     async private Task SendRequest(MultipartFormDataContent? data)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
     {
#if !DEBUG
          var client = await this.GetHttpClient;
          var response = await client.PostAsync("message/sms/send", data);
          response.EnsureSuccessStatusCode();
#endif
     }

     public async Task SendSmsAsync(SendMessageDto dto)
     {
          var data = PrepareFormData(dto);
          await this.SendRequest(data);
     }
}