using GameEngine.Nouns;
using GameEngine.Verbs;
using SDL2;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.GUI.Controls
{
  public class Label : Control
  {
    private string text;
    private Sprite textures;

    public Label(IntPtr rend, string text, int x, int y)
    {
      this.x = x;
      this.y = y;
      this.rend = rend;
      this.text = text;
      textures = Draw.BuildMakeText(rend, text, 32, ref Colors.Black, ref Colors.White);

      this.w = textures.src[0].w;
      this.h = textures.src[0].h;

      this.OnDraw = (rend) =>
      {
        Draw.RenderSprite(rend, textures, this.x, this.y, 0);
      };

      CanFocus = false;
    }
  }
}
