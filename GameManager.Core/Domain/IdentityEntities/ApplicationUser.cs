using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;


namespace GameManager.Core.Domain.IdentityEntities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        [Required]
        public string PersonName { get; set; }
    }
}
