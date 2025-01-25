using System.ComponentModel.DataAnnotations;

namespace FlightManagement.BusinessLogic.BL.DTOs
{
    public class UserDTOs
    {
        public class BaseUser
        {
            [Required]
            public string FullName { get; set; } = null!;
        }
        public class CreateUser : BaseUser
        {
        }
        public class UpdateUser : CreateUser
        {
            [Required]
            public Guid Id { get; set; }
        }


    }
}
