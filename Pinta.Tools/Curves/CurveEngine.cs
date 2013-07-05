﻿// 
// CurveEngine.cs
//  
// Author:
//       Andrew Davis <andrew.3.1415@gmail.com>
// 
// Copyright (c) 2013 Andrew Davis, GSoC 2013
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cairo;

namespace Pinta.Tools
{
	public class CurveEngine
	{
		public List<List<ControlPoint>> givenPointsCollection = new List<List<ControlPoint>>();
		public List<PointD[]> generatedPointsCollection = new List<PointD[]>();
		public List<Dictionary<double, Dictionary<double, List<OrganizedPoint>>>> organizedPointsCollection =
			new List<Dictionary<double, Dictionary<double, List<OrganizedPoint>>>>();

		public CurveEngine()
		{
			givenPointsCollection.Add(new List<ControlPoint>());
			generatedPointsCollection.Add(new PointD[0]);
			organizedPointsCollection.Add(new Dictionary<double, Dictionary<double, List<OrganizedPoint>>>());
		}

		public CurveEngine Clone()
		{
			CurveEngine clonedCE = new CurveEngine();

			clonedCE.givenPointsCollection[0] = givenPointsCollection[0].Select(i => i.Clone()).ToList();
			clonedCE.generatedPointsCollection[0] = generatedPointsCollection[0].Select(i => new PointD(i.X, i.Y)).ToArray();

			foreach (KeyValuePair<double, Dictionary<double, List<OrganizedPoint>>> xSection in organizedPointsCollection[0])
			{
				//This must be created each time to ensure that it is fresh for each loop iteration.
				Dictionary<double, List<OrganizedPoint>> tempSection = new Dictionary<double, List<OrganizedPoint>>();

				foreach (KeyValuePair<double, List<OrganizedPoint>> section in xSection.Value)
				{
					tempSection.Add(section.Key,
						section.Value.Select(i => new OrganizedPoint(new PointD(i.Position.X, i.Position.Y), i.Index)).ToList());
				}

				clonedCE.organizedPointsCollection[0].Add(xSection.Key, tempSection);
			}

			return clonedCE;
		}
	}
}
