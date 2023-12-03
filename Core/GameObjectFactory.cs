using System;
using System.Reflection;

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
