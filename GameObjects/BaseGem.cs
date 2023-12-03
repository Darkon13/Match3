using Microsoft.Xna.Framework;

namespace Match3.GameObjects
{
    public class BaseGem : GemType
    {
        public BaseGem(GemView gemView) : base(gemView) { }

        public override void CallAction(Grid grid, Point point)
        {
            CallEvent();
        }
    }
}
