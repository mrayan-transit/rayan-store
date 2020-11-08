using System.ComponentModel.DataAnnotations;

namespace RayanStore.Data
{
    public class User
    {
        public int Id {get;set;}

        [Required]
        [StringLength(20)]
        public string UserName {get;set;}

        [Required]
        [StringLength(50)]
        public string FullName {get;set;}

        [Required]
        [StringLength(20)]
        public string Email {get;set;}

        [Required]
        [StringLength(100)]
        public string PasswordHash {get;set;}
    }
}