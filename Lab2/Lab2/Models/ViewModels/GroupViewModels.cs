using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab2.Models.ViewModels
{
    //list of groups 
    public class GroupViewModel
    {
        public GroupViewModel(string name, int id)
        {
            Name = name;
            GroupId = id; 
        }
        public string Name { get; set; }
        public int GroupId { get; set; }
    }
    //user with the id joins group with id
    public class JoinGroupViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public Guid GroupId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public Guid UserId { get; set; }
    }
    //create a new group
    public class CreateGroupViewModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string Creater { get; set; }
    }
    //edit existing group
    public class EditGroupViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public Guid GroupId { get; set; }
        public string CurrentName { get; set; }
        [Required]
        [Display(Name = "New Name")]
        public string NewName { get; set; }
    }
    //group and members
    public class GroupMemberViewModel
    {
        public string Name { get; set; }
        public List<string> Members { get; set; }
    }
}