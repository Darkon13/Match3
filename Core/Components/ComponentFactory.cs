namespace Match3.Core.Components
{
    public static class ComponentFactory
    {
        public static Component GetComponent<T>(GameObject gameObject, GameController gameController) where T : Component
        {
            if(typeof(T) == typeof(SpriteRenderer))
            {
                return new SpriteRenderer(gameObject, gameController.DefaultTexture);
            }
            else if(typeof(T) == typeof(Transform))
            {
                return new Transform(gameObject);
            }
            else if (typeof(T) == typeof(Animator))
            {
                return new Animator(gameObject, gameObject.CreateTimer(1f));
            }
            else
            {
                return null;
            }
        }
    }
}
