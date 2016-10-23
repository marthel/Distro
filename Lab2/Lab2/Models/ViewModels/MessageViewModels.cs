using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab2.Models.ViewModels
{
    //details about a single message
    public class MessageViewModel
    {
        public MessageViewModel()
        {
            
        }
        public MessageViewModel(string subject,string sender,string body)
        {
            Subject = subject;
            Sender = sender;
            Body = body;
        }
        public string Subject { get; set; }
        public string Sender { get; set; }
        public string Body { get; set; }
    }
    //Create and send message to user(s) and/or group(s)
    public class SendMessageViewModel
    {
        [Display(Name = "Subject")]
        public string Subject { get; set; }
        [Required]
        [Display(Name = "Body")]
        public string Body { get; set; }

        [Display(Name = "User receiver(s)")]
        public string UserReceivers { get; set; }

        [Display(Name = "Group receiver(s)")]
        public string GroupReceivers { get; set; }
    }

    //List of messages in inbox
    public class InboxMessageViewModel
    {
        public InboxMessageViewModel()
        {

        }
        public InboxMessageViewModel(string text,string sender,string subject)
        {
            Text = text;
            Sender = sender;
            Subject = subject;
        }
        public string Text { get; set; }
        public string Sender { get; set; }
        public string Subject { get; set; }
    }
    //list of messages in sent
    public class SentMessageViewModel
    {
        public string Subject { get; set; }
        public List<string> Receivers { get; set; }

    }

}