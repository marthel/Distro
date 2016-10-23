using Lab2.DAL.Contexts;
using Lab2.Models.DbModels;
using Lab2.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;


namespace Lab2.Controllers
{
    [Authorize]
    public class GroupController : Controller
    {

        private IdentityContext Db = new IdentityContext();


        // Get: Group Start view
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateGroupViewModel gvm)
        {
            Group group = new Group();
            group.Users = new List<ApplicationUser>();
            ApplicationUser currentUser = Db.Users.Find(User.Identity.GetUserId());
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
            string usrId = User.Identity.GetUserId();
            var grps = Db.Groups.Where(g => !g.Users.Any(u => u.Id.Equals(usrId))).ToList();
            foreach (Group g in grps)
            {
                groups.Add(new GroupViewModel(g.Name, g.Id));
            }

            return View(groups);
        }

        public ActionResult GetGroupOfMine()
        {
            List<GroupViewModel> groups = new List<GroupViewModel>();
            string usrId = User.Identity.GetUserId();
            var grps = Db.Groups.Where(g => g.Users.Any(u => u.Id.Equals(usrId))).ToList();
            foreach (Group g in grps)
            {
                groups.Add(new GroupViewModel(g.Name, g.Id));
            }

            return View(groups);
        }

        public ActionResult Participate(int? id)
        {
            List<GroupViewModel> groups = new List<GroupViewModel>();
            Group g = Db.Groups.Find(id);
            ApplicationUser currentUser = Db.Users.Find(User.Identity.GetUserId());
            g.Users.Add(currentUser);

            Db.SaveChanges();

            return RedirectToAction("GetGroupOfMine");
        }

        public ActionResult Leave(int? id)
        {
            List<GroupViewModel> groups = new List<GroupViewModel>();
            Group g = Db.Groups.Find(id);
            ApplicationUser currentUser = Db.Users.Find(User.Identity.GetUserId());
            g.Users.Remove(currentUser);

            Db.SaveChanges();

            return RedirectToAction("GetGroupOfMine");
        }

    }
}