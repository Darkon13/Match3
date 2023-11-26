using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Match3.Core
{
    public static class GameObjectFactory
    {
        public static T Create<T>(GameController gameController) where T : GameObject
        {
            ConstructorInfo constructor = typeof(T).GetConstructor(new Type[] { typeof(GameController) });   

            if(constructor != null)
            {
                return (T)constructor.Invoke(new object[] { gameController });
            }
            else
            {
                return null;
            }
        }
    }
}
