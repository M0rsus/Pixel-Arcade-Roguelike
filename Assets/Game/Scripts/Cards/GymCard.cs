namespace Cards
{
    [System.Serializable]
    public class GymCard : Card
    {
        private readonly Effect _effect = new GymEffect();
        public override Effect GetEffect()
        {
            return _effect;
        }
    }
}