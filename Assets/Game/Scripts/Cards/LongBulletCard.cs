namespace Cards
{
    [System.Serializable]
    public class LongBulletCard : Card
    {
        private readonly Effect _effect = new LongBulletEffect();
        public override Effect GetEffect()
        {
            return _effect;
        }
    }
}