using System;
using EPiServer.DataAnnotations;

namespace EpiServerBlogs.Web.Business.ImageRepository
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ImageScaleDescriptorAttribute : ImageDescriptorAttribute
    {
        public ImageScaleType ScaleMethod { get; set; }

        public ImageScaleDescriptorAttribute()
            : this(48, 48, ImageScaleType.ScaleToFill)
        {
        }

        public ImageScaleDescriptorAttribute(int width, int height, ImageScaleType scaleMethod)
        {
            Height = height;
            Width = width;
            ScaleMethod = scaleMethod;
        }
    }
}