using Lab2.DAL.Contexts;
using Lab2.Models.DbModels;
using Lab2.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab2.Controllers
{
    public class MessageController : Controller
    {

        private IdentityContext Db = new IdentityContext();
        // GET: Message
        public ActionResult Sent()
        {
            var viewList = new List<SentMessageViewModel>();
            var list = new List<string>();
            string id = User.Identity.GetUserId();
            var messages = Db.Messages.Where(m => m.SenderId.Equals(id)).ToList();


            foreach(Message m in messages)
            {

                SentMessageViewModel viewModel = new SentMessageViewModel();
                viewModel.Subject = m.Subject;
                viewModel.Receivers = new List<string>();

                //System.Diagnostics.Debug.WriteLine(viewModel.Subject);
                list = m.ApplicationUserMessages.Where(u => u.Message_Id == m.Id).Select(u => u.User_Id).ToList();
                foreach (string s in list)
                {
                    viewModel.Receivers.Add(Db.Users.First(u => u.Id.Equals(s)).Email.ToString()+";");
                   //System.Diagnostics.Debug.WriteLine(s);   
                }
                
                viewList.Add(viewModel);
               
            }
            return View(viewList);
        }
        //GET
        public ActionResult Send()
        {
            return View();
        }
        // POST: Mesesage
        [HttpPost]
        public ActionResult Send(SendMessageViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
               
                Message m = new Message();
                m.SenderId = User.Identity.GetUserId();
                Debug.WriteLine(m.SenderId);

                m.Subject = viewModel.Subject;
                Debug.WriteLine(m.Subject);
                m.Body = viewModel.Body;
                Debug.WriteLine(m.Body);

                Db.Messages.Add(m);
                Db.SaveChanges();
                string[] Receivers;
                Receivers = viewModel.UserReceivers.Split(',');
                foreach (string r in Receivers)
                {
                    ApplicationUserMessage user_message = new ApplicationUserMessage();
                    user_message.Message_Id = m.Id;
                    user_message.User_Id = (Db.Users.First(u => u.Email.Equals(r)).Id.ToString());
                    Db.ApplicationUserMessages.Add(user_message);
                    Db.SaveChanges();
                }
                return RedirectToAction("Sent");
            }
            return View();
        }
        // GET USER-INBOX
        public ActionResult Inbox()
        {
            ApplicationUserMessage user_message = new ApplicationUserMessage();
            var list = new List<string>();
            string id = User.Identity.GetUserId();
            //user_message.User_Id.Where(m=> m == id)
            var userMessage = Db.ApplicationUserMessages.Where(m => m.User_Id.Equals(id)).ToList();
            List<InboxMessageViewModel> viewModel = new List<InboxMessageViewModel>();
            List<Message> listOfMsg = new List<Message>();

            foreach(ApplicationUserMessage um in userMessage)
            {
               // Debug.WriteLine("OLLON: " + um.Message_Id + "   " + um.User_Id);
                listOfMsg.Add( Db.Messages.Where(a => a.Id.Equals(um.Message_Id)).First());
                Debug.WriteLine("PONANI" + listOfMsg.Last().Body);
                viewModel.Add(new InboxMessageViewModel(listOfMsg.Last().Body, listOfMsg.Last().Sender.Email, listOfMsg.Last().Subject));
            }

            return View(viewModel);
        }
        //GET 
        public ActionResult Details(MessageViewModel viewModel)
        {

            Debug.WriteLine("BÖÖÖÖÖÖÖÖÖÖÖÖÖÖÖÖHHHHHHHHH");
            Debug.WriteLine("BÖÖÖÖÖÖÖÖÖÖÖÖÖÖÖÖHHHHHHHHH");

            Debug.WriteLine("BÖÖÖÖÖÖÖÖÖÖÖÖÖÖÖÖHHHHHHHHH");
            Debug.WriteLine("BÖÖÖÖÖÖÖÖÖÖÖÖÖÖÖÖHHHHHHHHH");


            // MessageViewModel mvm = new MessageViewModel(viewModel.Subject, viewModel.Sender, viewModel.Body);
            return View(viewModel);
        }
    }
}