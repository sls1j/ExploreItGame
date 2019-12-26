using ExploreItGame.Nouns;
using SDL2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Rect = SDL2.SDL.SDL_Rect;

namespace ExploreItGame.Verbs
{
  class SceneVerb
  {
    private static Func<int, int, int, int, Rect> rect = (x, y, w, h) => new Rect() { x = x, y = y, w = w, h = h };

    public static void InitScene(IntPtr rend, GameState state)
    {

      Func<string, string, Sprite> load = (name, filename) =>
        {
          string fullName = Path.Combine("Resources", filename);
          var texture = SDL_image.IMG_LoadTexture(rend, fullName);
          Sprite d = new Sprite()
          {
            name = name,
            image = texture,
          };
          state.sprites[name] = d;
          return d;
        };

      // load sprites
      load("sky", "sky.png")
        .AddState(0, 0, 4, 800);

      state.sky = new Element() { sprite = state.sprites["sky"], spriteState = 0, x = 0, y = 0, isFixed = true, id = "sky" };

      // background
      load("back", "background1.png")
        .AddState(0, 0, 1280, 800);

      state.background = new Element() { sprite = state.sprites["back"], spriteState = 0, x = 0, y = 0, isFixed = true, id = "back" };

      // ground
      load("ground", "ground.png")
        .AddState(0, 0, 48, 48);

      load("ground-top", "ground-top.png")
        .AddState(0, 0, 48, 48);

      // hero graphics
      load("hero", "Golem.png")
        // left
        .AddState(0 * 0, 1 * 48, 48, 48)
        .AddState(1 * 48, 1 * 48, 48, 48)
        .AddState(2 * 48, 1 * 48, 48, 48)
        // right
        .AddState(0 * 48, 2 * 48, 48, 48)
        .AddState(1 * 48, 2 * 48, 48, 48)
        .AddState(2 * 48, 2 * 48, 48, 48);

      // create level      
      for (int i = 0; i < 100; i++)
      {
        List<Element> column = new List<Element>();
        int start = (int)((Math.Sin(i / 3.0) * 1.5 + Math.Sin(i / 7) * 1.5) + 10);
        bool isFirst = true;
        for (int j = start; j < 20; j++)
        {

          string spriteName;
          if (isFirst)
          {
            isFirst = false;
            spriteName = "ground-top";
          }
          else
            spriteName = "ground";

          Element ground = Element.Rectangle($"ground ({i},{j})", state.sprites[spriteName], 0, 0, 0, 0, true);
          ground.x = i * ground.w;
          ground.y = j * ground.h;
          ground.spriteState = 0;
          column.Add(ground);
          state.everything.Add(ground);
        }
        state.ground.Add(column);
      }

      // create pushable blocks
      Random r = new Random();
      for (int i = 0; i < 10; i++)
      {
        var pushable = Element.Rectangle($"pushable ({i})", state.sprites["ground-top"], r.NextDouble() * 4800);
        state.everything.Add(pushable);
        state.items.Add(pushable);
        pushable.log = (p1) =>
        {
          switch(p1)
          {
            case Collision h:
              {
                Element e = h.s as Element;
                Console.WriteLine($"{e.id} {h.collision} {h.s.y}");
                break;
              }
          }
        };
      }

      // create hero
      state.hero = Element.Rectangle("hero", state.sprites["hero"], state.ground[2][0].x, state.ground[2][0].y - 100);
      state.hero.spriteState = 4;
      state.hero.handleCollisions = HeroVerbs.HandleCollision;
      state.everything.Add(state.hero);
    }

    public static void DrawScene(IntPtr rend, GameState state)
    {
      Action<Element, SDL.SDL_Rect> draw = (element, dest) =>
      {
        SDL.SDL_Rect src = element.sprite.src[element.spriteState];
        SDL.SDL_RenderCopy(rend, element.sprite.image, ref src, ref dest);
      };

      Action<Element> tranDraw = (element) =>
      {
        SDL.SDL_Rect tp = new Rect();
        tp.y = (int)element.y;
        tp.x = (int)(element.x - (state.hero.x - 96));
        tp.w = element.w;
        tp.h = element.h;

        draw(element, tp);
      };

      SDL.SDL_RenderClear(rend);

      // draw the sky
      draw(state.sky, rect(0, 0, 1024, 800));

      // draw the background
      state.background.x = -state.hero.x / 5;
      draw(state.background, rect((int)state.background.x, 0, 1280, 800));

      // draw everything
      for (int i = 0; i < state.everything.Count; i++)
      {
        tranDraw(state.everything[i]);
      }

      SDL.SDL_RenderPresent(rend);
    }
  }
}
