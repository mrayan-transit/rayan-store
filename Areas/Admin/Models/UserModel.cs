using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using RayanStore.Data;

namespace RayanStore.Areas.Admin.Models
{
    public class UserModel
    {
        public UserModel()
        {
            initializeModel();
        }

        public UserModel(User entity)
        {
            Id = entity.Id;
            FullName = entity.FullName;
            Email = entity.Email;
            Role = entity.Role;

            initializeModel();
        }

        private void initializeModel()
        {
            RoleSelectList = User.Roles.Select(x => new SelectListItem(x, x));
        }

        public int Id {get;set;}

        [Display(Name = "Full Name")]
        [Required]
        public string FullName {get;set;}

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email {get;set;}

        [DataType(DataType.Password)]
        public string Password {get;set;}

        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword {get;set;}

        [Required]
        public string Role {get;set;}

        public IEnumerable<SelectListItem> RoleSelectList {get;set;}
    }
}