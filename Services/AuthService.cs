using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using appP.A.Models;
using Microsoft.Maui.Storage;
using SQLite;

namespace appP.A.Services
{
    public static class AuthService
    {
        private static SQLiteAsyncConnection _db;
        private const string CurrentUserKey = "app_current_user";

        // 1. Inicializa la base de datos y crea la tabla si no existe
        private static async Task InitAsync()
        {
            if (_db != null)
                return;

            // Crea un archivo físico llamado NontonioUsers.db3 en el dispositivo
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "NontonioUsers.db3");
            _db = new SQLiteAsyncConnection(databasePath);

            // Crea la tabla basada en el modelo User
            await _db.CreateTableAsync<User>();
        }

        // 2. Guardar un nuevo usuario en la base de datos
        public static async Task<(bool success, string error)> RegisterAsync(string username, string password)
        {
            await InitAsync();

            if (string.IsNullOrWhiteSpace(username)) return (false, "Usuario vacío.");
            if (string.IsNullOrWhiteSpace(password)) return (false, "Contraseña vacía.");
            username = username.Trim();

            if (string.Equals(username, "admin", StringComparison.OrdinalIgnoreCase))
                return (false, "El nombre 'admin' está reservado.");

            // Buscar si el usuario ya existe en SQLite
            var existingUser = await _db.Table<User>()
                                        .Where(u => u.Username.ToLower() == username.ToLower())
                                        .FirstOrDefaultAsync();

            if (existingUser != null)
                return (false, "El usuario ya existe.");

            try
            {
                var newUser = new User
                {
                    Username = username,
                    PasswordHash = Hash(password)
                };

                // Insertar el usuario en la tabla
                await _db.InsertAsync(newUser);

                Preferences.Set(CurrentUserKey, username);
                return (true, string.Empty);
            }
            catch (Exception ex)
            {
                return (false, $"Error al guardar usuario: {ex.Message}");
            }
        }

        // 3. Consultar la base de datos para iniciar sesión
        public static async Task<bool> LoginAsync(string username, string password)
        {
            await InitAsync();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password)) return false;
            username = username.Trim();

            // Buscar al usuario en la tabla
            var user = await _db.Table<User>()
                                .Where(u => u.Username.ToLower() == username.ToLower())
                                .FirstOrDefaultAsync();

            if (user != null)
            {
                var providedHash = Hash(password);

                // Si la contraseña coincide, dar acceso
                if (user.PasswordHash == providedHash)
                {
                    Preferences.Set(CurrentUserKey, user.Username);
                    return true;
                }
            }

            return false;
        }

        // Nuevo: obtener todos los usuarios
        public static async Task<List<User>> GetAllUsersAsync()
        {
            await InitAsync();
            return await _db.Table<User>().ToListAsync();
        }

        // Nuevo: eliminar usuario por Id
        public static async Task<bool> DeleteUserAsync(int id)
        {
            await InitAsync();

            try
            {
                var user = await _db.FindAsync<User>(id);
                if (user == null) return false;

                await _db.DeleteAsync(user);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void Logout()
        {
            Preferences.Remove(CurrentUserKey);
        }

        public static string? GetCurrentUser()
        {
            return Preferences.Get(CurrentUserKey, null);
        }

        // 4. Encriptar contraseñas (Seguridad)
        private static string Hash(string input)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input ?? string.Empty));
            var sb = new StringBuilder(bytes.Length * 2);
            foreach (var b in bytes) sb.Append(b.ToString("x2"));
            return sb.ToString();
        }
    }
}