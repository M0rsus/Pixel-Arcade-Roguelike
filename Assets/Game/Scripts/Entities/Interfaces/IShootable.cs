namespace Game
{
    public interface IShootable
    {
        public event System.Action<bool> OnShoot;
    }
}