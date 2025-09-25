using Inmobiliaria.Models;
using MySql.Data.MySqlClient;

namespace Inmobiliaria.Models
{
    public class RepositorioInmuebles
    {
        private readonly string connectionString;

        public RepositorioInmuebles(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        }

        public int Alta(Inmuebles inmueble)
        {
            int res = -1;
            using (var connection = new MySqlConnection(connectionString))
            {
                string sql = @"INSERT INTO Inmuebles
                    ( direccion, ambientes, superficie, latitud, longitud, idPropietario, IdTipoInmueble,precio,habilitado)
                    VALUES ( @direccion, @ambientes, @superficie, @latitud, @longitud, @idPropietario, @IdTipoInmueble,@precio,@habilitado);
                    SELECT LAST_INSERT_ID();";
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@direccion", inmueble.Direccion);
                    command.Parameters.AddWithValue("@ambientes", inmueble.Ambientes);
                    command.Parameters.AddWithValue("@superficie", inmueble.Superficie);
                    command.Parameters.AddWithValue("@latitud", inmueble.Latitud);
                    command.Parameters.AddWithValue("@longitud", inmueble.Longitud);
                    command.Parameters.AddWithValue("@idPropietario", inmueble.IdPropietario);
                    command.Parameters.AddWithValue("@IdTipoInmueble", inmueble.IdTipoInmueble);
                    command.Parameters.AddWithValue("@precio", inmueble.Precio);
                    command.Parameters.AddWithValue("@habilitado", 1);
                    connection.Open();
                    res = Convert.ToInt32(command.ExecuteScalar());
                    inmueble.IdTipoInmueble = res;
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
                string sql = "DELETE FROM Inmuebles WHERE IdInmuebles = @id";
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

        public int Modificacion(Inmuebles inmueble)
        {
            int res = -1;
            using (var connection = new MySqlConnection(connectionString))
            {
                string sql = @"UPDATE Inmuebles SET 
                        direccion=@direccion, ambientes=@ambientes, superficie=@superficie, latitud=@latitud, longitud=@longitud, idPropietario=@idPropietario, IdTipoInmueble=@IdTipoInmueble,precio=@precio,habilitado=@habilitado
                        WHERE IdInmuebles = @id";
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", inmueble.IdInmuebles);
                    command.Parameters.AddWithValue("@direccion", inmueble.Direccion);
                    command.Parameters.AddWithValue("@ambientes", inmueble.Ambientes);
                    command.Parameters.AddWithValue("@superficie", inmueble.Superficie);
                    command.Parameters.AddWithValue("@latitud", inmueble.Latitud);
                    command.Parameters.AddWithValue("@longitud", inmueble.Longitud);
                    command.Parameters.AddWithValue("@idPropietario", inmueble.IdPropietario);
                    command.Parameters.AddWithValue("@idTipoInmueble", inmueble.IdTipoInmueble);
                    command.Parameters.AddWithValue("@precio", inmueble.Precio);
                    command.Parameters.AddWithValue("@habilitado", inmueble.Habilitado);
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return res;
        }

        public IList<Inmuebles> ObtenerTodos()
        {
            var res = new List<Inmuebles>();
            using (var connection = new MySqlConnection(connectionString))
            {
                string sql = @"SELECT IdInmuebles,Direccion, Ambientes, Superficie, Latitud, Longitud, idPropietario, IdTipoInmueble,precio,habilitado
                            FROM Inmuebles
                            ORDER BY Direccion";
                using (var command = new MySqlCommand(sql, connection))
                {
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var inmueble = new Inmuebles
                        {
                            IdInmuebles = Convert.ToInt32(reader["IdInmuebles"]),
                            Direccion = reader["Direccion"].ToString() ?? string.Empty,
                            Ambientes = Convert.ToInt32(reader["Ambientes"]),
                            Superficie = Convert.ToInt32(reader["Superficie"]),
                            Latitud = Convert.ToDecimal(reader["Latitud"]),
                            Longitud = Convert.ToDecimal(reader["Longitud"]),
                            IdPropietario = Convert.ToInt32(reader["idPropietario"]),
                            IdTipoInmueble = Convert.ToInt32(reader["IdTipoInmueble"]),
                            Precio = Convert.ToDecimal(reader["precio"]),
                            Habilitado = Convert.ToBoolean(reader["habilitado"])

                        };
                        res.Add(inmueble);
                    }
                    connection.Close();
                }
            }
            return res;
        }

        public Inmuebles? ObtenerPorId(int id)
        {


            Inmuebles? inmueble = null;
            using (var connection = new MySqlConnection(connectionString))
            {
                string sql = @"SELECT IdInmuebles,Direccion, Ambientes, Superficie, Latitud, Longitud, idPropietario, IdTipoInmueble,precio,habilitado
                            FROM Inmuebles
                            WHERE IdInmuebles = @id";
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        inmueble = new Inmuebles
                        {
                            IdInmuebles = Convert.ToInt32(reader["IdInmuebles"]),
                            Direccion = reader["Direccion"].ToString() ?? string.Empty,
                            Ambientes = Convert.ToInt32(reader["Ambientes"]),
                            Superficie = Convert.ToInt32(reader["Superficie"]),
                            Latitud = Convert.ToDecimal(reader["Latitud"]),
                            Longitud = Convert.ToDecimal(reader["Longitud"]),
                            IdPropietario = Convert.ToInt32(reader["IdPropietario"]),
                            IdTipoInmueble = Convert.ToInt32(reader["IdTipoInmueble"]),
                            Precio = Convert.ToDecimal(reader["precio"]),
                            Habilitado = Convert.ToBoolean(reader["habilitado"])

                        };
                    }
                    connection.Close();
                }
            }
            return inmueble;
        }
        public Inmuebles? ObtenerPorDireccion(String dir)
        {


            Inmuebles? inmueble = null;
            using (var connection = new MySqlConnection(connectionString))
            {
                string sql = @"SELECT IdInmuebles,Direccion, Ambientes, Superficie, Latitud, Longitud, idPropietario, IdTipoInmueble,precio,habilitado
                            FROM Inmuebles
                            WHERE Direccion = @dir";
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@dir", dir);
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        inmueble = new Inmuebles
                        {
                            IdInmuebles = Convert.ToInt32(reader["IdInmuebles"]),
                            Direccion = reader["Direccion"].ToString() ?? string.Empty,
                            Ambientes = Convert.ToInt32(reader["Ambientes"]),
                            Superficie = Convert.ToInt32(reader["Superficie"]),
                            Latitud = Convert.ToDecimal(reader["Latitud"]),
                            Longitud = Convert.ToDecimal(reader["Longitud"]),
                            IdPropietario = Convert.ToInt32(reader["IdPropietario"]),
                            IdTipoInmueble = Convert.ToInt32(reader["IdTipoInmueble"]),
                            Precio = Convert.ToDecimal(reader["precio"]),
                            Habilitado = Convert.ToBoolean(reader["habilitado"])

                        };
                    }
                    connection.Close();
                }
            }
            return inmueble;
        }

        public IList<Inmuebles> BuscarPorFraccionDireccion(string fraccion)
        {
            IList<Inmuebles> res = new List<Inmuebles>();

            using (var connection = new MySqlConnection(connectionString))
            {
                string sql = @"
                            SELECT IdInmuebles, Direccion, Precio,Habilitado
                            FROM Inmuebles
                            WHERE direccion LIKE @fraccion
                        ";

                using (var command = new MySqlCommand(sql, connection))
                {
                    // Agregamos los comodines para LIKE
                    command.Parameters.AddWithValue("@fraccion", "%" + fraccion + "%");

                    connection.Open();
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {

                        if (reader.GetBoolean("Habilitado"))
                        {
                            Inmuebles i = new Inmuebles
                            {
                                IdInmuebles = reader.GetInt32("IdInmuebles"),
                                Direccion = reader.GetString("Direccion"),
                                Precio = reader.GetDecimal("Precio"),
                                Habilitado = reader.GetBoolean("Habilitado")
                            };
                            res.Add(i);
                        }



                    }

                    connection.Close();
                }
            }

            return res;
        }

        public IList<Inmuebles> ObtenerPorPropietario(int idPropietario)
        {
            var res = new List<Inmuebles>();
            using (var connection = new MySqlConnection(connectionString))
            {
                string sql = @"SELECT IdInmuebles,Direccion, Ambientes, Superficie, Latitud, Longitud, idPropietario, IdTipoInmueble,precio,habilitado
                            FROM Inmuebles
                            WHERE idPropietario = @idPropietario
                            ORDER BY Direccion";
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@idPropietario", idPropietario);
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var inmueble = new Inmuebles
                        {
                            IdInmuebles = Convert.ToInt32(reader["IdInmuebles"]),
                            Direccion = reader["Direccion"].ToString() ?? string.Empty,
                            Ambientes = Convert.ToInt32(reader["Ambientes"]),
                            Superficie = Convert.ToInt32(reader["Superficie"]),
                            Latitud = Convert.ToDecimal(reader["Latitud"]),
                            Longitud = Convert.ToDecimal(reader["Longitud"]),
                            IdPropietario = Convert.ToInt32(reader["idPropietario"]),
                            IdTipoInmueble = Convert.ToInt32(reader["IdTipoInmueble"]),
                            Precio = Convert.ToDecimal(reader["precio"]),
                            Habilitado = Convert.ToBoolean(reader["habilitado"])

                        };
                        res.Add(inmueble);
                    }
                    connection.Close();
                }
            }
            return res;
        }
        public IList<Inmuebles> ObtenerTodosDisponibles()
        {
            var res = new List<Inmuebles>();
            using (var connection = new MySqlConnection(connectionString))
            {
                string sql = @"SELECT IdInmuebles,Direccion, Ambientes, Superficie, Latitud, Longitud, idPropietario, IdTipoInmueble,precio,habilitado
                            FROM Inmuebles
                            where habilitado=1
                            ORDER BY Direccion";
                using (var command = new MySqlCommand(sql, connection))
                {
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var inmueble = new Inmuebles
                        {
                            IdInmuebles = Convert.ToInt32(reader["IdInmuebles"]),
                            Direccion = reader["Direccion"].ToString() ?? string.Empty,
                            Ambientes = Convert.ToInt32(reader["Ambientes"]),
                            Superficie = Convert.ToInt32(reader["Superficie"]),
                            Latitud = Convert.ToDecimal(reader["Latitud"]),
                            Longitud = Convert.ToDecimal(reader["Longitud"]),
                            IdPropietario = Convert.ToInt32(reader["idPropietario"]),
                            IdTipoInmueble = Convert.ToInt32(reader["IdTipoInmueble"]),
                            Precio = Convert.ToDecimal(reader["precio"]),
                            Habilitado = Convert.ToBoolean(reader["habilitado"])

                        };
                        res.Add(inmueble);
                    }
                    connection.Close();
                }
            }
            return res;
        }
         public IList<Inmuebles> BuscarDesocupados(DateTime? fechaDesde, DateTime? fechaHasta)
        {
            var res = new List<Inmuebles>();
            using (var connection = new MySqlConnection(connectionString))
            {
                string sql = @"SELECT i.IdInmuebles, i.Direccion, i.Ambientes, i.Superficie,  i.Latitud, i.Longitud, i.idPropietario, i.IdTipoInmueble,  i.precio, i.habilitado
                            
                                FROM Inmuebles i
                                LEFT JOIN Contratos c 
                                    ON i.IdInmuebles = c.IdInmuebles 
                                    AND c.vigente = 1
                                    AND (
                                        (YEAR(c.fechaDesde) < YEAR(@fechaHasta) 
                                        OR (YEAR(c.fechaDesde) = YEAR(@fechaHasta) AND MONTH(c.fechaDesde) <= MONTH(@fechaHasta)))
                                        AND
                                        (YEAR(c.fechaHasta) > YEAR(@fechaDesde) 
                                        OR (YEAR(c.fechaHasta) = YEAR(@fechaDesde) AND MONTH(c.fechaHasta) >= MONTH(@fechaDesde)))
                                    )
                                WHERE c.IdInmuebles IS NULL
                                ORDER BY i.Direccion";

                using (var command = new MySqlCommand(sql, connection))
                {
                     command.Parameters.AddWithValue("@fechaDesde", fechaDesde ?? DateTime.MinValue);
                    command.Parameters.AddWithValue("@fechaHasta", fechaHasta ?? DateTime.MaxValue);
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var inmueble = new Inmuebles
                        {
                            IdInmuebles = Convert.ToInt32(reader["IdInmuebles"]),
                            Direccion = reader["Direccion"].ToString() ?? string.Empty,
                            Ambientes = Convert.ToInt32(reader["Ambientes"]),
                            Superficie = Convert.ToInt32(reader["Superficie"]),
                            Latitud = Convert.ToDecimal(reader["Latitud"]),
                            Longitud = Convert.ToDecimal(reader["Longitud"]),
                            IdPropietario = Convert.ToInt32(reader["idPropietario"]),
                            IdTipoInmueble = Convert.ToInt32(reader["IdTipoInmueble"]),
                            Precio = Convert.ToDecimal(reader["precio"]),
                            Habilitado = Convert.ToBoolean(reader["habilitado"])

                        };
                        res.Add(inmueble);
                    }
                    connection.Close();
                }
            }
            return res;
        }


    }
}
        
    