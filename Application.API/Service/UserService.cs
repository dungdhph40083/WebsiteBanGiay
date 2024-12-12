using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Data.Models;
using Microsoft.IdentityModel.Tokens;

public class UserService
{
    private readonly List<User> _users = new();
    private readonly string _jwtKey;

    public UserService(string jwtKey)
    {
        _jwtKey = jwtKey;
    }

    public User Register(string username, string password)
    {
        if (_users.Any(u => u.Username == username))
            throw new Exception("User already exists");

        var user = new User 
        { 
            UserID = Guid.NewGuid(), 
            Username = username, 
            Password = password
        };
        _users.Add(user);
        return user;
    }

    public string Authenticate(string username, string password)
    {
        var user = _users.SingleOrDefault(u => u.Username == username && u.Password == password);
        if (user == null) return null;

        // Tạo JWT
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Username)
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
