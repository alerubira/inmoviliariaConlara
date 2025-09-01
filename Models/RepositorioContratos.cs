using Inmobiliaria.Models;
using MySql.Data.MySqlClient;

namespace Inmobiliaria.Models
{
    public class RepositorioContratos
    {
        private readonly string connectionString;

        public RepositorioContratos(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        }

        public int Alta(Contratos contrato)
        {
            int res = -1;
            //hacer transaccion para modificar habilitado de inmueble a false
            using (var connection = new MySqlConnection(connectionString))
            {
                string sql = @"INSERT INTO Contratos
                    ( idInquilino, idInmuebles, monto, fechadesde, fechahasta,vigente)
                    VALUES ( @IdInquilino, @idInmuebles,@monto, @fechaDesde, @fechaHasta,@vigente);
                    SELECT LAST_INSERT_ID();";
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@idInquilino", contrato.IdInquilino);
                    command.Parameters.AddWithValue("@idInmuebles", contrato.IdInmuebles);
                    command.Parameters.AddWithValue("@monto", contrato.Monto);
                    command.Parameters.AddWithValue("@fechaDesde", contrato.FechaDesde);
                    command.Parameters.AddWithValue("@fechaHasta", contrato.FechaHasta);
                    command.Parameters.AddWithValue("@vigente", contrato.Vigente);

                    connection.Open();
                    res = Convert.ToInt32(command.ExecuteScalar());
                    contrato.IdContrato = res;
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
                string sql = "DELETE FROM Contratos WHERE IdContrato = @id";
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

        public int Modificacion(Contratos contrato)
        {
            int res = -1;
            using (var connection = new MySqlConnection(connectionString))
            {
                string sql = @"UPDATE Contratos SET 
                        idInquilino=@idInquilino, idInmuebles=@idInmuebles, monto=@monto, fechaDesde=@fechaDesde, fechaHasta=@fechaHasta, vigente=@vigente
                        WHERE IdContrato = @id";
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", contrato.IdContrato);
                    command.Parameters.AddWithValue("@idInquilino", contrato.IdInquilino);
                    command.Parameters.AddWithValue("@idInmueble", contrato.IdInmuebles);
                    command.Parameters.AddWithValue("@monto", contrato.Monto);
                    command.Parameters.AddWithValue("@fechaDesde", contrato.FechaDesde);
                    command.Parameters.AddWithValue("@fechaHasta", contrato.FechaHasta);
                    command.Parameters.AddWithValue("@vigente", contrato.Vigente);
                   
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return res;
        }

        public IList<Contratos> ObtenerTodos()
        {
            var res = new List<Contratos>();
            using (var connection = new MySqlConnection(connectionString))
            {
                string sql = @"SELECT IdContrato,idInquilino, idInmuebles, monto, fechaDesde, fechaHasta, vigente
                            FROM Contratos
                            ORDER BY fechaDesde";
                using (var command = new MySqlCommand(sql, connection))
                {
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var contrato = new Contratos
                        {
                            IdContrato = Convert.ToInt32(reader["IdContrato"]),
                            IdInquilino =Convert.ToInt32(reader["idInquilino"]),
                            IdInmuebles = Convert.ToInt32(reader["idInmuebles"]),
                            Monto = Convert.ToDecimal(reader["monto"]),
                            FechaDesde = Convert.ToDateTime(reader["fechaDesde"]),
                            FechaHasta = Convert.ToDateTime(reader["fechaHasta"]),
                            Vigente = Convert.ToBoolean(reader["vigente"]),
                           

                        };
                        res.Add(contrato);
                    }
                    connection.Close();
                }
            }
            return res;
        }

        public Contratos? ObtenerPorId(int id)
        {


            Contratos? contrato = null;
            using (var connection = new MySqlConnection(connectionString))
            {
                string sql = @"SELECT IdContrato,IdInquilino, idInmuebles, monto, fechaDesde, fechaHasta, vigente
                            FROM contratos
                            WHERE IdContrato = @id";
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        contrato = new Contratos
                        {
                            IdContrato = Convert.ToInt32(reader["IdContrato"]),
                            IdInquilino =Convert.ToInt32(reader["idInquilino"]),
                            IdInmuebles = Convert.ToInt32(reader["idInmuebles"]),
                            Monto = Convert.ToDecimal(reader["monto"]),
                            FechaDesde = Convert.ToDateTime(reader["fechaDesde"]),
                            FechaHasta = Convert.ToDateTime(reader["fechaHasta"]),
                            Vigente = Convert.ToBoolean(reader["vigente"]),
                           

                        };
                    }
                    connection.Close();
                }
            }
            return contrato;
        }
    }
}
        
    