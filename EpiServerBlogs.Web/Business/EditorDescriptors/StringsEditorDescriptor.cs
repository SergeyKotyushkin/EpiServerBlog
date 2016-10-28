using System;
using System.Collections.Generic;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Shell.ObjectEditing.EditorDescriptors;

namespace EpiServerBlogs.Web.Business.EditorDescriptors
{
    [EditorDescriptorRegistration(TargetType = typeof(IList<string>), UIHint = Global.SiteUiHints.Strings)]
    public class StringListEditorDescriptor : EditorDescriptor
    {
        public override void ModifyMetadata(ExtendedMetadata metadata, IEnumerable<Attribute> attributes)
        {
            ClientEditingClass = "app/Editors/stringlist/Editor";

            base.ModifyMetadata(metadata, attributes);
        }
    }
}