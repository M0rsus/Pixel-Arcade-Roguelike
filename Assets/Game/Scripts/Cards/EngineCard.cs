using Game;

namespace Cards
{
    public class EngineCard : BaseCard
    {
        public override Effect CreateEffect()
        {
            return new EngineEffect();
        }
    }
}