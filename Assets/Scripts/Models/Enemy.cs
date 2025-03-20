namespace Assets.Scripts.Models
{
    public class Enemy
    {
        public string enemyName { get; set; }
        public float enemyMaxHealth { get; set; }
        public float enemyMovementSpeed { get; set; }
        public bool isAlive { get; set; }
        public int enemyId { get; set; }
    }
}
