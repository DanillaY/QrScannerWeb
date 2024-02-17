using copyqr.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace copyqr.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly QrInfoContext _context;

        public UserController(QrInfoContext context) => _context = context;

        private static byte[] HashPass(string pass)
        {
            byte[] hashBytes = null;
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(pass);
                hashBytes = md5.ComputeHash(inputBytes);
            }
            return hashBytes;
        }

        [HttpPost("api/[controller]/Authorization")]
        public long? Post(string login, string password)
        {
            byte[] passResult = HashPass(password);
            bool hashEquals = false;
            User user = null;

            foreach (User u in _context.Users)
            {
                if (u.Hash.Length == passResult.Length && u.Login.Equals(login))
                {
                    int i = 0;
                    while ((i < passResult.Length) && (passResult[i] == u.Hash[i]))
                        i++;
                    if (i == passResult.Length)
                    {
                        hashEquals = true;
                        user = u;
                    }
                }
            }
            return user == null ? -1 : user.Id;
        }
        [HttpGet("api/[controller]/GetAllUsers")]
        public ICollection<User> Get() => _context.Users.ToList();

        [HttpGet("api/[controller]/NewUser")]
        public void Getuser(string pass, string login)
        {

            User info = new User(HashPass(pass), login);
            _context.Users.Entry(info).State = EntityState.Added;
            _context.SaveChanges();

        }
    }
}
