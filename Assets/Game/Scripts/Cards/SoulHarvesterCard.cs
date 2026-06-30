namespace Cards
{
    [System.Serializable]
    public class SoulHarvesterCard : Card
    {
        private readonly Effect _effect = new SoulHarvesterEffect();
        public override Effect GetEffect()
        {
            return _effect;
        }
    }
}