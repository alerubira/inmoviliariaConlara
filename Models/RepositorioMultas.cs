using MySql.Data.MySqlClient;

namespace Inmobiliaria.Models
{
    public class RepositorioMultas
    {
        private readonly string connectionString;

        public RepositorioMultas(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        }

        public int Alta(Multas multa)
        {
            int res = -1;
            //hacer transaccion para modificar habilitado de inmueble a false
            using (var connection = new MySqlConnection(connectionString))
            {
                string sql = @"INSERT INTO Multa
                    (idContrato,fechaMulta, fechaHastaContrato,nuevaFechaHastaContrato,importeCuota,importeMulta,cuotasAdeudadas)
                    VALUES (@idContrato,@fechaMulta, @fechaHastaContrato, @nuevaFechaHastaContrato,@importeCuota,@importeMulta,@cuotasAdeudadas);
                    SELECT LAST_INSERT_ID();";
                using (var command = new MySqlCommand(sql, connection))
                {
                
                    command.Parameters.AddWithValue("@idContrato", multa.IdContrato);
                    command.Parameters.AddWithValue("@fechaMulta", multa.FechaMulta);
                    command.Parameters.AddWithValue("@fechaHastaContrato", multa.FechaHastaContrato);
                    command.Parameters.AddWithValue("@nuevaFechaHastaContrato", multa.NuevaFechaHastaContrato);
                    command.Parameters.AddWithValue("@importeCuota", multa.ImporteCuota);
                    command.Parameters.AddWithValue("@importeMulta", multa.ImporteMulta);
                    command.Parameters.AddWithValue("@cuotasAdeudadas", multa.CuotasAdeudadas);
                    

                    connection.Open();
                    res = Convert.ToInt32(command.ExecuteScalar());
                    multa.IdMulta = res;
                    connection.Close();
                }
            }
            return res;
        }

      /*  public int Baja(int id)
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
        }*/

       /* public int Modificacion(Contratos contrato)
        {
            int res = -1;
            using (var connection = new MySqlConnection(connectionString))
            {
                string sql = @"UPDATE Contratos SET 
                        idInquilino=@idInquilino, idInmuebles=@idInmuebles, monto=@monto, fechaDesde=@fechaDesde, fechaHasta=@fechaHasta, vigente=@vigente,cantidadCuotas=@cantidadCuotas,cuotasPagas=@cuotasPagas,mesInicio=@mesInicio
                        WHERE IdContrato = @id";
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", contrato.IdContrato);
                    command.Parameters.AddWithValue("@idInquilino", contrato.IdInquilino);
                    command.Parameters.AddWithValue("@idInmuebles", contrato.IdInmuebles);
                    command.Parameters.AddWithValue("@monto", contrato.Monto);
                    command.Parameters.AddWithValue("@fechaDesde", contrato.FechaDesde);
                    command.Parameters.AddWithValue("@fechaHasta", contrato.FechaHasta);
                    command.Parameters.AddWithValue("@vigente", contrato.Vigente);
                    command.Parameters.AddWithValue("@cantidadCuotas", contrato.CantidadCuotas);
                    command.Parameters.AddWithValue("@cuotasPagas", contrato.CuotasPagas);
                    command.Parameters.AddWithValue("@mesInicio", contrato.MesInicio);

                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return res;
        }*/

        public IList<Multas> ObtenerTodos()
        {
            var res = new List<Multas>();
            using (var connection = new MySqlConnection(connectionString))
            {
                string sql = @"SELECT IdMulta,idContrato, fechaMulta, fechaHastaContrato, nuevaFechaHastaContrato, importeCuota, importeMulta,cuotasAdeudadas
                            FROM multa
                            
                            ORDER BY fechaMulta";
                using (var command = new MySqlCommand(sql, connection))
                {
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var multa = new Multas
                        {
                            IdMulta = Convert.ToInt32(reader["IdMulta"]),
                            IdContrato = Convert.ToInt32(reader["idContrato"]),
                            FechaMulta = Convert.ToDateTime(reader["fechaMulta"]),
                            FechaHastaContrato = Convert.ToDateTime(reader["fechaHastaContrato"]),
                            NuevaFechaHastaContrato = Convert.ToDateTime(reader["nuevaFechaHastaContrato"]),
                            ImporteCuota = Convert.ToDecimal(reader["importeCuota"]),
                            ImporteMulta = Convert.ToDecimal(reader["importeMulta"]),
                            CuotasAdeudadas = Convert.ToInt32(reader["cuotasAdeudadas"])



                        };
                        res.Add(multa);
                    }
                    connection.Close();
                }
            }
            return res;
        }
       /*  public IList<Contratos> ObtenerTodosPoIdInmueble(int idInmueble)
        {
             var res = new List<Contratos>();
            
            using (var connection = new MySqlConnection(connectionString))
            {
                string sql = @"SELECT IdContrato,IdInquilino, idInmuebles, monto, fechaDesde, fechaHasta, vigente,cantidadCuotas,cuotasPagas,mesInicio
                            FROM contratos
                            WHERE IdInmuebles = @id && vigente=1";
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", idInmueble);
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var contrato = new Contratos
                        {
                            IdContrato = Convert.ToInt32(reader["IdContrato"]),
                            IdInquilino = Convert.ToInt32(reader["idInquilino"]),
                            IdInmuebles = Convert.ToInt32(reader["idInmuebles"]),
                            Monto = Convert.ToDecimal(reader["monto"]),
                            FechaDesde = Convert.ToDateTime(reader["fechaDesde"]),
                            FechaHasta = Convert.ToDateTime(reader["fechaHasta"]),
                            Vigente = Convert.ToBoolean(reader["vigente"]),
                            CantidadCuotas = Convert.ToInt32(reader["cantidadCuotas"]),
                            CuotasPagas = Convert.ToInt32(reader["cuotasPagas"]),
                            MesInicio = Convert.ToInt32(reader["mesInicio"])



                        };
                        res.Add(contrato);
                    }
                    connection.Close();
                }
            }
            return res;
        }*/


        public Multas? ObtenerPorId(int id)
        {


            Multas? multa = null;
            using (var connection = new MySqlConnection(connectionString))
            {
                string sql = @"SELECT IdMulta,IdContrato, frchaMulta, fechaHastaContrato, nuevaFechaHastaContrato, importeCuota, importeMulta,cuotasAdeudadas
                            FROM multa
                            WHERE IdMulta = @id";
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        multa = new Multas
                        {
                            IdMulta = Convert.ToInt32(reader["IdMulta"]),
                            IdContrato = Convert.ToInt32(reader["idContrato"]),
                            FechaMulta = Convert.ToDateTime(reader["fechaMulta"]),
                            FechaHastaContrato = Convert.ToDateTime(reader["fechaHastaContrato"]),
                            NuevaFechaHastaContrato = Convert.ToDateTime(reader["nuevaFechaHastaContrato"]),
                            ImporteCuota = Convert.ToDecimal(reader["importeCuota"]),
                            ImporteMulta = Convert.ToDecimal(reader["importeMulta"]),
                            CuotasAdeudadas = Convert.ToInt32(reader["cuotasAdeudadas"])


                        };
                    }
                    connection.Close();
                }
            }
            return multa;
        }
       
        
        }
    
}