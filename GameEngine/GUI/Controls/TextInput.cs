using GameEngine.Nouns;
using GameEngine.Verbs;
using SDL2;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.GUI.Controls
{
  public class TextInput : Control
  {
    private string text;
    private int cursorTime;
    private Sprite currentTextImage;
    private int cursorIndex;
    private int selectionStart;
    private int selectionEnd;

    public TextInput(IntPtr rend, int x, int y, int w, int h, int fontSize, string defaultValue)
    {
      Drawable d = new Drawable(x, y, w, h);
      this.text = defaultValue ?? string.Empty;
      this.cursorTime = 500;
      CanFocus = true;

      this.OnEvent = HandleOnEvent;
      this.OnState = HandleOnState;
      this.OnDraw = HandleDraw;
    }

    private void HandleOnState(Globals g)
    {
      throw new NotImplementedException();
    }
    private void HandleDraw(IntPtr rend)
    {
      // draw box       
      Draw.DrawRect(rend, x + 1, y + 1, w - 2, h - 2, Colors.White);
      Draw.DrawRect(rend, x, y, w, h, Colors.Gray);

      // draw cursor
      // text
      // draw selection
    }

    private void HandleOnEvent(SDL.SDL_Event e, Globals g)
    {
      // keyboard events
      // handle CTRL-A
      // handle CTRL-C
      // handle CTRL-V
      // handle CTRL-X
      // handle DELETE
      // handle BACKSPACE
      // handle LEFT ARROW
      // handle LEFT SHIFT
      // handle RIGHT ARROW
      // handle RIGHT SHIFT
      // handle HOME
      // handle HOME SHIFT
      // handle END
      // handle END SHIFT
      // handle PRINTABLE

      // select - when selected and printable character typed it replaces the text

      // mouse events
      // double click (select word)
      // click set cursor position
      // 
    }

    public string GetText()
    {
      return text;
    }
  }
}
