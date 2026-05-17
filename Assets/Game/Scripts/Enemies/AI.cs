namespace Game
{
    public abstract class AI
    {
        public abstract float Angle { get; set; }
        public abstract bool IsMoving { get; set; }
        public abstract void OnUpdate(float deltaTime);
    }
}