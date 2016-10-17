using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lab2.Models.ViewModels
{
    public class SendMessageViewModel
    {
        [Required]
        [Display(Name = "Text")]
        public string Text { get; set; }

        [Display(Name = "User Recivers")]
        public List<string> Reciver { get; set; }

        [Display(Name = "Reciver Or Recivers")]
        public List<string> GroupRecivers { get; set; }
    }

    public class InboxMessageViewModel
    {

        [Display(Name = "Text")]
        public string Text { get; set; }

        [Display(Name = "Sender")]
        public string Sender { get; set; }


    }
}