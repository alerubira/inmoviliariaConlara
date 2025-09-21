using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using MySql.Data.MySqlClient;

namespace InmobiliariaConlara.Models
{
    public class RepositorioUsuario
    {
        private readonly string connectionString;

        private readonly string GlobalSalt = "MiSaltSecreto123";

        public RepositorioUsuario(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection") ?? string.Empty; ;
        }

        public int Alta(Usuario u)
        {
            int res = -1;
            using (var connection = new MySqlConnection(connectionString))
            {
                string sql = @"INSERT INTO Usuario
                    (Nombre, Apellido, eMail, Clave,avatar,rol)
                    VALUES (@nombre, @apellido, @email, @clave,@avatar,@rol);
                    SELECT LAST_INSERT_ID();";
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@nombre", u.Nombre);
                    command.Parameters.AddWithValue("@apellido", u.Apellido);
                    command.Parameters.AddWithValue("@eMail", u.Email);
                    command.Parameters.AddWithValue("@clave", u.Clave ?? "");
                    command.Parameters.AddWithValue("@avatar", u.Avatar);
                    command.Parameters.AddWithValue("@rol", u.Rol);
                    connection.Open();
                    res = Convert.ToInt32(command.ExecuteScalar());
                    u.IdUsuario = res;
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
                string sql = "DELETE FROM usuario WHERE IdUsuario = @id";
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

        public int Modificacion(Usuario u)
        {
            int res = -1;
            using (var connection = new MySqlConnection(connectionString))
            {
                string sql = @"UPDATE Usuario
                    SET  Apellido=@apellido,Nombre=@nombre, Email=@email, clave=@clave, avatar=@avatar,rol=@rol
                    WHERE IdUsuario = @id";
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@apellido", u.Apellido);
                    command.Parameters.AddWithValue("@nombre", u.Nombre);
                    command.Parameters.AddWithValue("@email", u.Email);
                    command.Parameters.AddWithValue("@clave", u.Clave ?? "");
                    if (String.IsNullOrEmpty(u.Avatar))
                        command.Parameters.AddWithValue("@avatar", /*DBNull.Value*/"");
                    else
                        command.Parameters.AddWithValue("@avatar", u.Avatar);
                    // command.Parameters.AddWithValue("@avatar", u.Avatar);
                    command.Parameters.AddWithValue("@rol", u.Rol);
                    command.Parameters.AddWithValue("@id", u.IdUsuario);
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return res;
        }

        public IList<Usuario> ObtenerTodos()
        {
            IList<Usuario> res = new List<Usuario>();
            using (var connection = new MySqlConnection(connectionString))
            {
                string sql = @"SELECT IdUsuario, Nombre, Apellido, Email, clave, avatar, rol FROM usuario";
                using (var command = new MySqlCommand(sql, connection))
                {
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Usuario u = new Usuario
                        {
                            IdUsuario = reader.GetInt32("IdUsuario"),
                            Nombre = reader.GetString("Nombre"),
                            Apellido = reader.GetString("Apellido"),
                            Email = reader.GetString("email"),
                            Clave = reader.GetString("clave"),
                            Avatar = reader.GetString("avatar"),
                            Rol = reader.GetInt32("rol"),
                        };
                        res.Add(u);
                    }
                    connection.Close();
                }
            }
            return res;
        }

        public Usuario? ObtenerPorId(int id)
        {
            Usuario? u = null;
            using (var connection = new MySqlConnection(connectionString))
            {
                string sql = @"SELECT IdUsuario, Nombre, Apellido,email, clave,avatar, rol 
                    FROM usuario WHERE IdUsuario=@id";
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        u = new Usuario
                        {
                            IdUsuario = reader.GetInt32("IdUsuario"),
                            Nombre = reader.GetString("Nombre"),
                            Apellido = reader.GetString("Apellido"),
                            Email = reader.GetString("email"),
                            Clave = reader.GetString("clave"),
                            Avatar = reader.GetString("avatar"),
                            Rol = reader.GetInt32("rol"),
                        };
                    }
                    connection.Close();
                }
            }
            return u;
        }
        public Usuario? ObtenerPorEmail(String email)
        {
            Usuario? u = null;
            using (var connection = new MySqlConnection(connectionString))
            {
                string sql = @"SELECT IdUsuario, Nombre, Apellido, email, clave, avatar, rol 
                    FROM usuario WHERE email=@email";
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@email", email);
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        u = new Usuario
                        {
                            IdUsuario = reader.GetInt32("IdUsuario"),
                            Nombre = reader.GetString("Nombre"),
                            Apellido = reader.GetString("Apellido"),
                            Email = reader.GetString("email"),
                            Clave = reader.GetString("clave"),
                            Avatar = reader.GetString("avatar"),
                            Rol = reader.GetInt32("rol"),
                        };
                    }
                    connection.Close();
                }
            }
            return u;
        }
        public Usuario? Login(string email, string password)
        {
            var usuario = ObtenerPorEmail(email);
            if (usuario == null) return null;

            byte[] saltBytes = Encoding.ASCII.GetBytes(GlobalSalt);
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: saltBytes,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8
            ));


            if (usuario.Clave != hashed) return null;

            return usuario;

        }

        public Usuario? ObtenerPerfilPorEmail(string email)
        {
            Usuario? u = null;
            using (var connection = new MySqlConnection(connectionString))
            {
                string sql = @"SELECT IdUsuario, Nombre, Apellido, email, avatar, rol 
                            FROM usuario WHERE email=@email";
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@email", email);
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        u = new Usuario
                        {
                            IdUsuario = reader.GetInt32("IdUsuario"),
                            Nombre = reader.GetString("Nombre"),
                            Apellido = reader.GetString("Apellido"),
                            Email = reader.GetString("email"),
                            Avatar = reader.GetString("avatar"),
                            Rol = reader.GetInt32("rol"),
                        };
                    }
                    connection.Close();
                }
            }
            return u;
        }


    }
}