namespace Cards
{
    [System.Serializable]
    public class EngineCard : Card
    {
        private readonly Effect _effect = new EngineEffect();
        public override Effect GetEffect()
        {
            return _effect;
        }
    }
}