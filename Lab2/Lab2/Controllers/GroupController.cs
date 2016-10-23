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
    [Authorize]
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
            // ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());
            ApplicationUser currentUser = Db.Users.First(x => x.Id == id);
            group.Name = gvm.Name;
            Db.Groups.Add(group);
            group.Users.Add(currentUser);
            

            Db.SaveChanges();

            return View();
        }

        //Get Groups
        public ActionResult GetGroup()
        {
            List<GroupViewModel> groups = new List<GroupViewModel>();
           // Group group = new Group();

            var all_groups = Db.Groups.ToList();
            string id = User.Identity.GetUserId();
            ApplicationUser currentUser = Db.Users.First(x => x.Id == id);
            //var tmp = group.Users.Where(u => u.Id.Equals(id));

            foreach (Group g in all_groups)
            {
                // ApplicationUser currentUser = Db.Users.First(x => x.Id.Equals(id));
                
              //  ApplicationUser c = g.Users.Where(a => a.Id.Equals(id));
                    if (!g.Users.Contains(currentUser))
                        groups.Add(new GroupViewModel(g.Name,g.Id));
            }

            return View(groups);
        }

        public ActionResult GetGroupOfMine()
        {
            List<GroupViewModel> groups = new List<GroupViewModel>();
            // Group group = new Group();

            var all_groups = Db.Groups.ToList();
            string id = User.Identity.GetUserId();
            ApplicationUser currentUser = Db.Users.First(x => x.Id == id);
            //var tmp = group.Users.Where(u => u.Id.Equals(id));

            foreach (Group g in all_groups)
            {
                // ApplicationUser currentUser = Db.Users.First(x => x.Id.Equals(id));
                //  ApplicationUser c = g.Users.Where(a => a.Id.Equals(id));
                if (g.Users.Contains(currentUser))
                    groups.Add(new GroupViewModel(g.Name, g.Id));
            }

            return View(groups);
        }

        public ActionResult Participate()
        {
            List<GroupViewModel> groups = new List<GroupViewModel>();

            var all_groups = Db.Groups.ToList();

            foreach (Group g in all_groups)
            {
                groups.Add(new GroupViewModel(g.Name, g.Id));
            }

            return View(groups);
        }
    }
}