using System.Threading.Tasks;
using IdentityApp.Models;
using IdentityApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace IdentityApp.Controllers
{
    public class UsersController:Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        public UsersController(UserManager<AppUser> userManager,RoleManager<AppRole> roleManager)
        {   
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Index(){
            return View(_userManager.Users);
        }

        public IActionResult Create(){
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel model){
            if(ModelState.IsValid){
                var user = new AppUser {
                    UserName = model.UserName,
                    Email = model.Email,
                    FullName = model.FullName
                };
                
                IdentityResult result = await _userManager.CreateAsync(user,model.Password);

                if(result.Succeeded){
                    return RedirectToAction("Index");
                }
                foreach (var err in result.Errors){
                    ModelState.AddModelError("",err.Description);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(string Id){
            if(Id == null){
                return RedirectToAction("Index");
            }
            
            var user = await _userManager.FindByIdAsync(Id);

            if(user != null ){
                ViewBag.Roles = await _roleManager.Roles.Select(i => i.Name).ToListAsync();
                return View(new EditViewModel {
                    Id = Id,
                    FullName = user.FullName,
                    Email = user.Email,
                    SelectedRole = await _userManager.GetRolesAsync(user)
                });
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string Id , EditViewModel model){
            
            if(Id != model.Id){
                return RedirectToAction("Index");
            }

            if(ModelState.IsValid){
                var user = await _userManager.FindByIdAsync(Id);

                if(user == null){
                    return RedirectToAction("Index");
                }else{
                    user.Email = model.Email;
                    user.UserName = model.UserName;
                    user.FullName = model.FullName;
                    var result = await _userManager.UpdateAsync(user);

                    if(result.Succeeded && !string.IsNullOrEmpty(model.Password)){
                        await _userManager.RemovePasswordAsync(user);
                        await _userManager.AddPasswordAsync(user,model.Password);
                    }

                    if(result.Succeeded){
                        await _userManager.RemoveFromRolesAsync(user , await _userManager.GetRolesAsync(user));
                        if(model.SelectedRole != null){
                            await _userManager.AddToRolesAsync(user ,model.SelectedRole);
                        }
                        return RedirectToAction("Index");
                    }

                    foreach(var err in result.Errors){
                        ModelState.AddModelError("",err.Description);
                    }
                }
            }

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Delete(string id){
            var user = await _userManager.FindByIdAsync(id);
            if(user != null ){
                await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("Index");
        }
    }
}