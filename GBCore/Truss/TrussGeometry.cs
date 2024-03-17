using g4;
using GBCore.Utils;
using System.Collections.Generic;
using System.Linq;

namespace GBCore.Truss
{
    public class TrussGeometry
    {
        private readonly List<Vector3d> _points;
        private Frame3f _frame;
        public TrussGeometry(List<Vector3d> points, Frame3f frame)
        {
            _points = points.OrderBy(p => p.x).ToList();
            _frame = frame;
        }
        public List<Member> GetTopChords()
        {
            var indicies = _points.Select((point, i) => new { i, point })
                .Where(el => el.point.z >= _points[0].z && el.i != 0 && el.i != _points.Count - 1)
                .Select(el => el.i);
            var next = indicies.Skip(1);

            return indicies.Zip(next, (first, second) => new Member(first, second)).ToList();
        }
        public List<Member> GetDiagnals()
        {
            var indicies = _points.Select((point, i) => new { i, point })
                .Where(el => el.i != 0 && el.i != _points.Count - 1)
                .Select(el => el.i);
            var next = indicies.Skip(1);

            return indicies.Zip(next, (first, second) => new Member(first, second)).ToList();
        }
        public List<Member> GetBottomChords()
        {
            var indicies = _points.Select((point, i) => new { i, point })
                .Where(el => el.point.z < _points[0].z && el.i != 0 && el.i != _points.Count - 1)
                .Select(el => el.i);
            var next = indicies.Skip(1);

            return indicies.Zip(next, (first, second) => new Member(first, second)).ToList();
        }

        public List<(double[], double[])> GetTopChordPoints()
        {
            var indicies = _points.Select((point, i) => new { i, point })
                .Where(el => el.point.z >= _points[0].z)
                .Select(el => el.i);
            var next = indicies.Skip(1);

            var transformed = _points.Select(p => _frame.FromFrameP(p)).ToList();

            return indicies.Zip(next, (first, second) =>
            (new double[] { transformed[first].x.Round(4), transformed[first].y.Round(4), transformed[first].z.Round(4) },
            new double[] { transformed[second].x.Round(4), transformed[second].y.Round(4), transformed[second].z.Round(4) }))
                .ToList();
        }
        public List<(double[], double[])> GetDiagonalPoints()
        {
            var indicies = _points.Select((point, i) => new { i, point })
                .Where(el => el.i != 0 && el.i != _points.Count - 1)
                .Select(el => el.i);
            var next = indicies.Skip(1);

            var transformed = _points.Select(p => _frame.FromFrameP(p)).ToList();

            return indicies.Zip(next, (first, second) =>
            (new double[] { transformed[first].x.Round(4), transformed[first].y.Round(4), transformed[first].z.Round(4) },
            new double[] { transformed[second].x.Round(4), transformed[second].y.Round(4), transformed[second].z.Round(4) }))
                .ToList();
        }
        public List<(double[], double[])> GetBottomChordPoints()
        {
            var indicies = _points.Select((point, i) => new { i, point })
                .Where(el => el.point.z < _points[0].z && el.i != 0 && el.i != _points.Count - 1)
                .Select(el => el.i).ToList();
            var next = indicies.Skip(1).ToList();

            var transformed = _points.Select(p => _frame.FromFrameP(p)).ToList();

            return indicies.Zip(next, (first, second) =>
            (new double[] { transformed[first].x.Round(4), transformed[first].y.Round(4), transformed[first].z.Round(4) },
            new double[] { transformed[second].x.Round(4), transformed[second].y.Round(4), transformed[second].z.Round(4) }))
                .ToList();
        }

    }
}
