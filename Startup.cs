using jobs.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;


[assembly: OwinStartupAttribute(typeof(jobs.Startup))]
namespace jobs
{
    public partial class Startup
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            
           
            CreateDefaultRolesAndUsers();

        }
        public void CreateDefaultRolesAndUsers()
        {
            var roleManager=new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            IdentityRole role = new IdentityRole();
            if(!roleManager.RoleExists("Admin"))
            {
                role.Name = "Admin";
                roleManager.Create(role);
                ApplicationUser user = new ApplicationUser();
                user.Email = "mahmoud";
                user.UserName = "mahmoud@gmail.com";
                user.UserType = "Admin";
                user.Statues = "متاح";
                var Check = userManager.Create(user, "Hunger@97");
                if(Check.Succeeded)
                {
                    userManager.AddToRole(user.Id,"Admin");
                }
            }


        }
    }
}
