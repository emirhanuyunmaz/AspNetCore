using IdentityApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace IdentityApp.TagHelpers
{
    [HtmlTargetElement("td",Attributes ="asp-role-users")]
    public class RoleUsersTagHelper:TagHelper{

        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public RoleUsersTagHelper(RoleManager<AppRole> roleManager,UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HtmlAttributeName("asp-role-users")]
        public string RoleId { get; set; } = null!;

        public override async Task ProcessAsync(TagHelperContext context,TagHelperOutput output ){
            
            var userName = new List<string>();
            var role = await _roleManager.FindByIdAsync(RoleId);

            if(role != null && role.Name != null){
                foreach(var user in _userManager.Users){
                    if(await _userManager.IsInRoleAsync(user,role.Name)){
                        userName.Add(user.UserName ?? "");
                    }
                }
                output.Content.SetContent(userName.Count == 0 ? "Kullanıcı yok":string.Join(", ",userName));
            }
            
        }

    }
}