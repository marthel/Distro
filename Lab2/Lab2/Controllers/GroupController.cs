using Lab2.DAL.Contexts;
using Lab2.Models.DbModels;
using Lab2.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;


namespace Lab2.Controllers
{
    public class GroupController : Controller
    {

        private IdentityContext Db = new IdentityContext();


        // Get: Group
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(GroupViewModel gvm)
        {
            Group group = new Group();
            group.Users = new List<ApplicationUser>();
            string id = User.Identity.GetUserId();
            //UserManagerExtensions userman = new UserManager();
            //  ApplicationUser user = (ApplicationUser)await IdentityUser.Store.Users.FindAsync(id, CancellationToken.None);
            // ApplicationUser user = UserManagerExtensions.FindById(User.Identity.GetUserId());
            ApplicationUser currentUser = Db.Users.First(x => x.Id == id);
            group.Name = gvm.Name;
            if (currentUser == null)
                Debug.WriteLine("kukenenenennenennenennenennenenennenenen");
            Db.Groups.Add(group);
            group.Users.Add(currentUser);
            

            Db.SaveChanges();

            return View();
        }
    }
}