using ExploreItGame.Nouns;
using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExploreItGame.Verbs
{
  class HeroVerbs
  {
    public static void Update(GameState state)
    {
      var hero = state.hero;

      if (state.input.isLeft)
      {
        if (hero.x > 96)
          hero.xv = -6;
        hero.spriteState = (state.time / 100) % 3;
      }
      else if (state.input.isRight)
      {
        if (hero.x < state.ground.Count * 48)
          hero.xv = 6;
        hero.spriteState = (state.time / 100) % 3 + 3;
      }
      else
      {
        hero.xv = 0;
        if (hero.spriteState > 2)
          hero.spriteState = 4;
        else
          hero.spriteState = 1;
      }

      if (state.input.isUp)
      {
        var close = state.grid.getCloseElements(hero.x, hero.y);

        if (CollisionVerbs.FindCollisions(new Shape(hero.x, hero.y + 1, hero.w, hero.h), close).Any(c => c.collision == "bottom"))
          state.hero.yv = -13;
      }

      if (state.input.isFire)
      {
        state.input.isFire = false;
      }
    }

    public static void HandleCollision(GameState state, Element e, List<Collision> collisions)
    {
      if (state.input.isRight || state.input.isLeft)
      {
        var col = collisions.Find(c => c.collision == "left" || c.collision == "right" && c.e.isFixed == false);
        if (col != null)
          col.e.xv = e.xv;
      }

      Physics.DefaultCollisionAction(state,e,collisions);
    }
  }
}
