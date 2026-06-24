using Game;

namespace Cards
{
    public class EngineCard : BaseCard
    {
        private readonly Effect _effect = new EngineEffect();
        public override Effect GetEffect()
        {
            return _effect;
        }
    }
}