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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Text { get; set; }
        [ForeignKey("Sender")]
        [ScaffoldColumn(false)]
        public virtual string SenderId { get; set; }
        public virtual ApplicationUser Sender { get; set; }
        public virtual ICollection<Group> GroupReceivers { get; set; }
        public virtual ICollection<ApplicationUser> UserReceivers { get; set; }
    }
}