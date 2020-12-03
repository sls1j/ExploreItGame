using GameEngine.Nouns;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.GUI.Controls
{
  public class Control : Drawable
  {
    public Func<bool> IsFocused;
    public bool CanFocus { get; protected set; }
  }
}
