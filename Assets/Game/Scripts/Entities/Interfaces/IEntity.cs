namespace Game
{
    public interface IEntity
    {
        public IDamageable Damageable { get; }
    }
    public interface IEnemy : IEntity { }
    public interface IPlayer : IEntity { }
}