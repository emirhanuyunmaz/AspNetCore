using BlogApp.Data.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.ViewComponents
{
    public class TagsMenu: ViewComponent
    {
        private ITagRepository _tagRepository;
        public TagsMenu(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(){
            Console.WriteLine("*******************************************************");
            return View(await _tagRepository.Tags.ToListAsync());
        }
    }
}