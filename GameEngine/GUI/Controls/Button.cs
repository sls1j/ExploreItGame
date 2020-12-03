using GameEngine.Nouns;
using GameEngine.Verbs;
using SDL2;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GameEngine.GUI.Controls
{
  public class Button : Control
  {
    private bool isPressed;
    private bool isEnabled;
    private Sprite label;
    private bool over;
    private int spriteState;
    private Action<Button> click;

    public Button(IntPtr rend, int x, int y, Sprite sprite, Action<Button> click)
    {
      this.rend = rend;
      this.x = x;
      this.y = y;
      this.w = sprite.src[0].w;
      this.h = sprite.src[0].h;
      label = sprite;
      this.click = click;
      isEnabled = true;
      isPressed = false;
      over = false;
      CanFocus = true;

      this.OnEvent = HandleOnEvent;
      this.OnState = HandleOnState;
      this.OnDraw = HandleDraw;
    }

    private void HandleOnEvent(SDL.SDL_Event e, Globals g)
    {
      switch (e.type)
      {
        case SDL.SDL_EventType.SDL_KEYDOWN:
          switch(e.key.keysym.sym)
          {
            case SDL.SDL_Keycode.SDLK_SPACE:
            case SDL.SDL_Keycode.SDLK_RETURN:
            case SDL.SDL_Keycode.SDLK_KP_ENTER:
              if ( click != null )
              {
                click(this);
              }
              break;
          }
          break;
        case SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN:
          if (Drawable.Within(this, e.button.x, e.button.y))
          {
            isPressed = isEnabled;
          }
          break;
        case SDL.SDL_EventType.SDL_MOUSEBUTTONUP:
          if (isPressed)
          {
            isPressed = false;
            if (click != null)
            {
              click(this);
            }
          }
          break;
        case SDL.SDL_EventType.SDL_MOUSEMOTION:
          if (!Drawable.Within(this, e.button.x, e.button.y))
          {
            over = false;
            if (isPressed)
            {
              isPressed = false;
            }
          }
          break;
      }
    }

    private void HandleOnState(Globals g)
    {
      int x, y;
      SDL.SDL_GetMouseState(out x, out y);
      if (Drawable.Within(this, x, y))
      {
        this.Cursor = SDL.SDL_SystemCursor.SDL_SYSTEM_CURSOR_HAND;
      }
      else
      {
        this.Cursor = SDL.SDL_SystemCursor.SDL_SYSTEM_CURSOR_ARROW;
      }
    }

    private void HandleDraw(IntPtr rend)
    {
      Func<byte, byte> scale = x => (byte)(((isPressed) ? 1.0 : 0.6) * x);

      SDL.SDL_Rect r;
      r.x = this.x;
      r.y = this.y;
      r.w = this.w;
      r.h = this.h;
      var src = isPressed ? label.src[1] : label.src[0];
      var dst = GameEngine.Verbs.Draw.MakeRect(this.x, this.y, this.w, this.h);
      SDL.SDL_RenderCopy(rend, label.image, ref src, ref dst);
      if ( IsFocused())
      {
        SDL.SDL_SetRenderDrawColor(rend, Colors.Gray.r, Colors.Gray.g, Colors.Gray.b, 255);
        SDL.SDL_RenderDrawRect(rend, ref dst);
      }
    }


    public void SetEnabled(bool isEnabled)
    {
      isEnabled = false;
    }
  }
}
