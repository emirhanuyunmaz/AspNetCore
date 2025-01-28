using Microsoft.AspNetCore.Mvc;
using FormsApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace FormsApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index(string searchString,string category)
    {
        var product = Repository.Products;
        
        if(!String.IsNullOrEmpty(searchString)){
            ViewBag.searchString = searchString;
            product = product.Where(p => p.Name!.ToLower().Contains(searchString)).ToList();
        }

        if(!String.IsNullOrEmpty(category)){
            product = product.Where(p => p.CategoryId == int.Parse(category)).ToList();
        }

        // ViewBag.Categories = new SelectList(Repository.Categories,"Category","Name",category);

        var model = new ProductViewModel{
            Products = product,
            Categories = Repository.Categories,
            SelectedCategory = category
        };
        return View(model); 
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Categories = new SelectList(Repository.Categories,"CategoryId","Name");

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Product model,IFormFile imageFile)
    {
        var extension = Path.GetExtension(imageFile.FileName);
        
        if(ModelState.IsValid){
            if(imageFile != null){
                var randomFileName = string.Format($"{Guid.NewGuid().ToString()}{extension}");
                var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/img",randomFileName);
                using(var stream = new FileStream(path,FileMode.Create)){
                    await imageFile.CopyToAsync(stream);
                }
                model.Image = randomFileName;
                model.ProductId = Repository.Products.Count + 1 ;
                Repository.CreateProduct(model);
                return RedirectToAction("Index");
            }
        }
        ViewBag.Categories = new SelectList(Repository.Categories,"CategoryId","Name");
        return View(model);
    }

    public IActionResult Edit(int? id){

        if(id == null ){
            return NotFound();
        }
        var entity = Repository.Products.FirstOrDefault(p => p.ProductId == id);
        if(entity == null){
            return NotFound();
        }
        ViewBag.Categories = new SelectList(Repository.Categories,"CategoryId","Name");
        return View(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id,Product model,IFormFile? imageFile){
        if(id != model.ProductId){
            return NotFound();
        }

        if(ModelState.IsValid){
        
            if(imageFile != null ){
                var extension = Path.GetExtension(imageFile.FileName);
                var randomFileName = string.Format($"{Guid.NewGuid().ToString()}{extension}");
                var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/img",randomFileName);
                using(var stream = new FileStream(path,FileMode.Create)){
                    await imageFile.CopyToAsync(stream);
                }
                model.Image = randomFileName;
            }
            Repository.EditProduct(model);
            return RedirectToAction("Index");
        }
        ViewBag.Categories = new SelectList(Repository.Categories,"CategoryId","Name");
        return View(model);
    }

    public IActionResult Delete(int? id){
        if(id == null){
            return NotFound();
        }

        var entity = Repository.Products.FirstOrDefault(p => p.ProductId == id);
        Repository.DeleteProduct(entity!);
        return RedirectToAction("Index");
    }

}
