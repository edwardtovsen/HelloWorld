using Microsoft.AspNetCore.Mvc;
using System.Linq;
using MessageApi.Models;
using System.Collections.Generic;

namespace MessageApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly MessageContext _context;

        public MessageController(MessageContext context)
        {
            _context = context;

            if (_context.Messages.Count() == 0)
            {
                _context.Messages.Add(new Message { Id = 1, Text = "Hello World" });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public ActionResult<List<Message>> GetAll()
        {
            return _context.Messages.ToList();
        }

        [HttpGet("{id}", Name = "GetMessage")]
        public ActionResult<Message> GetById(int id)
        {
            var message = _context.Messages.Find(id);
            if (message == null)
            {
                return NotFound();
            }
            return message;
        }

        [HttpPost]
        public IActionResult Create(Message msg)
        {
            _context.Messages.Add(msg);
            _context.SaveChanges();

            return CreatedAtRoute("GetMessage", new { id = msg.Id }, msg);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, Message msg)
        {
            var message = _context.Messages.Find(id);
            if (message == null)
            {
                return NotFound();
            }

            message.Text = msg.Text;

            _context.Messages.Update(message);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var message = _context.Messages.Find(id);
            if (message == null)
            {
                return NotFound();
            }

            _context.Messages.Remove(message);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
