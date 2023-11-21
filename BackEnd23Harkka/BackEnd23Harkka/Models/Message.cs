using System.ComponentModel.DataAnnotations;

namespace BackEnd23Harkka.Models
{
    public class Message
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string? Title { get; set; }
        public string? Body { get; set; }
        [Required]
        public User Sender { get; set; }
        public User? Recipient { get; set; }
        public Message? prevMessage { get; set; }
    }
}
