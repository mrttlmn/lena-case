using LENA.WebAppCase.Core.Model;
using LENA.WebAppCase.Core.Repository;
using LENA.WebAppCase.Core.Service;
using LENA.WebAppCase.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace LENA.WebAppCase.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IGenericService<Form> _formService;
        public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IGenericService<Form> formService)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _formService = formService;
        }
        [Authorize]
        public async Task<IActionResult> IndexAsync()
        {
            var data = await _formService.GetAllAsync();
            return View(data);
        }

        [Route("form/{formId}")]
        [Authorize]
        public async Task<IActionResult> ViewForm(int formId)
        {
            var data = await _formService.GetAsync(formId);
            return View(data);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SubmitFormAsync(string formName, string formDescription, string fieldsJson)
        {
            var result = await _formService.AddAsync(await this.GenerateFormEntityAsync(formName, formDescription, fieldsJson));
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<Form> GenerateFormEntityAsync(string formName, string formDescription, string fieldsJson)
        {
            return new Form
            {
                name = formName,
                description = formDescription,
                createdAt = DateTime.Now,
                createdBy = _userManager.GetUserId(User),
                fields = JsonConvert.DeserializeObject<List<Field>>(fieldsJson)
            };
        }
    }
}
