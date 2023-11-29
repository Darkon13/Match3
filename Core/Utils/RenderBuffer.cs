using Match3.Core.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3.Core.Utils
{
    public class RenderBuffer<T> 
    {
        private Dictionary<Rectangle, T> _buffer;

        public RenderBuffer()
        {
            _buffer = new Dictionary<Rectangle, T>();
        }

        public bool Register(Rectangle rectangle, T @object)
        {
            if(rectangle.IsEmpty == false && @object != null)
            {
                if (_buffer.ContainsKey(rectangle))
                {
                    _buffer[rectangle] = @object;
                }
                else
                {
                    _buffer.Add(rectangle, @object);
                }

                return true;   
            }

            return false;
        }

        public bool TryGetObjects(Point point, out List<T> objects)
        {
            objects = new List<T>();

            foreach(Rectangle rectangle in _buffer.Keys)
            {
                if (rectangle.Contains(point) == true)
                {
                    objects.Add(_buffer[rectangle]);
                }
            }

            return objects.Count > 0;
        }

        public void Clear()
        {
            _buffer.Clear();
        }
    }
}
