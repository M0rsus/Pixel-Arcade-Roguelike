namespace Cards
{
    [System.Serializable]
    public class PassAmuletCard : Card
    {
        private readonly Effect _effect = new PassAmuletEffect();
        public override Effect GetEffect()
        {
            return _effect;
        }
    }
}