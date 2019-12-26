using ExploreItGame.Nouns;
using SDL2;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ExploreItGame.Verbs
{
  class CollisionVerbs
  {
    public static (Action update, Func<double, double, List<Element>[]> getCloseElements) GetGridBuilder(List<Element> everything)
    {
      const int width = 32;
      const int height = 32;
      const int totalCells = width * height;
      const int maxPixelWidth = 48 * 100;
      const int maxPixelHeight = 48 * 100;

      int[] indexes = new int[] { -width - 1, -width, -width + 1, -1, 0, 1, width - 1, width, width + 1 };

      // build grid
      List<List<Element>> grid = new List<List<Element>>(totalCells);

      List<Element>[] closeElements = new List<Element>[9];

      Func<double, double, int> makeIndex = (x, y) =>
      {
        int xi = (int)x * width / maxPixelWidth;
        int yi = (int)y * height / maxPixelHeight;
        return xi + yi * width;
      };

      for (int i = 0; i < totalCells; i++)
        grid.Add(new List<Element>(10));

      Action update = () =>
      {
        for (int i = 0; i < totalCells; i++)
          grid[i].Clear();

        for (int i = 0; i < everything.Count; i++)
        {
          Element e = everything[i];
          int index = makeIndex(e.x, e.y);
          if (index >= 0 && index < grid.Count)
            grid[index].Add(e);
        }
      };

      Func<double, double, List<Element>[]> getCloseElements = (x, y) =>
      {
        int index = makeIndex(x, y);
        for (int i = 0; i < closeElements.Length; i++)
        {
          int closeIndex = indexes[i] + index;
          if (closeIndex >= 0 && closeIndex < grid.Count)
            closeElements[i] = grid[closeIndex];
          else
            closeElements[i] = null;
        }

        return closeElements;
      };

      return (update, getCloseElements);
    }

    public static List<Collision> FindCollisions(Shape r1, List<Element>[] closeElements)
    {
      List<Collision> collisions = new List<Collision>();
      for (int i = 0; i < closeElements.Length; i++)
      {
        var elements = closeElements[i];
        if (elements == null)
          continue;

        for (int j = 0; j < elements.Count; j++)
        {

          var r2 = elements[j];

          if (r2 == r1)
            continue;

          Collision collision = CollisionVerbs.RectangleCollision(r1, r2);
          if (collision == null)
            continue;

          collisions.Add(collision);
        }
      }

      collisions.Sort((a, b) => (a.y < b.y) ? -1 : 1);
      return collisions;
    }

    public static bool HitTestPoint(double x, double y, Element other)
    {
      switch (other.type)
      {
        case ShapeType.Rectangle:
          {
            double left = other.x;
            double right = other.x + other.w;
            double top = other.y;
            double bottom = other.y + other.h;

            return x > left && x < right && y > top && y < bottom;
          }
        case ShapeType.Circle:
          {
            double xdiff = x - other.x;
            double ydiff = y - other.y;
            double mag = xdiff * xdiff + ydiff * ydiff;
            return mag < other.radius * other.radius;
          }
        default:
          return false;
      }
    }

    public static bool HitTestCircle(Shape c1, Shape c2)
    {
      Debug.Assert(c1.type == ShapeType.Circle);
      Debug.Assert(c2.type == ShapeType.Circle);

      double dx = c1.x - c2.x;
      double dy = c1.y - c1.y;
      double mag = Math.Sqrt(dx * dx + dy * dy);
      return mag < (c1.radius + c2.radius);
    }

    public static Collision RectangleCollision(Shape r1, Shape r2)
    {
      Collision hit = new Collision()
      {
        s = r2,
        collision = null,
        xv = 1,
        yv = 1,
        x = r1.x,
        y = r1.y,
      };

      double vx = r1.xc - r2.xc;
      double combinedHalfWidth = r1.w / 2 + r2.w / 2;

      if (Math.Abs(vx) < combinedHalfWidth)
      {

        double vy = r1.yc - r2.yc;
        double combinedHalfHeight = r1.h / 2 + r2.h / 2;

        if (Math.Abs(vy) < combinedHalfHeight)
        {


          double overlapX = combinedHalfWidth - Math.Abs(vx);
          double overlapY = combinedHalfHeight - Math.Abs(vy);

          if (overlapX >= overlapY)
          {
            //The collision is happening on the X axis
            //But on which side? vy can tell us

            if (vy > 0)
            {
              hit.collision = "top";
              //Move the rectangle out of the collision
              hit.y = r1.y + overlapY;
            }
            else
            {
              hit.collision = "bottom";
              //Move the rectangle out of the collision
              hit.y = r1.y - overlapY;
            }

            hit.yv *= -1;
          }
          else
          {
            //The collision is happening on the Y axis
            //But on which side? vx can tell us

            if (vx > 0)
            {
              hit.collision = "left";
              //Move the rectangle out of the collision
              hit.x = r1.x + overlapX;
            }
            else
            {
              hit.collision = "right";
              //Move the rectangle out of the collision
              hit.x = r1.x - overlapX;
            }

            hit.xv *= -1;
          }
        }
      }

      if (hit.collision != null)
      {
        return hit;
      }
      else
        return null;
    }
  }
}
