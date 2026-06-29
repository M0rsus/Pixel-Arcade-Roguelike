namespace Game
{
    public class Modifier<T>
    {
        public T Value { get; set; }
        public ModifierType Type { get; private set; }

        public Modifier(T value, ModifierType type)
        {
            Value = value;
            Type = type;
        }
        public enum ModifierType
        {
            Add,
            Subtract,
            Multiply,
            Divide,
        }
    }
}