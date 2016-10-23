using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Lab2.Models.DbModels
{
    public class Message
    {
        public Message()
        {

        }
        public Message(string senderId, string subject, string body)
        {
            SenderId = senderId;
            Subject = subject;
            Body = body;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Subject { get; set; }
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime SendTime { get; set; }

        [Required]
        [StringLength(255)]
        public string Body { get; set; }

        [ForeignKey("Sender")]
        public virtual string SenderId { get; set; }
        public virtual ApplicationUser Sender { get; set; }

        public virtual ICollection<Group> GroupReceivers { get; set; }
        public virtual ICollection<ApplicationUserMessage> ApplicationUserMessages { get; set; }
        //public virtual ICollection<ApplicationUser> UserReceivers { get; set; }
    }
}