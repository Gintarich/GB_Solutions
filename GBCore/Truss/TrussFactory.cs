using g4;
using System;
using System.Collections.Generic;

namespace GBCore.Truss
{
    public static class TrussFactory
    {
        public static TrussGeometry GetSimpleTruss(TrussGeometrySettings settings)
        {
            var points = new List<Vector3d>();
            Vector3f x = new Vector3f(settings.EndPoint - settings.StartPoint).Normalized;
            var frame = new Frame3f(settings.StartPoint);
            frame.AlignAxis(0, x);

            var localSP = frame.ToFrameP(settings.StartPoint);
            localSP.Round(2);
            var localEP = frame.ToFrameP(settings.EndPoint);
            localEP.Round(2);
            var tang = Math.Tan(settings.Angle * Math.PI / 180);
            var offsetetZ = settings.FirstDiagonalOffset * tang;

            points.Add(localSP);
            points.Add(localEP);

            var newStartPoint = new Vector3d(settings.FirstDiagonalOffset,
                0, offsetetZ);

            var newEndPoint = new Vector3d(localEP.x - settings.FirstDiagonalOffset,
                0, offsetetZ);

            double slopeHeight = localEP.x / 2 * tang;
            slopeHeight = Math.Round(slopeHeight, 2);

            var midpoint = new Vector3d(localEP.x / 2, 0, slopeHeight);

            // Create top chord points in forward direction
            var forvardSlope = (midpoint - newStartPoint) / settings.Sections;
            var index = points.Count;
            points.Add(newStartPoint);
            for (int i = index; i < index + settings.Sections - 1; i++)
            {
                var nextPoint = points[i] + forvardSlope;
                points.Add(nextPoint);
            }

            // Create top chord points in backward direction
            index = points.Count;
            points.Add(newEndPoint);
            var backvardSlope = (midpoint - newEndPoint) / settings.Sections;
            for (int i = index; i < index + settings.Sections - 1; i++)
            {
                var nextPoint = points[i] + backvardSlope; ;
                points.Add(nextPoint);
            }
            points.Add(midpoint);

            // create bottom points
            double bottomChordZValue = midpoint.z - settings.Height;
            // Truss height has to be larger than 500
            if (bottomChordZValue > localSP.z - 500)
            {
                bottomChordZValue = localSP.z - 500;
            }

            double bottomChordSPXValue = (forvardSlope.x / 2) + settings.FirstDiagonalOffset;

            var bottomChordSP = new Vector3d(bottomChordSPXValue,
                0, bottomChordZValue);

            points.Add(bottomChordSP);
            index = points.Count - 1;
            for (int i = index; i < index + settings.Sections + settings.Sections - 1; i++)
            {
                var nextPoint = points[i] + new Vector3d(forvardSlope.x, forvardSlope.y, 0);
                points.Add(nextPoint);
            }

            return new TrussGeometry(points, frame);
        }

        public static TrussGeometry GenerateTrussForRhino(double[] startPoint, double[] endPoint,
            double angle, double height, int sections, double firstDiagonalOffset)
        {
            var settings = new TrussGeometrySettings
            {
                StartPoint = new Vector3d(startPoint[0], startPoint[1], startPoint[2]),
                EndPoint = new Vector3d(endPoint[0], endPoint[1], endPoint[2]),
                Angle = angle,
                Height = height,
                Sections = sections,
                FirstDiagonalOffset = firstDiagonalOffset
            };
            return GetSimpleTruss(settings);
        }
    }
}
