using Microsoft.AspNetCore.Identity;


namespace GameManager.Core.Domain.IdentityEntities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string PersonName { get; set; }
    }
}
