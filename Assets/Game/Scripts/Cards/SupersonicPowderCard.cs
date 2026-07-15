namespace Cards
{
    [System.Serializable]
    public class SupersonicPowderCard : Card
    {
        private readonly Effect _effect = new SupersonicPowderEffect();
        public override Effect GetEffect()
        {
            return _effect;
        }
    }
}