using System;
using ExploreItGame.Nouns;
using ExploreItGame.Verbs;
using SDL2;

namespace Hello
{
  class Program
  {
    static void Main(string[] args)
    {
      if (SDL.SDL_Init(SDL.SDL_INIT_VIDEO) < 0)
      {
        Console.WriteLine("Unable to initialize SDL. Error: {0}", SDL.SDL_GetError());
      }
      else
      {
        var window = IntPtr.Zero;
        window = SDL.SDL_CreateWindow("Glummyl",
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
          IntPtr renderer = SDL.SDL_CreateRenderer(window, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED);
          SDL_image.IMG_Init(SDL_image.IMG_InitFlags.IMG_INIT_PNG);

          GameState state = new GameState() { running = true };
          state.grid = CollisionVerbs.GetGridBuilder(state.everything);
          
          SceneVerb.InitScene(renderer, state);
          const int frameRate = 60;
          const int frameDelay = 1000 / frameRate;          
          
          while (state.running)
          {
            uint frameStart = SDL.SDL_GetTicks();
            state.lastTime = state.time;
            state.time = (int)frameStart;

            InputVerbs.Execute(state.input);
            HeroVerbs.Update(state);
            Physics.Execute(state);
            SceneVerb.DrawScene(renderer, state);

            uint frameTime = SDL.SDL_GetTicks() - frameStart;
            if (frameDelay > frameTime)
            {
              uint delay = frameDelay - frameTime;
              SDL.SDL_Delay(delay);
            }

            if (state.input.isQuit)
              state.running = false;            
          }          
        }

        SDL.SDL_DestroyWindow(window);
        SDL.SDL_Quit();
      }
    }
  }
}