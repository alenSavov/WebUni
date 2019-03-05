using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolDiary.Models;
using SchoolDiary.Services.Articles.Models;
using SchoolDiary.Services.Contracts;
using SchoolDiary.Services.Utilities;
using System.Collections.Generic;
using System.Linq;
using X.PagedList;

namespace SchoolDiary.Web.Areas.Articles.Controllers
{
    [Area(GlobalConstants.Areas.Articles)]
    [Authorize]
    public class ArticlesController : Controller
    {
        private const string ManageArticlePath = "/Administration/Administration/ManageArticle";
        private const string ArticlesPath = "/articles/articles";
        private const string CreateArticlePathToAction = "Create";

        private readonly IArticleService _articleService;
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;
        private readonly ICloudinaryService _cloudinaryService;

        public ArticlesController(IArticleService articleService,
                                   ICommentService commentService,
                                   IMapper mapper,
                                   ICloudinaryService cloudinaryService)
        {
            this._articleService = articleService;
            this._commentService = commentService;
            this._mapper = mapper;
            this._cloudinaryService = cloudinaryService;
        }

        public IActionResult Index()
        {
            var articles = _articleService
                .GetAllArticles()
                .ToList();

            foreach (var article in articles)
            {
                article.ArticlePictureUrl = this._cloudinaryService.BuildArticlePictureUrl(article.Title, article.ArticlePictureVersion);
            }

            var articlesCollection = new AllArticlesCollectionViewModel
            {
                Articles = articles
            };

            return this.View(articlesCollection);
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(CreateArticleInputViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            if (model.Title == null ||
                model.Content == null ||
                model.Category == null)
            {
                TempData[GlobalConstants.TempDataErrorMessageKey] = GlobalConstants.InvalidData;
                return RedirectToAction(CreateArticlePathToAction);
            }

            Article article;
            var file = model.ArticlePicture;
            var validExtension = IsValidImageFileExtensions(file);
            if (!validExtension)
            {
                TempData[GlobalConstants.TempDataErrorMessageKey] = GlobalConstants.WrongFileType;
                return Redirect(CreateArticlePathToAction);
            }

            article = this._mapper.Map<Article>(model);

            if (file != null)
            {
                var articlePictureId = string.Format(GlobalConstants.ArticlePicture, model.Title);
                var fileStream = file.OpenReadStream();

                var imageUploadResult = this._cloudinaryService.UploadPicture(article.GetType(), articlePictureId, fileStream);

                var creatorName = this.User.Identity.Name;
                this._articleService.Create(model, imageUploadResult.Version, creatorName);

                TempData[GlobalConstants.TempDataSuccessMessageKey] = $"The article {model.Title} is created successfully";

                return RedirectToAction("Index");
            }

            return RedirectToAction("/");
        }

        public IActionResult Details(string id)
        {
            if (id == null)
            {
                TempData[GlobalConstants.TempDataErrorMessageKey] = GlobalConstants.InvalidId;
                return Redirect("/");
            }

            var article = this._articleService.GetById(id);
            article.ArticlePictureUrl = this._cloudinaryService.BuildArticlePictureUrl(article.Title, article.ArticlePictureVersion);
          
            var comments = this._commentService.GetAllComentsForArticle(id);

            var vmCollection = new DetailsViewModelCollection
            {
                Article = article,
                Comments = comments
            };

            return this.View(vmCollection);
        }

        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                TempData[GlobalConstants.TempDataErrorMessageKey] = GlobalConstants.InvalidId;
                return Redirect("/");
            }

            this._articleService.Delete(id);

            return Redirect(ManageArticlePath);
        }

        //public IActionResult Edit(string id)
        //{
        //    if (id == null)
        //    {
        //        TempData[GlobalConstants.TempDataErrorMessageKey] = GlobalConstants.InvalidId;
        //        return Redirect("/");
        //    }

        //    var article = this._articleService.GetById(id);

        //    return this.View(article);
        //}

        //[HttpPost]
        //public IActionResult Edit(EditArticleInputModel model, string imageUploadResult)
        //{
        //    if (model.Title == null ||
        //       model.Content == null ||
        //       model.Category == null)
        //    {
        //        TempData[GlobalConstants.TempDataErrorMessageKey] = GlobalConstants.InvalidData;
        //        return RedirectToAction(CreateArticlePathToAction);
        //    }

        //    this._articleService.Edit(model, imageUploadResult);

        //    return Redirect(ArticlesPath);
        //}

        public bool IsValidImageFileExtensions(IFormFile file)
        {
            var currentFileExtention = file.FileName.Split('.')[1];
            var correctFilesExtentionCollection = new[] { GlobalConstants.FileExtensions.PngFormat,
                                                          GlobalConstants.FileExtensions.JpgFormat ,
                                                          GlobalConstants.FileExtensions.JpegFormat};

            if (!correctFilesExtentionCollection.Contains(currentFileExtention))
            {
                return false;
            }

            return true;
        }
    }
}
