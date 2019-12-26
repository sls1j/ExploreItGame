using System;
using System.Collections.Generic;
using System.Text;

namespace ExploreItGame.Nouns
{
  class GameState
  {
    public Dictionary<string, Sprite> sprites;
    public Element sky;
    public Element background;
    public List<List<Element>> ground;
    public List<Element> life;
    public List<Element> items;
    public List<Element> everything;
    public (Action updateGrid, Func<double,double,List<Element>[]> getCloseElements) grid;
    public Element hero;
    public int time;
    public int lastTime;
    public bool running;
    public Input input;
    internal object log;

    public GameState()
    {
      sprites = new Dictionary<string, Sprite>();
      ground = new List<List<Element>>();
      life = new List<Element>();
      items = new List<Element>();
      everything = new List<Element>();
      input = new Input();
    }
  }
}
