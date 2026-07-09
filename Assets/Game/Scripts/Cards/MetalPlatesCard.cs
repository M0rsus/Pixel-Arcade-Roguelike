namespace Cards
{
    [System.Serializable]
    public class MetalPlatesCard : Card
    {
        private readonly Effect _effect = new MetalPlatesEffect();
        public override Effect GetEffect()
        {
            return _effect;
        }
    }
}