namespace Cards
{
    [System.Serializable]
    public class AmmunitionCard : Card
    {
        private readonly Effect _effect = new AmmunitionEffect();
        public override Effect GetEffect()
        {
            return _effect;
        }
    }
}