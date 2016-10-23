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

        public ActionResult Participate(int? id)
        {
            List<GroupViewModel> groups = new List<GroupViewModel>();
            Group g = Db.Groups.Find(id);

            string userID = User.Identity.GetUserId();
            ApplicationUser currentUser = Db.Users.First(x => x.Id == userID);
            g.Users.Add(currentUser);

            Db.SaveChanges();

            return RedirectToAction("GetGroupOfMine");
        }

        public ActionResult Leave(int? id)
        {
            List<GroupViewModel> groups = new List<GroupViewModel>();
            Group g = Db.Groups.Find(id);

            string userID = User.Identity.GetUserId();
            ApplicationUser currentUser = Db.Users.First(x => x.Id == userID);
            g.Users.Remove(currentUser);
                //Add(currentUser);

            Db.SaveChanges();

            return RedirectToAction("GetGroupOfMine");
        }

    }
}