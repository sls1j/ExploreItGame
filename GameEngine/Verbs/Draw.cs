using GameEngine.Nouns;
using SDL2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GameEngine.Verbs
{
  public class Draw
  {
    /// <summary>
    /// Makes an SDL rectangle
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="w"></param>
    /// <param name="h"></param>
    /// <returns></returns>
    public static SDL.SDL_Rect MakeRect(int x, int y, int w, int h)
    {
      return new SDL.SDL_Rect()
      {
        x = x,
        y = y,
        w = w,
        h = h
      };
    }

    public static SDL.SDL_Color MakeColor(byte r, byte g, byte b)
    {
      return new SDL.SDL_Color()
      {
        a = 255,
        r = r,
        g = g,
        b = b
      };
    }

    public static SDL.SDL_Color MakeColor(byte r, byte g, byte b, byte a)
    {
      return new SDL.SDL_Color()
      {
        a = a,
        r = r,
        g = g,
        b = b
      };
    }

    /// <summary>
    /// Builds a text texture
    /// </summary>
    /// <param name="text"></param>
    /// <param name="size"></param>
    /// <param name="renderer"></param>
    /// <returns></returns>
    public static Sprite BuildMakeText(IntPtr renderer, string text, int size, ref SDL.SDL_Color foreground, ref SDL.SDL_Color background)
    {
      unsafe
      {
        string fontPath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "font.ttf");
        if (!File.Exists(fontPath))
          Console.WriteLine("No font!");

        IntPtr font = SDL_ttf.TTF_OpenFont(fontPath, size);        

        SDL.SDL_Surface* s = (SDL.SDL_Surface*)SDL_ttf.TTF_RenderText_Shaded(font, text, foreground, background);
        IntPtr texture = SDL.SDL_CreateTextureFromSurface(renderer, (IntPtr)s);

        Sprite sprite = new Sprite()
        {
          image = texture,
          src = new List<SDL.SDL_Rect>() { MakeRect(0, 0, s->w, s->h) },
          name = "text"
        };

        SDL.SDL_FreeSurface((IntPtr)s);
        SDL_ttf.TTF_CloseFont(font);

        return sprite;
      }
    }

    public static void DrawRect(IntPtr renderer, int x, int y, int w, int h, SDL.SDL_Color color)
    {
      SDL.SDL_SetRenderDrawColor(renderer, color.r, color.g, color.b, color.a);
      var rect = MakeRect(x, y, w, h);
      SDL.SDL_RenderFillRect(renderer, ref rect);
    }

    public static Sprite LoadSprite(IntPtr rend, string name, string filename)
    {
      string fullName = Path.Combine("Resources", filename);
      var texture = SDL_image.IMG_LoadTexture(rend, fullName);
      Sprite d = new Sprite()
      {
        name = name,
        image = texture,
      };      
      return d;
    }
  }
}
