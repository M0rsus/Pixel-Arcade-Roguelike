namespace Cards
{
    [System.Serializable]
    public class ReboundingAmuletCard : Card
    {
        private readonly Effect _effect = new ReboundingAmuletEffect();
        public override Effect GetEffect()
        {
            return _effect;
        }
    }
}