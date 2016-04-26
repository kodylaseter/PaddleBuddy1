using PaddleBuddy.Core.Models.Map;

namespace PaddleBuddy.Core.DependencyServices
{
    public interface IMapDrawer
    {
        void DrawLine(object points, int width, int color, int zindex);
        void DrawMarker(Point point);
        void MoveCamera(Point p);
        void MoveCameraZoom(Point p, int zoom);
    }
}
