using System.Text.Json;
namespace Api.Controllers;

public class SnakeCaseNamingPolicy : JsonNamingPolicy
{
    public override string ConvertName(string name)
    {
        if (string.IsNullOrEmpty(name))
            return name;

        // Преобразование к формату CamelCase
        name = ConvertCamelCase(name);

        // Преобразование к Snake Case
        return ConvertSnakeCase(name);
    }

    private string ConvertCamelCase(string name)
    {
        return System.Text.RegularExpressions.Regex.Replace(name, @"([a-z])([A-Z])", "$1_$2");
    }

    private string ConvertSnakeCase(string name)
    {
        return name.ToLower().Replace(" ", "_");
    }
}
