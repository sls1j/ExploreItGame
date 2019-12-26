//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace ExploreItGame.Nouns
//{
//  enum MonsterDirection { Down = 0, Left = 1, Right = 2, Up = 3 }

//  class MonsterMap
//  {
//    public static Rect GetMonster(int index, MonsterDirection direction, int state)
//    {
//      const int spriteWidth = 48;
//      const int spriteHeight = 48;

//      index = index % 8;
//      state = state % 3;

//      int x0 = index % 4 * spriteWidth;
//      int y0 = index / 4 * spriteHeight;

//      int y = (int)direction * spriteHeight + y0;
//      int x = (int)state * spriteWidth + x0;
//      Rect r = new Rect(x, y, spriteWidth, spriteHeight);
//      return r;
//    }
//  }
//}
