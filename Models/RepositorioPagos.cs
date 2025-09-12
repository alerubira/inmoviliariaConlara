using MySql.Data.MySqlClient;

namespace Inmobiliaria.Models
{
    public class RepositorioPagos
    {
        private readonly string connectionString;

        public RepositorioPagos(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        }

        public int Alta(Pagos pago)
        {
            int res = -1;
            
            using (var connection = new MySqlConnection(connectionString))
            {
                string sql = @"INSERT INTO Pagos
                    ( idContratos, fechaPago, importe)
                    VALUES ( @IdContratos, @fechaPago,@importe);
                    SELECT LAST_INSERT_ID();";
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@idContratos", pago.IdContratos);
                    command.Parameters.AddWithValue("@fechaPago", pago.FechaPago);
                    command.Parameters.AddWithValue("@importe", pago.Importe);
                    

                    connection.Open();
                    res = Convert.ToInt32(command.ExecuteScalar());
                    pago.IdPagos = res;
                    connection.Close();
                }
            }
            return res;
        }

        public int Baja(int id)
        {
            int res = -1;
            using (var connection = new MySqlConnection(connectionString))
            {
                string sql = "DELETE FROM Pagos WHERE IdPagos = @id";
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return res;
        }

        public int Modificacion(Pagos pago)
        {
            int res = -1;
            using (var connection = new MySqlConnection(connectionString))
            {
                string sql = @"UPDATE Pagos SET 
                        idContratos=@idContratos, fechaPago=@fechaPago, importe=@importe
                        WHERE IdPagos = @id";
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", pago.IdPagos);
                    command.Parameters.AddWithValue("@idContratos", pago.IdContratos);
                    command.Parameters.AddWithValue("@fechaPago", pago.FechaPago);
                    command.Parameters.AddWithValue("@importe", pago.Importe);
                   
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return res;
        }

public IList<Pagos> ObtenerTodos()
{
    var res = new List<Pagos>();
    using (var connection = new MySqlConnection(connectionString))
    {
        string sql = @"
            SELECT p.IdPagos, p.idContratos, p.fechaPago, p.importe,
                   i.Direccion AS DireccionInmueble
            FROM pagos p
            INNER JOIN contratos c ON p.idContratos = c.IdContrato
            INNER JOIN inmuebles i ON c.IdInmuebles = i.idInmuebles
            ORDER BY p.fechaPago;
        ";

        using (var command = new MySqlCommand(sql, connection))
        {
            connection.Open();
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var pago = new Pagos
                {
                    IdPagos = Convert.ToInt32(reader["IdPagos"]),
                    IdContratos = Convert.ToInt32(reader["idContratos"]),
                    FechaPago = Convert.ToDateTime(reader["fechaPago"]),
                    Importe = Convert.ToDecimal(reader["importe"]),
                    DireccionInmueble = reader["DireccionInmueble"].ToString()
                };

                res.Add(pago);
            }
            connection.Close();
        }
    }
    return res;
}



        public Pagos? ObtenerPorId(int id)
        {


            Pagos? pago = null;
            using (var connection = new MySqlConnection(connectionString))
            {
                string sql = @"SELECT IdPagos,IdContratos,fechaPago, importe
                            FROM pagos
                            WHERE IdPagos = @id";
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        pago = new Pagos
                        {
                            IdPagos = Convert.ToInt32(reader["IdPagos"]),
                            IdContratos =Convert.ToInt32(reader["idContratos"]),
                            FechaPago = Convert.ToDateTime(reader["fechaPago"]),
                            Importe = Convert.ToDecimal(reader["importe"]),
                    };
                    }
                    connection.Close();
                }
            }
            return pago;
        }
    }
}