﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geometry;

namespace StereoPair
{
	class Camera
	{
		public readonly Point position;
		public readonly Plane plane;

		public Camera(Point _position, Plane _plane)
		{
			position = _position;
			plane = _plane;
		}

		public Point GetDirectionOfView()
		{
			return new Point(0, 0, 0) - position;
		}

		public Point[] GetEyes()
		{
			Point dir = GetDirectionOfView();
			Point vertical = new Point(0, 1, 0);
			Point toEye = dir.CrossProduct(vertical).Normalize(1);
			return new[] {position + toEye, position - toEye};
		}

		public Point2D[][] GetFrames(Polyhedron polyhedron)
		{
			List<Point2D[]> frames = new List<Point2D[]>();
			Point yBasis = GeometryOperations.CentralProjectionVectorOnPlane(new Point(0, 1, 0), plane, position);
			Point xBasis = plane.GetSecondBasisVector(yBasis);
			foreach (var polygon in polyhedron.faces)
			{
				if (this.IsVisible(polyhedron, polygon))
					frames.Add(polygon.CentralProjectionToPlane(plane, position).ConvertTo2D(xBasis, yBasis));
			}
			return frames.ToArray();
		}

		private bool IsVisible(Polyhedron polyhedron, Polygon polygon)
		{
			foreach (var point in polygon.vertices)
			{
				foreach (var face in polyhedron.faces)
				{
					Plane plane = face.GetPlane();
					foreach (var vertex in face.vertices)
					{
						Segment viewSegment = new Segment(position, vertex);
						Point interPoint = plane.Intersect(viewSegment.GetLine()); //TODO: something wrong...
						if (!face.IsInside(interPoint) || !viewSegment.CheckBelongingOfPoint(interPoint))
							return false;
					}
				}
			}
			return true;
		}
	}
}
