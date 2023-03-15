using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TatBlog.Core.Entities;
using TatBlog.Services.Blogs;
using TatBlog.WebApp.Areas.Admin.Models;
using MapsterMapper;
using TatBlog.Services.Media;
using FluentValidation;


namespace TatBlog.WebApp.Areas.Admin.Controllers
{
    public class PostsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IBlogRepository _blogRepository;
        private readonly IMediaManager _mediaManager;
        public PostsController(IBlogRepository blogRepository, IMapper mapper, IMediaManager mediaManager)
        {
            _blogRepository = blogRepository;
            _mapper = mapper;   
            _mediaManager = mediaManager;
        }
        private async Task PopulatePostFilterModelAsync(PostFilterModel model)
        {
            var authors = await _blogRepository.GetAuthorsAsync();
            var categories = await _blogRepository.GetCategoriesAsync();

            model.AuthorList = authors.Select(c => new SelectListItem()
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });
        }
        public async Task<IActionResult>Index(PostFilterModel model)
        {
            var postQuery = _mapper.Map<PostQuery>(model);
            

            ViewBag.PostsList = await _blogRepository
                .GetPagedPostsAsync(postQuery, 1, 10);
            await PopulatePostFilterModelAsync(model);

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id =0) 
        {
            var post = id >0
                ?await _blogRepository.GetPostByIdAsync(id, true)
                : null;

            var model = post == null
                ? new PostEditModel()
                : _mapper.Map<PostEditModel>(post);

            await PopulatePostFilterModelAsync(model);

            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(
            IValidator<PostEditModel> postValidator, 
            PostEditModel model)
        {
            var validationResult = await postValidator.ValidateAsync(model);

            if(!ModelState.IsValid)
            {
                await PopulatePostFilterModelAsync(model);
                return View(model);
            }

            var post = model.Id > 0
                ? await _blogRepository.GetPostByIdAsync(model.Id)
                : null;

            if(post == null)
            {
                post = _mapper.Map<Post>(model);
                post.Id = 0;
                post.PostedDate = DateTime.Now;
            }
            else
            {
                _mapper.Map<Post>(post);

                post.Category = null;
                post.ModifiedDate = DateTime.Now;
            }

            if (model.ImageFile?.Length > 0) 
            {
                var newImagePath = await _mediaManager.SaveFileAsync(
                    model.ImageFile.OpenReadStream(),
                    model.ImageFile.FileName,
                    model.ImageFile.ContentType);
                if (!string.IsNullOrWhiteSpace(newImagePath))
                {
                    await _mediaManager.DeleteFileAsync(post.ImageUrl);
                    post.ImageUrl = newImagePath;
                }
            }

            await _blogRepository.CreateOrUpdatePostAsync(post, model.GetSelectedTags());

            return RedirectToAction(nameof(Index));
        }

        private Task PopulatePostFilterModelAsync(PostEditModel model)
        {
            throw new NotImplementedException();
        }

        [HttpPost]

        public async Task<IActionResult> VerifyPostSlug(int id, string urlSlug)
        {
            var slugExistedd = await _blogRepository
                .IsPostSlugExistedAsync(id, urlSlug);

            return slugExistedd
                ? Json($"Slug '{urlSlug}' đã được sửa dụng")
                : Json(true);
        }
        private IActionResult View(PostFilterModel model)
        {
            throw new NotImplementedException();
        }
    }
}
