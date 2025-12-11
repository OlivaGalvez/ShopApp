using Newtonsoft.Json;

namespace ShopApp.Models.Backend.Inmueble;

public class CategoryResponse
{
    public int Id { get; set; }
    [JsonProperty("nombre")]
    public string? NombreCategory { get; set; }
    public string? ImagenUrl { get; set; }
}
