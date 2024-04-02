using CRUD1_M.Entities;
using CRUD1_M.Utils;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CRUD1_M.Repositories.Impl
{
    public class UserRepository
    {
        private DbUtil dbUtil;
        public UserRepository(DbUtil dbUtil)
        {
            this.dbUtil = dbUtil;
        }

        public User register(User user)
        {
            string sql = "INSERT INTO users (name, email, password) VALUES (@name, @email, @password)";
            try
            {
                NpgsqlCommand cmd = dbUtil.GetNpgsqlCommand(sql);
                cmd.Parameters.AddWithValue("name", user.name);
                cmd.Parameters.AddWithValue("email", user.email);
                cmd.Parameters.AddWithValue("password", user.password);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                dbUtil.closeConnection();
                return user;
            } catch(NpgsqlException e)
            {
                throw e;
            }
            return null;
        }

        public User login(User user, IConfiguration configuration)
        {
            string sql = "SELECT * FROM users WHERE email = @email AND password = @password";
            try
            {
                NpgsqlCommand cmd = dbUtil.GetNpgsqlCommand(sql);
                cmd.Parameters.AddWithValue("email", user.email);
                cmd.Parameters.AddWithValue("password", user.password);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                if(reader.Read())
                {
                    User userLoggedIn = new User()
                    {
                        id = reader.GetInt32(0),
                        name = reader.GetString(1),
                        email = reader.GetString(2),
                        password = reader.GetString(3)
                    };
                    return new User()
                    {
                        id = userLoggedIn.id,
                        name = userLoggedIn.name,
                        email = userLoggedIn.email,
                        token = GenerateJWTToken(userLoggedIn, configuration)
                    };
                }
                cmd.Dispose();
                dbUtil.closeConnection();
            } catch(NpgsqlException e)
            {
                throw e;
            }
            return null;
        }

        private string GenerateJWTToken(User user, IConfiguration configuration )
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.name),
                new Claim(ClaimTypes.Email, user.email)
            };
            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],configuration["Jwt:Audience"],claims,expires: DateTime.Now.AddMinutes(120),signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
