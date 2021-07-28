using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models.Request
{
    public class UserRegisterRequestModel
    {
        [Required]
        [EmailAddress]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(64)]
        public string Fullname { get; set; }

        [Required]
        [StringLength(maximumLength: 100, ErrorMessage = "The password should be minimum of 8 characters",
            MinimumLength = 8)]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,}$",
            ErrorMessage = "Password should have minimum of 8 characters and should include one upper, lower, number and a special char ")]
        public string Password { get; set; }

        [DataType(DataType.Date)]
        public DateTime? JoinedOn { get; set; }
    }
}
