using System.Data;
using System.Data.SqlClient;

using Domain;

namespace Infrastructure;

public class ProductosDbContext
{
    private readonly string _connectionString;
    public ProductosDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public List<Producto> List()
    {
        var data = new List<Producto>();

        var con = new SqlConnection(_connectionString);
        var cmd = new SqlCommand("SELECT [Id],[Descripcion],[Precio],[Cantidad] FROM [Producto]", con);
        try
        {
            con.Open();
            var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                data.Add(new Producto
                {
                    Id = (Guid)dr["Id"],
                    Descripcion = (string)dr["Descripcion"],
                    Precio = (decimal)dr["Precio"],
                    Cantidad = (int)dr["Cantidad"]
                });
            }
            return data;
        }
        catch (Exception) 
        { throw; }
        finally
        {
            con.Close();
        }
    }

    public Producto Details(Guid id)
    {
        var data = new Producto();

        var con = new SqlConnection(_connectionString);
        var cmd = new SqlCommand("SELECT [Id],[Descripcion],[Precio],[Cantidad] FROM [Producto] WHERE Id = @Id", con);
        cmd.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = id;
        try
        {
            con.Open();
            var dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                data.Id = (Guid)dr["Id"];
                data.Descripcion = (string)dr["Descripcion"];
                data.Precio = (decimal)dr["Precio"];
                data.Cantidad = (int)dr["Cantidad"];
            }
            return data;
        }
        catch (Exception) 
        { throw; }
        finally
        {
            con.Close();
        }
    }

    public void Create(Producto data)
    {
        var con = new SqlConnection(_connectionString);
        var cmd = new SqlCommand("INSERT INTO [Producto] ([Descripcion],[Precio],[Cantidad]) VALUES (@Descripcion,@Precio,@Cantidad)", con);
        cmd.Parameters.Add("Id", SqlDbType.UniqueIdentifier).Value = data.Id;
        cmd.Parameters.Add("Descripcion", SqlDbType.NVarChar, 128).Value = data.Descripcion;
        cmd.Parameters.Add("Precio", SqlDbType.Decimal, 10).Value = data.Precio;
        cmd.Parameters.Add("Cantidad", SqlDbType.Int, 4).Value = data.Cantidad;

        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception) 
        { throw; }
        finally
        {
            con.Close();
        }
    }

    public void Edit(Producto data)
    {
        var con = new SqlConnection(_connectionString);
        var cmd = new SqlCommand("UPDATE [Producto] SET [Descripcion] =@Descripcion, [Precio] = @Precio, [Cantidad] = @Cantidad WHERE Id = @Id", con);
        cmd.Parameters.Add("Id", SqlDbType.UniqueIdentifier).Value = data.Id;
        cmd.Parameters.Add("Descripcion", SqlDbType.NVarChar, 128).Value = data.Descripcion;
        cmd.Parameters.Add("Precio", SqlDbType.Decimal, 10).Value = data.Precio;
        cmd.Parameters.Add("Cantidad", SqlDbType.Int, 4).Value = data.Cantidad;

        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception) 
        { throw; }
        finally
        {
           con.Close();
        }
    }

    public void Delete(Guid id)
    {
        var con = new SqlConnection(_connectionString);
        var cmd = new SqlCommand("DELETE FROM [Producto] WHERE [Id] = @Id", con);
        cmd.Parameters.Add("Id", SqlDbType.UniqueIdentifier).Value = id;


        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception) 
        { throw; }
        finally
        {
             con.Close();
        }
    }
}
