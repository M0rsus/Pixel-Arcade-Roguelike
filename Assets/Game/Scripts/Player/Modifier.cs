namespace Game
{
    public class Modifier<T>
    {
        public T Value { get; private set; }

        public Modifier(T value)
        {
            Value = value;
        }
    }
}