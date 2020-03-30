using ToolkitCore.Models;

namespace ToolkitCore.Controllers
{
    public static class ViewerController
    {
        public static Viewer GetViewer(string Username)
        {
            return Viewers.All.Find(vwr => vwr.Username == Username) ?? new Viewer(Username);
        }
    }
}
