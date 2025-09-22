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
                    (idContrato,fechaMulta, fechaHastaContrato,nuevaFechaHastaContrato,importeCuota,importeMulta,cuotasAdeudadas,pagada)
                    VALUES (@idContrato,@fechaMulta, @fechaHastaContrato, @nuevaFechaHastaContrato,@importeCuota,@importeMulta,@cuotasAdeudadas,@pagada);
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
                    command.Parameters.AddWithValue("@pagada", multa.Pagada);


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

        public int Modificacion(Multas multa)
        {
            int res = -1;
            using (var connection = new MySqlConnection(connectionString))
            {
                string sql = @"UPDATE multa SET 
                        idContrato=@idContrato, fechaMulta=@fechaMulta, fechaHastaContrato=@fechaHastaContrato, nuevaFechaHastaContrato=@nuevaFechaHastaContrato, importeCuota=@importeCuota, importeMulta=@importeMulta,cuotasAdeudadas=@cuotasAdeudadas,pagada=@pagada
                        WHERE IdMulta = @id";
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", multa.IdMulta);
                    command.Parameters.AddWithValue("@idContrato", multa.IdContrato);
                    command.Parameters.AddWithValue("@fechaMulta", multa.FechaMulta);
                    command.Parameters.AddWithValue("@fechaHastaContrato", multa.FechaHastaContrato);
                    command.Parameters.AddWithValue("@nuevaFechaHastaContrato", multa.NuevaFechaHastaContrato);
                    command.Parameters.AddWithValue("@importeCuota", multa.ImporteCuota);
                    command.Parameters.AddWithValue("@importeMulta", multa.ImporteMulta);
                    command.Parameters.AddWithValue("@cuotasAdeudadas", multa.CuotasAdeudadas);
                    command.Parameters.AddWithValue("@pagada", multa.Pagada);


                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return res;
        }

        public IList<Multas> ObtenerTodos()
        {
            var res = new List<Multas>();
            using (var connection = new MySqlConnection(connectionString))
            {
                string sql = @"SELECT IdMulta,idContrato, fechaMulta, fechaHastaContrato, nuevaFechaHastaContrato, importeCuota, importeMulta,cuotasAdeudadas,pagada
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
                            CuotasAdeudadas = Convert.ToInt32(reader["cuotasAdeudadas"]),
                            Pagada = Convert.ToBoolean(reader["pagada"])



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
                string sql = @"SELECT IdMulta,IdContrato, fechaMulta, fechaHastaContrato, nuevaFechaHastaContrato, importeCuota, importeMulta,cuotasAdeudadas,pagada
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
                            CuotasAdeudadas = Convert.ToInt32(reader["cuotasAdeudadas"]),
                            Pagada = Convert.ToBoolean(reader["pagada"])


                        };
                    }
                    connection.Close();
                }
            }
            return multa;
        }
        public Multas? ObtenerPorIdContrato(int id)
        {


            Multas? multa = null;
            using (var connection = new MySqlConnection(connectionString))
            {
                string sql = @"SELECT IdMulta,IdContrato, fechaMulta, fechaHastaContrato, nuevaFechaHastaContrato, importeCuota, importeMulta,cuotasAdeudadas,pagada
                            FROM multa
                            WHERE IdContrato = @id";
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
                            CuotasAdeudadas = Convert.ToInt32(reader["cuotasAdeudadas"]),
                            Pagada=Convert.ToBoolean(reader["pagada"])


                        };
                    }
                    connection.Close();
                }
            }
            return multa;
        }
       
        
        }
    
}