using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cinerva.Services.Common.Properties;
using Cinerva.Services.Common.Cities;
using Cinerva.Services.Common.PropertyTypes;
using Cinerva.Web.Models.Properties;
using Cinerva.Services.Common.Properties.Dto;
using Cinerva.Services.Common.Users;
using Microsoft.AspNetCore.Http;
using Cinerva.Services.Common.Blob;
using System.IO;
using Cinerva.Services.Common.PropertyImages;

namespace Cinerva.Web.Controllers
{
    public class PropertyController : Controller
    {
        private readonly string ImageUrl = "https://cinervaphotos.blob.core.windows.net/propertyimages/";
        private readonly IPropertyService propertyService;
        private readonly ICityService cityService;
        private readonly IPropertyTypeService propertyTypeService;
        private readonly IUserService userService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IBlobService blobService;
        private readonly IPropertyImagesService propertyImagesService;
        public PropertyController(
            IPropertyImagesService propertyImagesService, IBlobService blobService,
            IPropertyService propertyService, ICityService cityService,
            IPropertyTypeService propertyTypeService, IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            this.propertyImagesService = propertyImagesService;
            this.blobService = blobService;
            this.propertyService = propertyService;
            this.propertyTypeService = propertyTypeService;
            this.cityService = cityService;
            this.userService = userService;
            this.httpContextAccessor = httpContextAccessor;
        }
        #region [Public Methods]
        public IActionResult Index(int page = 0)
        {
            const int maxItemsOnPage = 5;
            const int descriptionMaxLength = 40;
            var propertiesDtos = propertyService.GetPagedProperties(page);
            int numberOfProperties = propertiesDtos.Item2;

            var indexpropertiesViewModels = propertiesDtos.Item1.Select(x => new IndexPropertyViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Rating = x.Rating,
                Description = x.Description.Substring(0,
                        x.Description.Length < descriptionMaxLength ? x.Description.Length : descriptionMaxLength),

                Address = x.Address,
                Phone = x.Phone,
                TotalRooms = x.TotalRooms,
                DaysForRefund = x.DaysForRefund,

                PropertyType = propertyTypeService.GetPropertyType(x.PropertyTypeId).Type,

                City = cityService.GetCity(x.CityId).Name,

                Admin = $"{userService.GetUser(x.UserId).FirstName} {userService.GetUser(x.UserId).LastName}"

            }).ToList();

            ViewBag.MaxPage = (numberOfProperties / maxItemsOnPage) - (numberOfProperties % maxItemsOnPage == 0 ? 1 : 0);
            ViewBag.Page = page;
            return View(indexpropertiesViewModels);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var createPropertyViewModel = new EditAndCreatePropertyViewModel();

            createPropertyViewModel.Users = userService.GetAllUsers();
            createPropertyViewModel.Cities = cityService.GetAllCities();
            createPropertyViewModel.PropertyTypes = propertyTypeService.GetAllPropertiesTypes();

            return View(createPropertyViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] EditAndCreatePropertyViewModel createPropertyViewModel)
        {
            if (!ModelState.IsValid)
            {
                createPropertyViewModel.Users = userService.GetAllUsers();
                createPropertyViewModel.Cities = cityService.GetAllCities();
                createPropertyViewModel.PropertyTypes = propertyTypeService.GetAllPropertiesTypes();

                return View(createPropertyViewModel);
            }

            var propertyDto = new PropertyDto
            {
                Name = createPropertyViewModel.Name,
                Rating = createPropertyViewModel.Rating,
                Description = DecodeDescription(createPropertyViewModel.Description),
                Address = createPropertyViewModel.Address,
                Phone = createPropertyViewModel.Phone,
                DaysForRefund = createPropertyViewModel.DaysForRefund,
                TotalRooms = createPropertyViewModel.TotalRooms,
                PropertyTypeId = createPropertyViewModel.PropertyTypeId,
                CityId = createPropertyViewModel.CityId,
                UserId = createPropertyViewModel.AdminId
            };

            propertyService.CreateProperty(propertyDto);
            var property = propertyService.GetNewProperty();

            if (property == null) return View(createPropertyViewModel);

            if (createPropertyViewModel.Images == null) return RedirectToAction("Index");

            var imagesName = new List<string>();
            foreach (var image in createPropertyViewModel.Images)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(image.FileName);
                try
                {
                    await AddImageToBolb(fileName, image);
                }
                catch
                {
                    throw;
                }
                imagesName.Add(fileName);

            }

            propertyImagesService.AddImagesToProperty(property.Id, imagesName);

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id < 1) return RedirectToAction("Index");

            var propertyDto = propertyService.GetProperty(id);
            if (propertyDto == null) return RedirectToAction("Index");

            var editProperty = GetCreatePropertyViewModelFromDto(propertyDto);

            editProperty.Cities = cityService.GetAllCities();
            editProperty.PropertyTypes = propertyTypeService.GetAllPropertiesTypes();
            editProperty.Users = userService.GetAllUsers();

            return View(editProperty);
        }
        [HttpPost]
        public IActionResult Edit(EditAndCreatePropertyViewModel propertyViewModel)
        {
            if (!ModelState.IsValid)
            {
                propertyViewModel.Cities = cityService.GetAllCities();
                propertyViewModel.PropertyTypes = propertyTypeService.GetAllPropertiesTypes();
                propertyViewModel.Users = userService.GetAllUsers();
                return View(propertyViewModel);
            }

            var propertyDto = propertyService.GetProperty(propertyViewModel.Id);
            if (propertyDto == null) return RedirectToAction("Index");

            propertyDto.Id = propertyViewModel.Id;
            propertyDto.Name = propertyViewModel.Name;
            propertyDto.Rating = propertyViewModel.Rating;
            propertyDto.Description = DecodeDescription(propertyViewModel.Description);
            propertyDto.Address = propertyViewModel.Address;
            propertyDto.Phone = propertyViewModel.Phone;
            propertyDto.TotalRooms = propertyViewModel.TotalRooms;
            propertyDto.PropertyTypeId = propertyViewModel.PropertyTypeId;
            propertyDto.CityId = propertyViewModel.CityId;
            propertyDto.UserId = propertyViewModel.AdminId;

            propertyService.UpdateProperty(propertyDto);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            if (id < 1) return RedirectToAction("Index");

            var propertyDto = propertyService.GetProperty(id);
            if (propertyDto == null) return RedirectToAction("Index");

            var propertyViewModel = GetDetailsAndDeletePropertyViewModelFromDto(propertyDto);
            propertyViewModel.Images = CreateImagesUrl(propertyImagesService.GetPropertyImages(propertyDto.Id));
            return View(propertyViewModel);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id < 1) return RedirectToAction("Index");

            var propertyDto = propertyService.GetProperty(id);
            if (propertyDto == null) return RedirectToAction("Index");

            var propertyViewModel = GetDetailsAndDeletePropertyViewModelFromDto(propertyDto);
            return View(propertyViewModel);

        }
        [HttpPost]
        public async Task<IActionResult> Delete(DetailsAndDeleteViewModel propertyViewModel)
        {
            if (propertyViewModel.Id < 1) return RedirectToAction("Index");

            var propertyDto = new PropertyDto
            {
                Id = propertyViewModel.Id
            };
            
            var imagesOfProperty = propertyImagesService.GetPropertyImages(propertyDto.Id);
            if (imagesOfProperty == null) return RedirectToAction("Index");
            DeleteImageFromDB(imagesOfProperty);
            foreach (var image in imagesOfProperty)
            {
                await blobService.DeleteBlob(image, "propertyimages");
            }
            propertyService.DeleteProperty(propertyDto);
            return RedirectToAction("Index");
        }
        #endregion
        #region [PRIVATE METHODS]
        private void DeleteImageFromDB(IEnumerable<string> images)
        {
            foreach (var image in images)
            {
                propertyImagesService.DeleteImage(image);
            }
        }
        private async Task<bool> AddImageToBolb(string fileName, IFormFile file)
        {
            if (file == null || file.Length < 1) return false;
            try
            {
                var succes = await blobService.UploadBlob(fileName, file, "propertyimages");
                return true;
            }
            catch
            {
                throw;
            }
        }
        private IEnumerable<string> CreateImagesUrl(IEnumerable<string> urls)
        {
            return urls.Select(x => $"{ImageUrl}{x}")
                .AsEnumerable();
        }
        private static EditAndCreatePropertyViewModel GetCreatePropertyViewModelFromDto(PropertyDto propertyDto)
        {
            if (propertyDto == null) return null;

            return new EditAndCreatePropertyViewModel
            {
                Id = propertyDto.Id,
                Name = propertyDto.Name,
                Rating = propertyDto.Rating,
                Description = propertyDto.Description,
                Address = propertyDto.Address,
                Phone = propertyDto.Phone,
                TotalRooms = propertyDto.TotalRooms,
                PropertyTypeId = propertyDto.PropertyTypeId,
                CityId = propertyDto.CityId,
                AdminId = propertyDto.UserId
            };
        }
        private static string DecodeDescription(string description)
        {
            int positionOfFirstDollarSign = -1, positionOfSecondDollarSign = -1;
            for (int i = 0; i < description.Length; i++)
            {
                if (description[i] == '$')
                    if (positionOfFirstDollarSign == -1)
                    {
                        positionOfFirstDollarSign = i;
                    }
                    else
                    {
                        positionOfSecondDollarSign = i;
                    }
            }
            return description.Substring(positionOfFirstDollarSign + 1, positionOfSecondDollarSign - positionOfFirstDollarSign - 1);
        }
        private DetailsAndDeleteViewModel GetDetailsAndDeletePropertyViewModelFromDto(PropertyDto propertyDto)
        {

            if (propertyDto == null) return null;

            var user = userService.GetUser(propertyDto.UserId);
            return new DetailsAndDeleteViewModel
            {
                Id = propertyDto.Id,
                Name = propertyDto.Name,
                Rating = propertyDto.Rating,
                Description = propertyDto.Description,
                Address = propertyDto.Address,
                Phone = propertyDto.Phone,
                TotalRooms = propertyDto.TotalRooms,
                PropertyType = propertyTypeService.GetPropertyType(propertyDto.PropertyTypeId).Type,
                City = cityService.GetCity(propertyDto.CityId).Name,
                Admin = $"{user.FirstName} {user.LastName}"
            };
        }
        #endregion
    }
}
