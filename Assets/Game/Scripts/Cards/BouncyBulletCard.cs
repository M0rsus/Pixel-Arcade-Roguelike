namespace Cards
{
    [System.Serializable]
    public class BouncyBulletCard : Card
    {
        private readonly Effect _effect = new BouncyBulletEffect();
        public override Effect GetEffect()
        {
            return _effect;
        }
    }
}