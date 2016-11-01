using System;
using System.Collections.Generic;
using EPiServer.Core;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Shell.ObjectEditing.EditorDescriptors;
using EPiServer.Web;

namespace EpiServerBlogs.Web.Business.EditorDescriptors
{
    /// <summary>
    /// Replaces the default editor for image properties
    /// </summary>
    [EditorDescriptorRegistration(TargetType = typeof(ContentReference), UIHint = UIHint.MediaFile)]
    public class ImageEditorDescriptor : EditorDescriptor
    {
        public override void ModifyMetadata(ExtendedMetadata metadata, IEnumerable<Attribute> attributes)
        {
            base.ModifyMetadata(metadata, attributes);

            metadata.ClientEditingClass = "app/Editors/ImageContentSelector";
        }
    }
}