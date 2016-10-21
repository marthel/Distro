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
                
                m.Subject = viewModel.Subject;
                m.Body = viewModel.Body;
                m.SenderId = User.Identity.GetUserId();
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
        // GET
        public ActionResult Inbox(InboxMessageViewModel viewModel)
        {

            return View();
        }
        //GET 
        public ActionResult Details(MessageViewModel viewModel)
        {
            return View();
        }
    }
}