using GameEngine.Nouns;
using System;
using System.Collections.Generic;
using System.Text;

namespace LevelEditor.Nouns
{
  class EditorState
  {
    public Dictionary<string, Sprite> sprites;
    public List<Element> items;
    public int time;
    public int lastTime;
    public bool running;
    public Input input;

    public EditorState()
    {
      input = new Input();
      items = new List<Element>();
      sprites = new Dictionary<string, Sprite>();
    }
  }

  
}
