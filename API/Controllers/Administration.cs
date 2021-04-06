using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;


namespace API.Controllers
{
    // [Authorize(Roles="Admin")]
    public class Administration : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public Administration(RoleManager<IdentityRole> roleManager,UserManager<User> userManager)
        {
            _roleManager=roleManager;
            _userManager=userManager;
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if(ModelState.IsValid)
            {
                IdentityRole identityRole=new IdentityRole{
                    Name=model.RoleName
                };
                IdentityResult result= await _roleManager.CreateAsync(identityRole);
                if(result.Succeeded)
                {
                    return Redirect("ListRoles");
                }
                foreach(IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("",error.Description);
                }
            }
            
            return View(model);
        }

        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles=_roleManager.Roles;
            return View(roles);
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(string id){
            var role=await _roleManager.FindByIdAsync(id);
            if(role==null)
            {
                ViewBag.ErrorMessage=$"Role with Id = {id} cannot be found";
                return View("NotFound");
            }
            var model =new EditRoleViewModel{
                Id=role.Id,
                RoleName=role.Name
            };

            foreach(var user in _userManager.Users)
            {
                if(await _userManager.IsInRoleAsync(user,role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model){
            var role=await _roleManager.FindByIdAsync(model.Id);
            if(role==null)
            {
                ViewBag.ErrorMessage=$"Role with Id = {model.Id} cannot be found";
                return View("NotFound");
            }
           else{
               role.Name=model.RoleName;
               var result = await _roleManager.UpdateAsync(role);
               if(result.Succeeded)
               {
                   return RedirectToAction("ListRoles");
               }
               foreach(var error in result.Errors)
               {
                   ModelState.AddModelError("",error.Description);
               }
               return View(model);
           }
        } 

        [HttpGet]
        public async Task<IActionResult> DeleteRole(string id){
            var role=await _roleManager.FindByIdAsync(id);
            if(role==null)
            {
                ViewBag.ErrorMessage=$"Role with Id = {id} cannot be found";
                return View("NotFound");
            } 
            else{             
               var result = await _roleManager.DeleteAsync(role);
               if(result.Succeeded)
               {
                   return RedirectToAction("ListRoles");
               }
               foreach(var error in result.Errors)
               {
                   ModelState.AddModelError("",error.Description);
               }
               return View();
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            ViewBag.roleId=roleId;
            var role = await _roleManager.FindByIdAsync(roleId);
            if(role==null)
            {
                ViewBag.ErrorMessage=$"Role with Id = {roleId} cannot be found";
                return View("NotFound");
            }
            var model=new List<UserRoleViewModel>();
            foreach(var user in _userManager.Users)
            {
                var userRoleViewModel = new UserRoleViewModel{
                    UserId=user.Id,
                    UserName=user.UserName
                };
                if(await _userManager.IsInRoleAsync(user,role.Name))
                {
                    userRoleViewModel.IsSelected=true;
                }
                else
                {
                    userRoleViewModel.IsSelected=false;
                }
                model.Add(userRoleViewModel);
            }
            return View(model);
        } 

        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> models,string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if(role==null)
            {
                ViewBag.ErrorMessage=$"Role with Id = {roleId} cannot be found";
                return View("NotFound");
            }
            for(int i=0;i<models.Count;i++)
            {
                var user=await _userManager.FindByIdAsync(models[i].UserId);
                IdentityResult result=null;
                if(models[i].IsSelected && !(await _userManager.IsInRoleAsync(user,role.Name)))
                {
                    result = await _userManager.AddToRoleAsync(user,role.Name);
                }
                else if(!models[i].IsSelected && await _userManager.IsInRoleAsync(user,role.Name))
                {
                     result = await _userManager.RemoveFromRoleAsync(user,role.Name);
                }
                else
                {
                    continue;
                }
                if(result.Succeeded)
                {
                    if(i<models.Count-1)
                    {
                        continue;
                    }
                    else
                    {
                        return RedirectToAction("EditRole",new{Id=roleId});
                    }
                }
            }
           return RedirectToAction("EditRole",new{Id=roleId});
        } 
    }
}
