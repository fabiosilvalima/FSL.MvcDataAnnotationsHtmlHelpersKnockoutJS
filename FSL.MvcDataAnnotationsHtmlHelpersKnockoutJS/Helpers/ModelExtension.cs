using Newtonsoft.Json;

public static class ModelExtension
{
    public static string ToJson<T>(this T obj)
    {
        if (obj == null) return null;

        return JsonConvert.SerializeObject(obj);
    }
}