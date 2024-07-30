using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WpfApp6.Models
{
    public class Login
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id
        {
            get; set;
        }

        [Required]
        [StringLength(100)]
        public string UserName
        {
            get; set;
        }

        [Required]
        [StringLength(100)]
        public string Password
        {
            get; set;
        }

        [Required]
        [StringLength(100)]
        public string Department
        {
            get; set;
        }

        [Required]
        [StringLength(50)]
        public string Role
        {
            get; set;
        }

        public ICollection<ProjectEngineer> ProjectEngineers { get; set; }

        public static class Roles
        {
            public const string Admin = "Admin";
            public const string GeneralManager = "GeneralManager";
            public const string BlastEngineer = "BlastEngineer";
            public const string StoreKeeper = "StoreKeeper";
            // Add other roles as needed
        }
    }
}
