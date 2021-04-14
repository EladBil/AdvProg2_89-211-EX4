﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FlightSimADVProg2_ex1.Model
{
	/// <summary>
	/// The department calculates calculations needed for the model
	/// Various
	/// average
	/// The regression line
	/// covariance
	/// </summary>
	class anomaly
	{
		public static float avg(List<float> x)
		{
			float sum = 0;
			for (int i = 0; i < x.Count; sum += x[i], i++) ;
			return sum / x.Count;
		}

		// returns the variance of X and Y
		public static float var(List<float> x)
		{
			float av = avg(x);
			float sum = 0;
			for (int i = 0; i < x.Count; i++)
			{
				sum += x[i] * x[i];
			}
			return (sum / x.Count) - (av * av);
		}

		// returns the covariance of X and Y
		public static float cov(List<float> x, List<float> y)
		{
			float sum = 0;
			for (int i = 0; i < x.Count; i++)
			{
				sum += x[i] * y[i];
			}
			sum /= x.Count;

			return sum - avg(x) * avg(y);
		}

		public Line lineReg(List<float> x, List<float> y)
		{
			float a = cov(x, y) / var(x);
			float b = avg(y) - a * avg(x);

			return new Line(a, b);
		}

		public static float pearson(List<float> x, List<float> y)
		{

			return (float)(cov(x, y) / (Math.Sqrt(var(x)) * Math.Sqrt(var(y))));
		}
	}
}