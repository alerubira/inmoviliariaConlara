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
                    ( idContratos, fechaPago, importe,concepto,numeroCuota,mesPago)
                    VALUES ( @IdContratos, @fechaPago,@importe,@concepto,@numeroCuota,@mesPago);
                    SELECT LAST_INSERT_ID();";
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@idContratos", pago.IdContratos);
                    command.Parameters.AddWithValue("@fechaPago", pago.FechaPago);
                    command.Parameters.AddWithValue("@importe", pago.Importe);
                    command.Parameters.AddWithValue("@concepto", pago.Concepto);
                    command.Parameters.AddWithValue("@numeroCuota", pago.NumeroCuota);
                    command.Parameters.AddWithValue("@mesPago", pago.MesPago);
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
                        idContratos=@idContratos, fechaPago=@fechaPago, importe=@importe,concepto=@concepto,numeroCuota=@numeroCuota,mesPago=@mesPago
                        WHERE IdPagos = @id";
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", pago.IdPagos);
                    command.Parameters.AddWithValue("@idContratos", pago.IdContratos);
                    command.Parameters.AddWithValue("@fechaPago", pago.FechaPago);
                    command.Parameters.AddWithValue("@importe", pago.Importe);
                    command.Parameters.AddWithValue("@concepto", pago.Concepto);
                    command.Parameters.AddWithValue("@numeroCuota", pago.NumeroCuota);
                    command.Parameters.AddWithValue("@mesPago", pago.MesPago);
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
            SELECT p.IdPagos, p.idContratos, p.fechaPago, p.importe,p.concepto,p.numeroCuota,mesPago,
                   i.Direccion AS DireccionInmueble
            FROM pagos p
            INNER JOIN contratos c ON p.idContratos = c.IdContrato
            INNER JOIN inmuebles i ON c.IdInmuebles = i.idInmuebles
            ORDER BY p.idPagos;
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
                            DireccionInmueble = reader["DireccionInmueble"].ToString(),
                            Concepto = reader["concepto"].ToString() ?? string.Empty,
                            NumeroCuota = Convert.ToInt32(reader["numeroCuota"]),
                            MesPago = Convert.ToInt32(reader["mesPago"]),
                            
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
                string sql = @"SELECT IdPagos,IdContratos,fechaPago, importe,concepto,numeroCuota,mesPago
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
                            IdContratos = Convert.ToInt32(reader["idContratos"]),
                            FechaPago = Convert.ToDateTime(reader["fechaPago"]),
                            Importe = Convert.ToDecimal(reader["importe"]),
                            Concepto = reader["concepto"].ToString() ?? string.Empty,
                            NumeroCuota = Convert.ToInt32(reader["numeroCuota"]),
                            MesPago = Convert.ToInt32(reader["mesPago"])
                        };
                    }
                    connection.Close();
                }
            }
            return pago;
        }

        public IList<Pagos> obtenerPorInquilino(int idInquiloino)
        {
            var res = new List<Pagos>();
            using (var connection = new MySqlConnection(connectionString))
            {
                string sql = @"
            SELECT p.idPagos, p.idContratos, p.fechaPago, p.importe,p.concepto,p.numeroCuota,p.mesPago,
                   i.Direccion AS DireccionInmueble
            FROM pagos p
            INNER JOIN contratos c ON p.idContratos = c.IdContrato
            INNER JOIN inmuebles i ON c.IdInmuebles = i.idInmuebles
            WHERE c.idInquilino = @idInquilino
            ORDER BY p.fechaPago;";

                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@idInquilino", idInquiloino);
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var pago = new Pagos
                        {
                            IdPagos = Convert.ToInt32(reader["idPagos"]),
                            IdContratos = Convert.ToInt32(reader["idContratos"]),
                            FechaPago = Convert.ToDateTime(reader["fechaPago"]),
                            Importe = Convert.ToDecimal(reader["importe"]),
                            DireccionInmueble = reader["DireccionInmueble"].ToString(),
                            Concepto = reader["concepto"].ToString() ?? string.Empty,
                            NumeroCuota = Convert.ToInt32(reader["numeroCuota"]),
                            MesPago = Convert.ToInt32(reader["mesPago"])
                        };

                        res.Add(pago);
                    }
                    connection.Close();
                }
            return res;
            }
        }

    }
}