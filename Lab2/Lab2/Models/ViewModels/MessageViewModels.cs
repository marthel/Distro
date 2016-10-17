using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab2.Models.ViewModels
{
    public class SendMessageViewModel
    {
        [Required]
        [Display(Name = "Text")]
        public string Text { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string SenderId { get; set; }

        [Display(Name = "User receiver(s)")]
        public List<string> UserReceivers { get; set; }

        [Display(Name = "Group receiver(s)")]
        public List<string> GroupReceivers { get; set; }
    }

    public class InboxMessageViewModel
    {
        [Display(Name = "Text")]
        public string Text { get; set; }

        [Display(Name = "Sender")]
        public string Sender { get; set; }
    }
    public class SentMessageViewModel
    {
        [Required]
        [Display(Name = "Text")]
        public string Text { get; set; }

        [Display(Name = "Receiver(s)")]
        public List<string> Receivers { get; set; }

    }

}