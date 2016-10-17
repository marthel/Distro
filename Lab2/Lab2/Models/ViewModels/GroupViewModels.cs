using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab2.Models.ViewModels
{
    public class JoinGroupViewModel
    {
        [Display(Name = "Name")]
        public string Name { get; set; }

        [HiddenInput(DisplayValue = false)]
        public Guid GroupId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public Guid UserId { get; set; }
    }
    public class CreateGroupViewModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string Creater { get; set; }
    }
    public class EditGroupViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public Guid GroupId { get; set; }

        [Required]
        [Display(Name = "New Name")]
        public string NewName { get; set; }
    }
}