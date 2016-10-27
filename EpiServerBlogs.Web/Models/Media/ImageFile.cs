using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Framework.Blobs;
using EPiServer.Framework.DataAnnotations;

namespace EpiServerBlogs.Web.Models.Media
{
    [ContentType(DisplayName = "ImageFile", GUID = "7a06d47a-2890-4117-a6a2-30e9913c7b14", Description = "")]
    [MediaDescriptor(ExtensionString = "jpg,jpeg,png,gif")]
    public class ImageFile : ImageData
    {
        //Medium 300x160
        [ImageDescriptor(Width = 500, Height = 500)]
        public virtual Blob Medium { get; set; }

        [CultureSpecific]
        [Editable(true)]
        [Display(
            Name = "Alternate text",
            Description = "Description of the image",
            GroupName = SystemTabNames.Content,
            Order = 1)]
        public virtual string AlternateText { get; set; }
    }
}