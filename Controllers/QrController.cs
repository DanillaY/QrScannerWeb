using copyqr.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace copyqr.Controllers
{
    [ApiController]
    public class QrController : ControllerBase
    {
        private QrInfoContext _context;

        public QrController(QrInfoContext context)
        {
            _context = context;
        }


        [HttpGet("api/[controller]/Write")]
        public void Get(string context, long id, string classroom)
        {
            QrInfo info = new QrInfo(context, classroom);
            User? user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null) return;
            info.UserId = user.Id;
            info.User = user;
            _context.Entry(info).State = EntityState.Added;
            _context.SaveChanges();
        }
        [HttpPost("api/[controller]/NewQr")]
        public QrInfo Post()
        {
            return new QrInfo(Guid.NewGuid().ToString("N")[..10], String.Empty);
        }

    }
}
