using GameEngine.Verbs;
using SDL2;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GameEngine.Nouns
{
  public class Button
  {
    public bool isPressed;
    public bool isEnabled;
    public Sprite label;
    private bool over;
    public int spriteState;
    public Action<Drawable> click;

    public static Drawable Create(int x, int y, Sprite sprite, Action<Drawable> click)
    {
      Drawable button = new Drawable();
      Set(button, x, y, sprite, click);
      return button;
    }

    public static void Set(Drawable button, int x, int y, Sprite sprite, Action<Drawable> click)
    {
      Button other = new Button();

      button.x = x;
      button.y = y;
      button.w = sprite.src[0].w;
      button.h = sprite.src[0].h;
      button.other = other;
      other.isEnabled = true;
      other.isPressed = false;
      other.click = click;
      other.label = sprite;
      other.over = false;      

      button.OnEvent = (e,g) =>
      {        
        switch (e.type)
        {
          case SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN:
            if (Drawable.Within(button, e.button.x, e.button.y))
            {
              other.isPressed = other.isEnabled;
            }
            break;
          case SDL.SDL_EventType.SDL_MOUSEBUTTONUP:
            if (other.isPressed)
            {
              other.isPressed = false;
              if (other.click != null)
              {
                other.click(button);
              }
            }
            break;
          case SDL.SDL_EventType.SDL_MOUSEMOTION:
            if (!Drawable.Within(button, e.button.x, e.button.y))
            {
              other.over = false;
              if (other.isPressed)
              {
                other.isPressed = false;
              }
            }
            break;
        }        
      };

      button.OnState = (g) =>
      {
        int x, y;
        SDL.SDL_GetMouseState(out x, out y);
        if (Drawable.Within(button, x, y))
        {
          g.cursor = SDL.SDL_SystemCursor.SDL_SYSTEM_CURSOR_HAND;
        }
      };

      button.Draw = (rend) =>
      {
        Func<byte, byte> scale = x => (byte)(((other.isPressed) ? 1.0 : 0.6) * x);

        SDL.SDL_Rect r;
        r.x = button.x;
        r.y = button.y;
        r.w = button.w;
        r.h = button.h;
        var src = other.isPressed ? other.label.src[1] : other.label.src[0];
        var dst = Draw.MakeRect(button.x, button.y, button.w, button.h);
        SDL.SDL_RenderCopy(rend, other.label.image, ref src, ref dst);
        //SDL.SDL_SetRenderDrawColor(rend, scale(other.background.r), scale(other.background.g), scale(other.background.b), scale(other.background.a));
        //SDL.SDL_RenderFillRect(rend, ref r);
        //if ( other.label == null )
        //{
        //  other.label = Draw.BuildMakeText(rend, other.text, 12, ref other.forground, ref other.background);
        //}
        //var textw = other.label.src[0];
        //var textSrc = other.label.src[0];
        //var testDest = Draw.MakeRect((button.w - other.label.src[0].w) / 2 + button.x, (button.h - textw.h) + button.y, textw.w, textw.h);
        //SDL.SDL_RenderCopy(rend, other.label.image, ref textSrc, ref testDest);
      };
    }

    public static void SetEnabled(Drawable button, bool isEnabled)
    {
      button.other.isEnabled = false;
    }    
  }
}
