namespace Game
{
    public class Modifier<T>
    {
        public T Value { get; set; }

        public Modifier(T value)
        {
            Value = value;
        }
    }
}