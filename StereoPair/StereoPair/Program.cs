﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Geometry;
using Point = Geometry.Point;

namespace StereoPair
{
	class Program
	{
		static void Main(string[] args)
		{
			int sizeX = 500, sizeY = 500;
			var form = new Form
			{
				Text = "Stereo Pair",
				ClientSize = new Size(sizeX * 2, sizeY)
			};
			Drawer drawer = new Drawer(form);
		}
	}
}
