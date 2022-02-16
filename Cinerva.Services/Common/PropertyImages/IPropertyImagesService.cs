using System.Collections.Generic;

namespace Cinerva.Services.Common.PropertyImages
{
    public interface IPropertyImagesService
    {
        void AddImagesToProperty(int propertyId, IEnumerable<string> imagesUrl);
        void DeleteImage(string imageName);
        IEnumerable<string> GetPropertyImages(int propertyId);
    }
}
