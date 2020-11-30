public class Methods 
    {
        public static double method_chord(double A, double B, double e, Polinom p)
        {
            double result = 0;
            do
            {
                result = A - p.ResultInPoint(A) / (p.ResultInPoint(B) - p.ResultInPoint(A)) * (B - A);
                if (p.ResultInPoint(result) * p.ResultInPoint(A) > 0)
                    A = result;
                else B = result;
            }
            while (Math.Abs(result) >= e);
            return result;
        }
        public static double method_half(double A, double B, double e, Polinom p)
        {
            double result;
            do
            {
                result = (A + B) / 2;
                if (p.ResultInPoint(result) * p.ResultInPoint(A) < 0) B = result;
                else A = result;
            }
            while (Math.Abs(A - B) >= e);
            return result;
        }
        public static double method_tangent(double A, double B, double e, Polinom p)
        {
            double result;
            Polinom d = p.Differential();
            Polinom d2 = d.Differential();
            if (p.ResultInPoint(A) * d.ResultInPoint(A) > 0) result = A;
            else result = B;
            do { result = result - p.ResultInPoint(result) / d.ResultInPoint(result); }
            while (Math.Abs(p.ResultInPoint(result)) >= e);
            return result;
        }
    }
