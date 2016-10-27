using System;
using System.Collections.Generic;
using System.IO;
using EpiServerBlogs.Web.Business.ImageRepository;
using EPiServer;
using EPiServer.Core.Internal;
using EPiServer.DataAnnotations;
using EPiServer.Framework.Blobs;
using EPiServer.ImageLibrary;
using EPiServer.Web;
using EPiServer.Web.Internal;
using EPiServer.Framework;

namespace EpiServerBlogs.Logic.ImageRepository
{
    public class ExtendedThumbnailManager : ThumbnailManager
    {
        private readonly IBlobFactory _blobFactory;

        public ExtendedThumbnailManager(IContentRepository contentRepository, IBlobFactory blobFactory,
            IBlobResolver blobResolver, IBlobAssigner blobAssigner)
            : base(contentRepository, blobFactory, blobResolver, blobAssigner)
        {
            _blobFactory = blobFactory;
        }

        public override Blob CreateImageBlob(Blob sourceBlob, string propertyName,
            ImageDescriptorAttribute descriptorAttribute)
        {
            Validator.ThrowIfNull("sourceBlob", sourceBlob);
            Validator.ThrowIfNullOrEmpty("propertyName", propertyName);
            Validator.ThrowIfNull("descriptorAttribute", descriptorAttribute);

            var uriString = string.Format("{0}{1}_{2}{3}", new object[]
            {
                Blob.GetContainerIdentifier(sourceBlob.ID).ToString(),
                Path.GetFileNameWithoutExtension(sourceBlob.ID.LocalPath),
                propertyName,
                Path.GetExtension(sourceBlob.ID.LocalPath)
            });
            var customDescriptorAttribute = descriptorAttribute as ImageScaleDescriptorAttribute;
            return customDescriptorAttribute == null
                ? CreateBlob(new Uri(uriString), sourceBlob, descriptorAttribute.Width, descriptorAttribute.Height)
                : CreateScaledBlob(new Uri(uriString), sourceBlob, customDescriptorAttribute);
        }

        private Blob CreateScaledBlob(Uri thumbnailUri, Blob blobSource,
            ImageScaleDescriptorAttribute imageDescriptorAttribute)
        {
            switch (imageDescriptorAttribute.ScaleMethod)
            {
                case ImageScaleType.Resize:
                    var imgOperation = new ImageOperation(ImageEditorCommand.Resize, imageDescriptorAttribute.Width,
                        imageDescriptorAttribute.Height);
                    return CreateBlob(thumbnailUri, blobSource, new List<ImageOperation> {imgOperation},
                        MimeMapping.GetMimeMapping(blobSource.ID.LocalPath));
                case ImageScaleType.ScaleToFit:
                    return CreateBlob(thumbnailUri, blobSource, imageDescriptorAttribute.Width,
                        imageDescriptorAttribute.Height);
                default:
                    var imgOperations = CreateImageOperations(blobSource, imageDescriptorAttribute.Width,
                        imageDescriptorAttribute.Height);

                    return CreateBlob(thumbnailUri, blobSource, imgOperations,
                        MimeMapping.GetMimeMapping(blobSource.ID.LocalPath));
            }
        }

        private IEnumerable<ImageOperation> CreateImageOperations(Blob blobSource, int width, int height)
        {
            var imgOperations = new List<ImageOperation>();
            int orgWidth;
            int orgHeight;
            using (var stream = blobSource.OpenRead())
            {
                var image = System.Drawing.Image.FromStream(stream, false);

                orgWidth = image.Width;
                orgHeight = image.Height;

                image.Dispose();
            }

            var scaleFactor = Math.Max((double) width/orgWidth, (double) height/orgHeight);

            var tempWidth = (int) (orgWidth*scaleFactor);
            var tempHeight = (int) (orgHeight*scaleFactor);

            imgOperations.Add(new ImageOperation(ImageEditorCommand.ResizeKeepScale, tempWidth, tempHeight));
            imgOperations.Add(new ImageOperation(ImageEditorCommand.Crop, width, height)
            {
                Top = (tempHeight - height)/2,
                Left = (tempWidth - width)/2
            });

            return imgOperations;
        }

        private Blob CreateBlob(Uri thumbnailUri, Blob blobSource, IEnumerable<ImageOperation> imgOperations,
            string mimeType)
        {
            byte[] buffer;
            using (Stream stream = blobSource.OpenRead())
            {
                var numArray = new byte[stream.Length];
                stream.Read(numArray, 0, (int) stream.Length);
                buffer = ImageService.RenderImage(numArray,
                    imgOperations,
                    mimeType, 1f, 50);
            }
            Blob blob = _blobFactory.GetBlob(thumbnailUri);
            using (Stream stream = blob.OpenWrite())
            {
                stream.Write(buffer, 0, buffer.Length);
                stream.Flush();
            }
            return blob;
        }
    }
}