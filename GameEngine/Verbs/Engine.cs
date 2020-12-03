using GameEngine.Nouns;
using SDL2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GameEngine.Verbs
{
  public delegate void OnInit(IntPtr rend);
  public delegate void OnEventHandler(SDL.SDL_Event e, Globals g);
  public delegate void OnGameState(Globals g);
  public delegate void OnDrawHandler(IntPtr rend);

  public class Engine
  {
    public static void Run(string title, Func<bool> isRunning, OnInit onInit, OnEventHandler onEvent, OnGameState onState, OnDrawHandler onDraw)
    {
      string path = Environment.GetEnvironmentVariable("PATH");
      path += ";" + Path.Combine(Environment.CurrentDirectory, "dlls");
      Environment.SetEnvironmentVariable("PATH", path);

      if (SDL.SDL_Init(SDL.SDL_INIT_VIDEO) < 0)
      {
        Console.WriteLine("Unable to initialize SDL. Error: {0}", SDL.SDL_GetError());
      }
      else
      {
        var window = IntPtr.Zero;
        window = SDL.SDL_CreateWindow(title,
            SDL.SDL_WINDOWPOS_CENTERED,
            SDL.SDL_WINDOWPOS_CENTERED,
            1024,
            800,
            SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE
        );

        if (window == IntPtr.Zero)
        {
          Console.WriteLine("Unable to create a window. SDL. Error: {0}", SDL.SDL_GetError());
        }
        else
        {
          SDL_ttf.TTF_Init();
          IntPtr rend = SDL.SDL_CreateRenderer(window, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED);
          SDL.SDL_SetRenderDrawColor(rend, 255, 255, 255, 255);
          SDL_image.IMG_Init(SDL_image.IMG_InitFlags.IMG_INIT_PNG);

          onInit(rend);
         
          const int frameRate = 60;
          const int frameDelay = 1000 / frameRate;

          uint lastTime = SDL.SDL_GetTicks();
          uint time = lastTime;
          Globals glob = new Globals();

          while (isRunning())
          {

            uint frameStart = SDL.SDL_GetTicks();
            lastTime = time;
            time = frameStart;

            uint frameTime = SDL.SDL_GetTicks() - frameStart;
            if (frameDelay > frameTime)
            {
              uint delay = frameDelay - frameTime;
              SDL.SDL_Delay(delay);
            }

            SDL.SDL_Event e;

            while (SDL.SDL_PollEvent(out e) != 0)
            {
              onEvent(e, glob);
            }

            onState(glob);

            SDL.SDL_SetRenderDrawColor(rend, 255, 255, 255, 255);
            SDL.SDL_RenderClear(rend);

            onDraw(rend);

            SDL.SDL_RenderPresent(rend);
          }
        }

        SDL.SDL_DestroyWindow(window);
        SDL.SDL_Quit();
      }
    }
  }
}
