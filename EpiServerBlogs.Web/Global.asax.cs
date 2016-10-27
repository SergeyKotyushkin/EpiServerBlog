using System.Web.Mvc;
using EpiServerBlogs.Web.Business.StructureMap;

namespace EpiServerBlogs.Web
{
    public class EPiServerApplication : EPiServer.Global
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            StructureMapFactory.Init();
            //Tip: Want to call the EPiServer API on startup? Add an initialization module instead (Add -> New Item.. -> EPiServer -> Initialization Module)
        }
    }
}