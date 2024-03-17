using System;
using System.Diagnostics;
using g4;
using GBCore.Truss;

namespace CommandFactory.Commands
{
    public class CreateTrussCommand : IGBCommand
    {
        public void Run()
        {
            Console.WriteLine("Is this called");
            var sw = Stopwatch.StartNew();
            try
            {
                var v =
                    new Vector3d(
                        Math.Cos(MathUtil.Deg2Rad * 30),
                        Math.Sin(MathUtil.Deg2Rad * 30),
                        0
                    ) * 24;
                var len = v.Length;

                TrussGeometrySettings settings = new TrussGeometrySettings
                {
                    StartPoint = new Vector3d(0.0, 0.0, 0.0),
                    EndPoint = new Vector3d(0.0, 24000.0, 0.0),
                    Angle = 9.4623,
                    Height = 5000.0,
                    Sections = 2,
                    FirstDiagonalOffset = 200
                };

                // 9.4623 angle
                var truss = TrussFactory.GetSimpleTruss(settings);
                //sw.Stop();
                //Console.WriteLine($"Took {sw.ElapsedMilliseconds}ms");
                //sw.Restart();
                _ = truss.GetTopChords();
                _ = truss.GetBottomChords();
                _ = truss.GetDiagnals();
            }
            finally
            {
                Console.WriteLine($"Took {sw.ElapsedMilliseconds}ms");
            }
        }
    }
}
