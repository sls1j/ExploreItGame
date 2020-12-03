using GameEngine.Nouns;
using GameEngine.Verbs;
using SDL2;
using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using LevelEditor.Nouns;
using GameEngine.GUI.Controls;

namespace LevelEditor
{
  class Program
  {   
    static void Main(string[] args)
    {
      bool isRunning = true;


      GUI gui = new GUI();
      Map map;
      void init(IntPtr rend)
      {
        MakeButtons(rend, gui.AddDrawable, m => map = m);
      }

      void eventHandler(SDL.SDL_Event e, Globals g)
      {
        gui.HandleEvents(e, g);

        if (e.type == SDL.SDL_EventType.SDL_QUIT)
        {
          isRunning = false;
        }
      }

      void updateState(Globals g)
      {
        gui.UpdateState(g);
      }

      void draw(IntPtr rend)
      {
        gui.Draw(rend);
      }

      Engine.Run("Level Editor", () => isRunning, init, eventHandler, updateState, draw);
    }

    private static void MakeButtons(IntPtr rend, Action<Drawable> addDrawable, Action<Map> setMap )
    {
      Sprite buttonSprite = Draw.LoadSprite(rend, "buttons", "buttons.png");
      int c = 0;
      int x = 4;
      addDrawable(new Button(rend, x, 4, buttonSprite.New("loadMap").AddState(c * 100, 0, 100, 26).AddState(c * 100 + 100, 0, 100, 26), d =>
      {
        string mapFile;
        if ( Dialogs.OpenFileDialog("Select map.", Directory.GetCurrentDirectory(), "*.map", out mapFile))
        {

        }
      }));

      x += 102;
      c = 4;
      addDrawable(new Button(rend, x, 4, buttonSprite.New("saveMap").AddState(c * 100, 0, 100, 26).AddState(c * 100 + 100, 0, 100, 26), d =>
      {

      }));

      x += 106;
      c = 2;
      addDrawable(new Button(rend, x, 4, buttonSprite.New("loadSprite").AddState(c * 100, 0, 100, 26).AddState(c * 100 + 100, 0, 100, 26), d =>
      {

      }));

      addDrawable(new Label(rend, "This is a test", 24, 30));
    }
  }
}
