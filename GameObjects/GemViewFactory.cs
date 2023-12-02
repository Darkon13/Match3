using Match3.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Match3.GameObjects
{
    public class GemViewFactory
    {
        private const string CircleTexture = "circle";
        private const string SquareTexture = "square";
        private const string StarTexture = "star";
        private const string TriangleTexture = "triangle";
        private const string LightningTexture = "molniya";

        private const string BombTexture = "bomb";
        private const string HorizontalBonusTexture = "horizontal_bonus";
        private const string VerticalBonusTexture = "vertical_bonus";

        private GameController _gameController;
        private Random _random;
        private List<GemView> _gemViews;

        public GemViewFactory(GameController gameController)
        {
            _gemViews = new List<GemView>();
            _gameController = gameController;
            _random = new Random();

            if (_gameController.TryGetContent(CircleTexture, out Texture2D circle))
                _gemViews.Add(new GemView(Color.Pink, circle));

            if (_gameController.TryGetContent(SquareTexture, out Texture2D square))
                _gemViews.Add(new GemView(Color.Cyan, square));

            if (_gameController.TryGetContent(StarTexture, out Texture2D star))
                _gemViews.Add(new GemView(Color.Blue, star));

            if (_gameController.TryGetContent(TriangleTexture, out Texture2D triangle))
                _gemViews.Add(new GemView(Color.Green, triangle));

            if (_gameController.TryGetContent(LightningTexture, out Texture2D lightning))
                _gemViews.Add(new GemView(Color.Yellow, lightning));
        }

        public GemView GetRandomGemView()
        {
            if(_gemViews.Count > 0)
                return _gemViews[_random.Next(_gemViews.Count)];

            return null;
        }

        public GemView GetBonusGemView<T>(Color color) where T : GemType
        {
            GemView gemView = null;

            if(typeof(T) == typeof(BombGem))
            {
                if(_gameController.TryGetContent(BombTexture, out Texture2D bomb))
                    gemView = new GemView(color, bomb);
            }
            else if (typeof(T) == typeof(HorizontalBonusGem))
            {
                if (_gameController.TryGetContent(HorizontalBonusTexture, out Texture2D horizontalBonus))
                    gemView = new GemView(color, horizontalBonus);
            }
            else if (typeof(T) == typeof(VerticalBonusGem))
            {
                if (_gameController.TryGetContent(VerticalBonusTexture, out Texture2D verticalBonus))
                    gemView = new GemView(color, verticalBonus);
            }

            return gemView;
        }
    }
}
