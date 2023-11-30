using Match3.Core;
using System.Collections.Generic;
using System.Linq;

namespace Match3.GameObjects
{
    public class ObjectPool<T> : GameObject where T : GameObject
    {
        private List<T> _objects;
        private bool _inited;

        public ObjectPool(GameController gameController) : base(gameController) { }

        protected override void OnDisable()
        {
            if(_inited == true)
            {
                foreach(T @object in _objects)
                {
                    @object.Disable();
                }
            }
        }

        public void Init(int objectsCount)
        {
            if(_inited == false)
            {
                _objects = new List<T>();

                for(int i = 0; i < objectsCount; i++)
                {
                    T @object = GameController.CreateObject<T>();
                    @object.Disable();

                    _objects.Add(@object);
                }

                _inited = true;
            }
        }

        public bool TryGetObject(out T @object)
        {
            @object = null;

            if(_inited == true)
            {
                @object = _objects.FirstOrDefault(gameObject => gameObject.IsActive == false);
            }

            return @object != null;
        }

        public bool TryGetObjects(int count, out List<T> objects)
        {
            objects = null;

            if(_inited == true)
            {
                objects = _objects.Where(gameObject => gameObject.IsActive == false).Take(count).ToList();
            }

            return objects != null && objects.Count > 0;
        }
    }
}
