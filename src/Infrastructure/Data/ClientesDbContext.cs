using System.Data;
using System.Data.SqlClient;

using Domain;

namespace Infrastructure;
public class ClientesDbContext
{
    private readonly string _connectionString;
    public ClientesDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public List<Cliente> List()
    {
        var data = new List<Cliente>();

        // ToDo
        
        var con = new SqlConnection(_connectionString);
        var cmd = new SqlCommand("SELECT [Id],[Nombre],[Direccion],[Telefono],[Correo] FROM [Cliente]", con);
        try
        {
            con.Open();
            var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                data.Add(new Cliente
                {
                    Id = (Guid)dr["Id"],
                    Nombre = (string)dr["Nombre"],
                    Direccion = (string)dr["Direccion"],
                    Telefono = (string)dr["Telefono"],
                    Correo = (string)dr["Correo"]
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

    public Cliente Details(Guid id)
    {
        var data = new Cliente();

        var con = new SqlConnection(_connectionString);
        var cmd = new SqlCommand("SELECT [Id],[Nombre],[Direccion],[Telefono],[Correo] FROM [Cliente] WHERE Id = @Id", con);
        cmd.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = id;
        try
        {
            con.Open();
            var dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                data.Id = (Guid)dr["Id"];
                data.Nombre = (string)dr["Nombre"];
                data.Direccion = (string)dr["Direccion"];
                data.Telefono = (string)dr["Telefono"];
                data.Correo = (string)dr["Correo"];
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

    public void Create(Cliente data)
    {
        var con = new SqlConnection(_connectionString);
        var cmd = new SqlCommand("INSERT INTO [Cliente] ([Id],[Nombre],[Direccion],[Telefono],[Correo]) VALUES (newid(),@Nombre,@Direccion,@Telefono,@Correo)", con);
       // cmd.Parameters.Add("Id", SqlDbType.UniqueIdentifier).Value = data.Id;
        cmd.Parameters.Add("Nombre", SqlDbType.NVarChar, 128).Value = data.Nombre;
        cmd.Parameters.Add("Direccion", SqlDbType.NVarChar, 128).Value = data.Direccion;
        cmd.Parameters.Add("Telefono", SqlDbType.NVarChar, 10).Value = data.Telefono;
        cmd.Parameters.Add("Correo", SqlDbType.NVarChar, 128).Value = data.Correo;

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

    public void Edit(Cliente data)
    {
        var con = new SqlConnection(_connectionString);
        var cmd = new SqlCommand("UPDATE [Cliente] SET [Nombre] =@Nombre, [Direccion] = @Direccion, [Telefono] = @Telefono, [Correo] = @Correo WHERE [Id] = @Id", con);
        cmd.Parameters.Add("Id", SqlDbType.UniqueIdentifier).Value = data.Id;
        cmd.Parameters.Add("Nombre", SqlDbType.NVarChar, 128).Value = data.Nombre;
        cmd.Parameters.Add("Direccion", SqlDbType.NVarChar, 128).Value = data.Direccion;
        cmd.Parameters.Add("Telefono", SqlDbType.NVarChar, 10).Value = data.Telefono;
        cmd.Parameters.Add("Correo", SqlDbType.NVarChar, 128).Value = data.Correo;

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
        var cmd = new SqlCommand("DELETE FROM [Cliente] WHERE [Id] = @Id", con);
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
