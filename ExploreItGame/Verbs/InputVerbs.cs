using ExploreItGame.Nouns;
using SDL2;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExploreItGame.Verbs
{
  class InputVerbs
  {
    public static void Execute(Input input)
    {
      SDL.SDL_Event e;

      while (SDL.SDL_PollEvent(out e) != 0)
      {
        switch (e.type)
        {
          case SDL.SDL_EventType.SDL_QUIT:
            input.isQuit = true;
            break;

          case SDL.SDL_EventType.SDL_KEYDOWN:
            switch (e.key.keysym.sym)
            {
              case SDL.SDL_Keycode.SDLK_LEFT: input.isLeft = true; break;
              case SDL.SDL_Keycode.SDLK_RIGHT: input.isRight = true; break;
              case SDL.SDL_Keycode.SDLK_UP: input.isUp = true; break;
              case SDL.SDL_Keycode.SDLK_DOWN: input.isDown = true; break;
              case SDL.SDL_Keycode.SDLK_SPACE: input.isFire = true; break;
            }
            break;
          case SDL.SDL_EventType.SDL_KEYUP:
            switch (e.key.keysym.sym)
            {
              case SDL.SDL_Keycode.SDLK_LEFT: input.isLeft = false; break;
              case SDL.SDL_Keycode.SDLK_RIGHT: input.isRight = false; break;
              case SDL.SDL_Keycode.SDLK_UP: input.isUp = false; break;
              case SDL.SDL_Keycode.SDLK_DOWN: input.isDown = false; break;
              case SDL.SDL_Keycode.SDLK_SPACE: input.isFire = false; break;
            }
            break;
        }
      }
    }
  }
}
