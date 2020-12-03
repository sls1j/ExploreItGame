using GameEngine.GUI.Controls;
using GameEngine.Nouns;
using SDL2;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.Verbs
{
  public class GUI
  {
    private List<Drawable> _drawables;
    private Drawable _focus;
    private SDL.SDL_SystemCursor _cursor;

    public GUI()
    {
      _drawables = new List<Drawable>();
      _cursor = SDL.SDL_SystemCursor.SDL_SYSTEM_CURSOR_ARROW;
    }

    public void AddDrawable(Drawable d)
    {
      Control c = d as Control;
      if (c != null)
        c.IsFocused = () => _focus == c;

      _drawables.Add(d);
    }

    public Drawable Focus
    {
      get => _focus;
      set
      {
        if (value != null && !_drawables.Contains(value))
          throw new InvalidOperationException($"Must add drawable to GUI before giving it focus.");

        _focus = value;
      }
    }

    public void Draw(IntPtr rend)
    {
      SDL.SDL_SystemCursor cursor = SDL.SDL_SystemCursor.SDL_SYSTEM_CURSOR_ARROW;
      for (int i = 0; i < _drawables.Count; i++)
      {
        var d = _drawables[i];
        d?.OnDraw(rend);
        if (d.Cursor != SDL.SDL_SystemCursor.SDL_SYSTEM_CURSOR_ARROW)
          cursor = d.Cursor;
      }

      // set the cursor
      if (cursor != _cursor)
      {
        IntPtr oc = SDL.SDL_GetCursor();
        IntPtr nc = SDL.SDL_CreateSystemCursor(cursor);
        SDL.SDL_SetCursor(nc);
        if (oc != null)
          SDL.SDL_FreeCursor(oc);

        _cursor = cursor;
      }
    }

    public void HandleEvents(SDL.SDL_Event e, Globals g)
    {
      switch (e.type)
      {
        case SDL.SDL_EventType.SDL_KEYDOWN:
          if (e.key.keysym.sym == SDL.SDL_Keycode.SDLK_TAB)
          {
            int index = _drawables.FindIndex(d => d == _focus);
            Drawable newFocus = null;
            for(int i=0; i < _drawables.Count; i++)
            {
              int x = (i + index + 1) % _drawables.Count;
              Control c = _drawables[x] as Control;
              if ( c != null && c.CanFocus)
              {
                newFocus = c;
                break;
              }
            }

            _focus = newFocus;
          }
          else if (_focus != null)
          {
            // only want focused drawable to handle the key presses            
            _focus?.OnEvent(e, g);
          }
          break;
        default:
          for (int i = 0; i < _drawables.Count; i++)
          {
            var d = _drawables[i];
            d?.OnEvent(e, g); ;
          }
          break;
      }
    }

    public void UpdateState(Globals g)
    {
      for (int i = 0; i < _drawables.Count; i++)
      {
        var d = _drawables[i];
        d?.OnState(g);
      }
    }
  }
}
