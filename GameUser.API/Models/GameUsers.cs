using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameUser.API.Models
{
    public class GameUsers
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("CustomerID")]

        public int CustomerId { get; set; }

        [Column("Name")]
        public string CustomerName { get; set; }

        [Column("PhoneNo")]
        public string MobileNumber { get; set; }

        [Column("Email")]
        public string Email { get; set; }
        [Column("Address")]
        public string Address { get; set; }
    }
}
