using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniShop.Business.Abstract;
using MiniShop.Shared.Helpers.Abstract;
using MiniShop.Shared.ResponseViewModels;
using MiniShop.Shared.ViewModels;
using MiniShop.UI.Extensions;
using MiniShop.UI.Helpers;
using NToastNotify;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace MiniShop.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="SuperAdmin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productManager;
        private readonly ICategoryService _categoryManager;
        private readonly IImageHelper _imageHelper;
        private readonly IMapper _mapper;
        private readonly IToastNotification _toastNotification;

        public ProductController(IProductService productManager, ICategoryService categoryManager, IImageHelper imageHelper, IMapper mapper, IToastNotification toastNotification)
        {
            _productManager = productManager;
            _categoryManager = categoryManager;
            _imageHelper = imageHelper;
            _mapper = mapper;
            _toastNotification = toastNotification;
        }

        //[Authorize(Roles = "SuperAdmin,Admin")]
        [AllowAnonymous]  //admin panele gşren herkes ürünleri listeleyebilsin demek.
        public async Task<IActionResult> Index(bool id = false)
        {
            Response<List<ProductViewModel>> result = await _productManager.GetAllNonDeletedAsync(id);
            ViewBag.ShowDeleted = id;
            TempData["TransferId"] = id;
            _toastNotification.AddSuccessToastMessage("Hello");
            return View(result.Data);
        }
        public async Task<IActionResult> UpdateIsHome(int id)
        {
            var result = await _productManager.UpdateIsHomeAsync(id);
            var product = await _productManager.GetByIdAsync(id);
            return Json(product.Data.IsHome);
        }

        public async Task<IActionResult> UpdateIsActive(int id)
        {
            var result = await _productManager.UpdateIsActiveAsync(id);
            var product = await _productManager.GetByIdAsync(id);
            return Json(product.Data.IsHome);
        }

        [HttpGet]
        //yalnızca superadmin create edebilir.
       
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryManager.GetActiveCategories();
            AddProductViewModel model = new AddProductViewModel
            {
                Categories = categories.Data
            };
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create(AddProductViewModel model, IFormFile image)
        {
        
            if (ModelState.IsValid && model.CategoryIds.Count > 0 && image != null)
            {
                model.ImageUrl = await _imageHelper.UploadImage(image, "products");
                model.Url = Jobs.GetUrl(model.Name);
                await _productManager.CreateAsync(model);
                _toastNotification.AddSuccessToastMessage("Ürün başarı ile eklenmiştir.");
             
                return RedirectToAction("Index");
         
            }
            ViewBag.CategoryErrorMessage = model.CategoryIds.Count == 0 ? "Herhangi bir kategori seçmeden, ürün kaydı yapılamaz" : null;
            ViewBag.ImageErrorMessage = model.ImageUrl == null || model.ImageUrl == "" ? "Resim hatalı!" : null;
            var categories = await _categoryManager.GetActiveCategories();
            model.Categories = categories.Data;
            return View(model);
        }



        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productManager.GetProductWithCategoriesAsync(id);//edit sayfasında seçili kategoriler de görünsün diye.
            ProductViewModel productViewModel = product.Data;
            var model = _mapper.Map<EditProductViewModel>(productViewModel);
            var categories = await _categoryManager.GetActiveCategories();
            model.Categories =  categories.Data;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditProductViewModel model, IFormFile image)
        {

            if (ModelState.IsValid && model.CategoryIds.Count > 0)
            {
                if (image != null)
                {
                    model.ImageUrl = await _imageHelper.UploadImage(image, "products");
                
                }
                model.Url = Jobs.GetUrl(model.Name);
                await _productManager.UpdateAsync(model);
                return RedirectToAction("Index");
            }
            ViewBag.CategoryErrorMessage = model.CategoryIds.Count == 0 ? "Herhangi bir kategori seçmeden, ürün kaydı yapılamaz" : null;
            var categories = await _categoryManager.GetActiveCategories();
            model.Categories = categories.Data;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var product=await  _productManager.GetByIdAsync(id);
            ProductViewModel productViewModel = product.Data;
            var model = _mapper.Map<DeleteProductViewModel>(productViewModel);
            model.ImageUrl= productViewModel.ImageUrl;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> HardDelete(int id)
        {
           await _productManager.HardDeleteAsync(id);
  
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> SoftDelete(int id)
        {
          await _productManager.SoftDeleteAsync(id);
            var tempdataId = TempData["TransferId"];

            return RedirectToAction("Index", new { id = tempdataId });
        }
    }
}
