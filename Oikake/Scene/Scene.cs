using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oikake.Actor;
using Oikake.Device;
using Oikake.Util;
namespace Oikake.Scene
{
    enum Scene
    {        
        Title,
        GamePlay,
        Ending,     
        GoodEnding
    }
    interface IGameMediator
    {
        void AddActor(Character character);
        void AddScore();
        void AddScore(int num);
    }
}
