using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Data.ModelContexts;
using Application.Data.Models;
using Microsoft.IdentityModel.Tokens;

public class UserService
{
    private readonly GiayDBContext _dbContext;
    private readonly string _jwtKey;

    public UserService(string jwtKey, GiayDBContext dBContext)
    {
        _jwtKey = jwtKey;
        _dbContext = dBContext;
    }

    public User Register(string username, string password)
    {
        if (_dbContext.Users.Any(u => u.Username == username))
            throw new Exception("User already exists");

        var user = new User 
        { 
            UserID = Guid.NewGuid(), 
            Username = username, 
            Password = password
        };
        _dbContext.Users.Add(user);
        return user;
    }

    public string Authenticate(string username, string password)
    {
        var user = _dbContext.Users.SingleOrDefault(u => u.Username == username && u.Password == password);
        if (user == null) return null;

        // Tạo JWT
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim("UserId", user.UserID.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role.RoleName)
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
