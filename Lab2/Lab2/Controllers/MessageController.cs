using Lab2.DAL.Contexts;
using Lab2.Models.DbModels;
using Lab2.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Lab2.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {

        private IdentityContext Db = new IdentityContext();
        //GET: Message/sent
        public ActionResult Sent()
        {
            string id = User.Identity.GetUserId();
            var messages = Db.Messages.Where(m => m.SenderId.Equals(id)).ToList();

            List<SentMessageViewModel> viewModel = new List<SentMessageViewModel>();
            List<Message> listOfMsg = new List<Message>();
            List<string> listOfUmr = new List<string>();
            foreach (Message m in messages)
            {
                listOfUmr = new List<string>(m.ApplicationUserMessages.Where(u => u.Message_Id.Equals(m.Id)).Select(u => u.User_Id).ToList());
                List<string> listOfReceivers = new List<string>();
                foreach (string s in listOfUmr)
                {
                    listOfReceivers.Add(Db.Users.First(u => u.Id.Equals(s)).Email.ToString() + ";");
                }
                viewModel.Add(new SentMessageViewModel(m.Id, m.Subject, listOfReceivers,m.SendTime));
            }
            return View(viewModel);
        }
        //GET: Message/send
        public ActionResult Send()
        {
            return View();
        }
        //POST: Mesesage/send
        [HttpPost]
        public ActionResult Send(SendMessageViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Message m = new Message(User.Identity.GetUserId(), viewModel.Subject, viewModel.Body);
                Db.Messages.Add(m);
                Db.SaveChanges();
                string[] Receivers = viewModel.UserReceivers.Split(',');
                foreach (string r in Receivers)
                {
                    ApplicationUserMessage user_message = new ApplicationUserMessage(m.Id, (Db.Users.First(u => u.Email.Equals(r)).Id.ToString()));
                    Db.ApplicationUserMessages.Add(user_message);
                    Db.SaveChanges();
                }
                return RedirectToAction("Sent");
            }
            return View();
        }
        // GET: Message/Inbox
        public ActionResult Inbox()
        {
            ApplicationUserMessage user_message = new ApplicationUserMessage();
            string id = User.Identity.GetUserId();
            var userMessage = Db.ApplicationUserMessages.Where(m => m.User_Id.Equals(id)).ToList();
            List<InboxMessageViewModel> viewModel = new List<InboxMessageViewModel>();
            List<Message> listOfMsg = new List<Message>();

            foreach(ApplicationUserMessage um in userMessage)
            {
                listOfMsg.Add(Db.Messages.Find(um.Message_Id));
                viewModel.Add(new InboxMessageViewModel(listOfMsg.Last().Id,listOfMsg.Last().Subject, listOfMsg.Last().Sender.Email, listOfMsg.Last().SendTime));
            }

            return View(viewModel);
        }
        //GET message/details/1
        public ActionResult Details(int? id)
        {
            string userId = User.Identity.GetUserId();
            Message message = Db.Messages.Find(id);
            Debug.WriteLine(message.ApplicationUserMessages.Where(u => u.User_Id.Equals(userId)).Any());
            if (!(message.SenderId.Equals(userId)) && !(message.ApplicationUserMessages.Where(u => u.User_Id.Equals(userId)).Any()))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            message.ApplicationUserMessages.Where(u => u.Message_Id.Equals(id)).First().User.Email.ToString();
            MessageViewModel viewModel= new MessageViewModel(
                 message.Subject,
                 message.ApplicationUserMessages.Where(u => u.Message_Id.Equals(id)).First().User.Email.ToString(),
                message.Body);

            return View(viewModel);
        }
    }
}