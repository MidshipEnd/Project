using System.Data;
using System.Data.SqlClient;

using Domain;

namespace Infrastructure;

public class VentasDbContext
{
    private readonly string _connectionString;
    public VentasDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public List<Venta> List()
    {
        var data = new List<Venta>();

        var con = new SqlConnection(_connectionString);
        var cmd = new SqlCommand("SELECT [Id],[ClienteId],[ProductoId], [Fecha] FROM [Venta]", con);
        try
        {
            con.Open();
            var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                data.Add(new Venta
                {
                    Id = (Guid)dr["Id"],
                    ClienteId = (Guid)dr["ClienteId"],
                    ProductoId = (Guid)dr["ProductoId"],
                    Fecha = (DateTime)dr["Fecha"]
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

    public Venta Details(Guid id)
    {
        var data = new Venta();

        var con = new SqlConnection(_connectionString);
        var cmd = new SqlCommand("SELECT [Id],[ClienteId],[ProductoId], [Fecha] FROM [Venta] WHERE Id = @Id", con);
        cmd.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = id;
        try
        {
            con.Open();
            var dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                data.Id = (Guid)dr["Id"];
                data.ClienteId = (Guid)dr["ClienteId"];
                data.ProductoId = (Guid)dr["ProductoId"];
                data.Fecha = (DateTime)dr["Fecha"];
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

    public void Create(Venta data)
    {
        var con = new SqlConnection(_connectionString);
        var cmd = new SqlCommand("INSERT INTO [Venta] ([ClienteId],[ProductoId],[Fecha]) VALUES (@ClienteId,@ProductoId,@Fecha)", con);
        cmd.Parameters.Add("Id", SqlDbType.UniqueIdentifier).Value = data.Id;
        cmd.Parameters.Add("ClienteId", SqlDbType.UniqueIdentifier).Value = data.ClienteId;
        cmd.Parameters.Add("ProductoId", SqlDbType.UniqueIdentifier).Value = data.ProductoId;
        cmd.Parameters.Add("Fecha", SqlDbType.DateTime).Value = data.Fecha;


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

    public void Edit(Venta data)
    {
        var con = new SqlConnection(_connectionString);
        var cmd = new SqlCommand("UPDATE [Venta] SET [ClienteId] =@ClienteId, [ProductoId] =@ProductoId, [Fecha] =@Fecha) WHERE Id = @Id", con);
        cmd.Parameters.Add("Id", SqlDbType.UniqueIdentifier).Value = data.Id;
        cmd.Parameters.Add("ClienteId", SqlDbType.UniqueIdentifier).Value = data.ClienteId;
        cmd.Parameters.Add("ProductoId", SqlDbType.UniqueIdentifier).Value = data.ProductoId;
        cmd.Parameters.Add("Fecha", SqlDbType.DateTime).Value = data.Fecha;

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
        var cmd = new SqlCommand("DELETE FROM [Venta] WHERE [Id] = @Id", con);
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
