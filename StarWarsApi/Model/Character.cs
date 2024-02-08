using System.ComponentModel.DataAnnotations;

namespace TestWebApi.Model;

public class Character
{
    public string Name { get; set; } = string.Empty;
    public string BirthYear { get; set; }
    public string HomeWorld { get; set; } = string.Empty;
    [Range(0, int.MaxValue)]
    public int FilmCount { get; set; }
}