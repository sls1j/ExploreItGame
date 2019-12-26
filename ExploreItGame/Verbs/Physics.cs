using ExploreItGame.Nouns;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExploreItGame.Verbs
{
  class Physics
  {
    public static void Execute(GameState state)
    {
      state.grid.updateGrid();
      state.everything.ForEach(e =>
      {
        if (e.isFixed)
          return;

        // apply gravigy
        e.yv += 0.5;
        if (e.yv > 6)
          e.yv = 6;

        // apply velocity
        e.x += e.xv;
        e.y += e.yv;

        // apply friction
        e.xv *= 0.9;

        // apply collisions
        var closeElements = state.grid.getCloseElements(e.x, e.y);
        var collisions = CollisionVerbs.FindCollisions(e, closeElements);

        // check boundaries
        e.x = Math.Max(Math.Min(e.x, (state.ground.Count - 1) * 48), 0);
        e.y = Math.Max(Math.Min(e.y, (state.ground.Count - 1) * 48), -50);

        if (e.handleCollisions == null)
          Physics.DefaultCollisionAction(state, e, collisions);
        else
          e.handleCollisions(state, e, collisions);

      });
    }    

    public static void DefaultCollisionAction(GameState state, Element r1, List<Collision> hits)
    {
      for (int i = 0; i < hits.Count; i++)
      {
        Collision hit = hits[i];
        switch (hit.collision)
        {
          case "top":
            r1.y = hit.y;
            r1.yv = hit.yv * 0.7;
            break;
          case "bottom":
            r1.y = hit.y;
            r1.yv = hit.yv * 0.7;
            break;
          case "left":
            r1.x = hit.x;
            r1.xv = hit.xv;
            break;
          case "right":
            r1.x = hit.x;
            r1.xv = hit.xv;
            break;
          default:
            break;
        }
      }
    }
  }
}
