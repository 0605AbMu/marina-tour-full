namespace WebApi.Extensions;

public static class EnumerableExtensions
{
     public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
     {
          foreach (var o in enumerable)
               action.Invoke(o);
     }

     public async static Task ForEachAsync<T>(this IEnumerable<T> enumerable, Func<T, Task> action)
     {
          foreach (var o in enumerable)
               await action.Invoke(o);
     }
}