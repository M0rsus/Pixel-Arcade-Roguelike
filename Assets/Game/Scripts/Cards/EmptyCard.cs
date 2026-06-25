namespace Cards
{
    [System.Serializable]
    public class EmptyCard : Card
    {
        public override Effect GetEffect()
        {
            throw new System.NotImplementedException();
        }
    }
}