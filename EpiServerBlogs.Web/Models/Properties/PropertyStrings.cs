using EPiServer.Framework.DataAnnotations;
using EPiServer.PlugIn;

namespace EpiServerBlogs.Web.Models.Properties
{
    /// <summary>
    /// Custom PropertyData implementation
    /// </summary>
    [EditorHint(Global.SiteUiHints.Strings)]
    [PropertyDefinitionTypePlugIn]
    public class PropertyStrings : PropertyListBase<string>
    {

    }
}
