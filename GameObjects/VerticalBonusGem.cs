using Microsoft.Xna.Framework;

namespace Match3.GameObjects
{
    public class VerticalBonusGem : GemType
    {
        public VerticalBonusGem(GemView gemView) : base(gemView) { }

        public override void CallAction(Grid grid, Point point)
        {
            for(int i = 0; i < grid.LineCount; i++)
            {
                Point pointToDestroy = new Point(point.X, i);

                if(pointToDestroy != point)
                    grid.DestroyGem(pointToDestroy);
            }

            CallEvent();
        }
    }
}
