using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Models.Backend.Login;

public class UsuarioResponse
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public string? Token { get; set; }
    public string? Apellido { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? Telefono { get; set; }
}
