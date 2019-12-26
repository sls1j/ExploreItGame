using System;
using System.Collections.Generic;
using System.Text;

namespace ExploreItGame.Nouns
{
  enum ShapeType { Circle, Rectangle };

  class Shape
  {
    public ShapeType type;
    public double x;
    public double y;
    public int w;
    public int h;
    public int w2 => w / 2;
    public int h2 => h / 2;
    public double xc => x + w / 2;
    public double yc => y + h / 2;

    public double xv, yv; // velocity    
    public int radius => w2;

    public Shape()
    {

    }

    public Shape(double x, double y, int w, int h)
    {
      this.type = ShapeType.Rectangle;
      this.x = x;
      this.y = y;
      this.w = w;
      this.h = h;
    }

    public Shape(double x, double y, int radius)
    {
      this.type = ShapeType.Circle;
      this.x = x;
      this.y = y;
      this.w = radius;
      this.h = radius;
    }
  }
}
