using SDL2;
using System;
using System.Collections.Generic;
using System.Text;

using Rect = SDL2.SDL.SDL_Rect;
using Point = SDL2.SDL.SDL_Point;
using ExploreItGame.Verbs;

namespace ExploreItGame.Nouns
{  
  class Element : Shape
  {
    public string id;
    
    public Sprite sprite;
    public int spriteState;
    public int state;    
    
    public bool isFixed;
    public Action<object> log;
    public Action<GameState, Element, List<Collision>> handleCollisions;
 
    private Dictionary<string, object> _properties;

    public T Prop<T>(string name, T defaultValue)
    {
      object v;
      if (_properties.TryGetValue(name, out v))
      {
        return (T)v;
      }
      else
      {
        return defaultValue;
      }
    }

    public Element()
    {
      _properties = new Dictionary<string, object>();
      handleCollisions = Physics.DefaultCollisionAction;
    }    

    public static Element Circle(string id, Sprite sprite, int radius, double x = 0, double y = 0, double xv = 0, double yv = 0, bool isFixed = false)
    {
      return new Element
      {
        id = id,
        type = ShapeType.Circle,
        sprite = sprite,        
        w = radius * 2,
        h = radius * 2,
        x = x,
        y = y,
        xv = xv,
        yv = yv,
        isFixed = isFixed
      };
    }

    public static Element Rectangle(string id, Sprite sprite, double x = 0, double y = 0, double xv = 0, double yv = 0, bool isFixed = false)
    {

      var e = new Element
      {
        id = id,
        type = ShapeType.Rectangle,
        sprite = sprite,
        x = x,
        y = y,
        xv = xv,
        yv = yv,
        isFixed = isFixed
      };

      if (sprite.src.Count == 0)
      {
        int w, h;
        SDL.SDL_QueryTexture(sprite.image, out uint format, out int access, out w, out h);
        e.w = w;
        e.h = h;
      }
      else
      {
        e.w = sprite.src[0].w;
        e.h = sprite.src[0].h;
      }

      return e;
    }

    public override string ToString()
    {
      return $"{id} {type} ({x:0.0},{y:0.0}) : {w:0.0}x{h:0.0}";
    }
  }
}
