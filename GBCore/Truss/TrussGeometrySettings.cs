using g4;

namespace GBCore.Truss
{
    public class TrussGeometrySettings
    {
        public Vector3d StartPoint { get; set; }
        public Vector3d EndPoint { get; set; }
        public double Angle { get; set; }
        public double Height { get; set; }
        public int Sections { get; set; }
        public double FirstDiagonalOffset { get; set; }
    }
}
