using GameEngine.Verbs;
using SDL2;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.Nouns
{
  public class Drawable
  {
    public int x;
    public int y;
    public int w;
    public int h;
    public dynamic other;

    public int xc => (x + (w >> 1));
    public int yc => (y + (h >> 1));


    public OnDrawHandler Draw;
    public OnEventHandler OnEvent;
    public OnGameState OnState;

    public static bool Within(Drawable d, int px, int py)
    {
      return (px >= d.x && px <= (d.x + d.w) && py >= d.y && py <= (d.y + d.h));
    }

    public static Collision CollisionTest(Drawable r1, Drawable r2)
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
