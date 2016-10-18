using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Lab2.Models.DbModels
{
    public class ApplicationUserMessage
    {
        // Set the column order so it appears nice in the database



        
       // [Required]
        [Key, Column(Order = 0)]
        public string User_Id { get; set; }

        [ForeignKey("User_Id")]
        public virtual ApplicationUser User { get; set; }


        [Key, Column(Order = 1)]
        public int Message_Id { get; set; }

        [ForeignKey("Message_Id")]
        public virtual Message Message { get; set; }

        // Add any additional fields you need
        public bool Read { get; set; }
    }
}