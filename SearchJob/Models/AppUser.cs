using Microsoft.AspNetCore.Identity;

namespace SearchJob.Models
{   

    public class AppUser : IdentityUser
    {
        public List<Favorite> Favorites = new List<Favorite>();
       
    }
}
