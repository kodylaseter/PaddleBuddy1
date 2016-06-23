using PaddleBuddy.Core.Models.Map;

namespace PaddleBuddy.Core.DependencyServices
{
    public interface IMapDrawer
    {
        void DrawLine(Point[] points);
        void DrawLine(Point start, Point end);
        void DrawMarker(Point point);
        void DrawCurrent(Point current = null);
        void MoveCamera(Point p);
        void MoveCameraZoom(Point p, int zoom);
        void AnimateCameraBounds(Point[] points);
        bool IsMapNull { get; }
    }
}
