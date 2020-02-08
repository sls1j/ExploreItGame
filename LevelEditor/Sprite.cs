using SDL2;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExploreItGame.Nouns
{
  class Sprite
  {
    public string name;
    public IntPtr image;
    public List<SDL.SDL_Rect> src;

    public Sprite()
    {
      src = new List<SDL.SDL_Rect>();
    }

    public Sprite AddState(int x, int y, int w, int h)
    {     
      src.Add(new SDL.SDL_Rect()
      {
        x = x,
        y = y,
        w = w,
        h = h
      });

      return this;
    }
  }
}
