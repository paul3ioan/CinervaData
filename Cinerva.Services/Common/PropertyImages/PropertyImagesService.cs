using Cinerva.Data;
using Cinerva.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Cinerva.Services.Common.PropertyImages
{
    public class PropertyImagesService : IPropertyImagesService
    {
        private readonly CinervaDBContext dbContext;
        public PropertyImagesService(CinervaDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void AddImagesToProperty(int propertyId, IEnumerable<string> imagesUrl)
        {
            if (imagesUrl.Count() == 0) return;

            foreach (var url in imagesUrl)
            {
                var newImage = new PropertyImage
                {
                    ImageUrl = url,
                    PropertyId = propertyId
                };
                dbContext.PropertyImages.Add(newImage);
            }
            dbContext.SaveChanges();
        }
        public IEnumerable<string> GetPropertyImages(int propertyId)
        {
            if (propertyId < 1) return null;

            return dbContext.PropertyImages
                .Where(x => x.PropertyId == propertyId)
                .Select(x => x.ImageUrl)
                .ToList();
        }
        public void DeleteImage(string imageName)
        {
            var imageToBeDeleted = dbContext.PropertyImages.FirstOrDefault(x => x.ImageUrl == imageName);
            if (imageToBeDeleted == null) return;

            dbContext.PropertyImages.Remove(imageToBeDeleted);
            dbContext.SaveChanges();
        }
    }
}
