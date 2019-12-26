using System;
using System.Collections.Generic;
using System.Text;

namespace ExploreItGame.Nouns
{
  class Collision
  {
    public Shape s;
    public Element e => (Element)s;
    public string collision;
    public double x, y, xv, yv;
  }
}
