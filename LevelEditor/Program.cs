using GameEngine.Nouns;
using GameEngine.Verbs;
using SDL2;
using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Win32;

namespace LevelEditor
{
  class Program
  {
    static void Main(string[] args)
    {
      bool isRunning = true;

      List<Drawable> buttons = new List<Drawable>();

      void init(IntPtr rend)
      {
        Sprite buttonSprite = Draw.LoadSprite(rend, "buttons", "buttons.png");

        int c = 0;
        int x = 4;
        buttons.Add(Button.Create(x, 4, buttonSprite.New("loadMap").AddState(c * 100, 0, 100, 25).AddState(c * 100 + 100, 0, 100, 25), d =>
              {

              }));

        x += 102;
        c = 4;
        buttons.Add(Button.Create(x, 4, buttonSprite.New("saveMap").AddState(c * 100, 0, 100, 25).AddState(c * 100 + 100, 0, 100, 25), d =>
        {

        }));

        x += 106;
        c = 2;
        buttons.Add(Button.Create(x, 4, buttonSprite.New("loadSprite").AddState(c * 100, 0, 100, 25).AddState(c * 100 + 100, 0, 100, 25), d =>
        {

        }));
      }

      void eventHandler(SDL.SDL_Event e, Globals g)
      {
        buttons.ForEach(b => b.OnEvent(e,g));

        if (e.type == SDL.SDL_EventType.SDL_QUIT)
        {
          isRunning = false;
        }
      }

      void updateState(Globals g)
      {
        buttons.ForEach(b => b.OnState(g));
      }

      void draw(IntPtr rend)
      {
        buttons.ForEach(b => b.Draw(rend));
      }

      Engine.Run("Level Editor", () => isRunning, init, eventHandler, updateState, draw);
    }
  }
}
