using System.ComponentModel.DataAnnotations;

namespace Domain;
public class Venta
{
    public Guid Id { get; set; }

    //Cliente
    public Guid ClienteId { get; set; }

    //Producto
    public Guid ProductoId { get; set; }
    
    public DateTime Fecha { get; set; }
}

