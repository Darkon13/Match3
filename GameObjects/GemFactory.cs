using Microsoft.Xna.Framework;

namespace Match3.GameObjects
{
    public class GemFactory
    {
        private GemViewFactory _gemViewFactory;

        public GemFactory(GemViewFactory gemViewFactory)
        {
            _gemViewFactory = gemViewFactory;
        }

        public GemType Create<T>(Color color) where T : GemType
        {
            if(typeof(T) == typeof(BaseGem))
            {
                return new BaseGem(_gemViewFactory.GetRandomGemView());
            }else if(typeof(T) == typeof(BombGem))
            {
                return new BombGem(_gemViewFactory.GetBonusGemView<BombGem>(color));
            }else if(typeof(T) == typeof(HorizontalBonusGem))
            {
                return new HorizontalBonusGem(_gemViewFactory.GetBonusGemView<HorizontalBonusGem>(color));
            }
            else if (typeof(T) == typeof(VerticalBonusGem))
            {
                return new VerticalBonusGem(_gemViewFactory.GetBonusGemView<VerticalBonusGem>(color));
            }

            return null;
        }
    }
}
