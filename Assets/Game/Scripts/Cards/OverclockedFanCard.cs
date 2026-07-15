namespace Cards
{
    [System.Serializable]
    public class OverclockedFanCard : Card
    {
        private readonly Effect _effect = new OverclockedFanEffect();
        public override Effect GetEffect()
        {
            return _effect;
        }
    }
}