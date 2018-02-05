using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing ;

namespace MotionProfileMapper
{
    class Points
    {
        private List<Point> pts = new List<Point>();

        public float x(int idx)
        {
            return pts[idx].X;
        }
        public List<float> x()
        {
            List<float> r = new List<float>();
            foreach(Point p in pts)
            {
                r.Add(p.X);
            }
            return r;
        }
        public float y(int idx)
        {
            return pts[idx].Y;
        }
        public List<float> y()
        {
            List<float> r = new List<float>();
            foreach (Point p in pts)
            {
                r.Add(p.Y);
            }
            return r;
        }
        public List<Point> points()
        {
            return pts;
        }

        public void add(Point pt)
        {
            pts.Add(pt);
        }

        public void add(float[] x, float[] y)
        {
            for (int i = 0; i <= x.Length-1; i++)
            {
                pts.Add(new Point((int)x[i], (int)y[i]));
            }
        }

        public void clear()
        {
            pts.Clear();
        }
    }


    class Path
    {
        public Points controlPoints = new Points();
        public Points spline = new Points();
        public Points leftTrack = new Points();
        public Points rightTrack = new Points();

        public int trackwidth = 600;
        public int resolution = 1000;

        public void Create()
        {
            float[] xs, ys;

            int eSplineLength = resolution;
            foreach (Point p in controlPoints.points())
            {
                eSplineLength = eSplineLength + (int)Math.Sqrt(p.X * p.X + p.Y * p.Y);
            }

            //clear out the previous point
            spline.clear();
            rightTrack.clear();
            leftTrack.clear();

            //Create spline for center of the bot
            TestMySpline.CubicSpline.FitParametric(controlPoints.x().ToArray(), controlPoints.y().ToArray(), eSplineLength/resolution, out xs, out ys);
            spline.add(xs, ys);

            //calculate the tangent to the line for the left and right track
            for (int i = 1; i < spline.points().Count; i++)
            {
                float x = spline.x(i) - spline.x(i-1);
                float y = spline.y(i) - spline.y(i-1);

                double z = -Math.Sqrt( x*x + y*y);
                int my = -(int)(trackwidth/2 / z * x);
                int mx = (int)(trackwidth/2 / z * y);

                //Create track points 
                rightTrack.add(new Point((int)(spline.x(i) + mx), (int)(spline.y(i) + my)));
                leftTrack.add(new Point((int)(spline.x(i) - mx), (int)(spline.y(i) - my)));
            }
            
        }
    }
}
