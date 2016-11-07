using System.ComponentModel.DataAnnotations;
using EpiServerBlogs.Web.Business.ImageRepository;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Framework.Blobs;
using EPiServer.Framework.DataAnnotations;

namespace EpiServerBlogs.Web.Models.Media
{
    [ContentType(DisplayName = "ImageFile", GUID = "7a06d47a-2890-4117-a6a2-30e9913c7b14", Description = "")]
    [MediaDescriptor(ExtensionString = "jpg,jpeg,png,gif,ico")]
    public class ImageFile : ImageData
    {
        [ImageScaleDescriptor(Width = 500, Height = 500, ScaleMethod = ImageScaleType.ScaleToFitIfNotLessThanDestination)]
        public virtual Blob Medium { get; set; }

        [ImageScaleDescriptor(Width = 300, Height = 300, ScaleMethod = ImageScaleType.ScaleToFill)]
        public virtual Blob Fill { get; set; }

        [ImageScaleDescriptor(Width = 300, Height = 300, ScaleMethod = ImageScaleType.ScaleToFit)]
        public virtual Blob Fit { get; set; }

        [ImageScaleDescriptor(Width = 300, Height = 300, ScaleMethod = ImageScaleType.Resize)]
        public virtual Blob Resize { get; set; }

        [ImageScaleDescriptor]
        public override Blob Thumbnail
        {
            get { return base.Thumbnail; }
            set { base.Thumbnail = value; }
        }

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