using UnityEngine;

public class HealthSystem : MonoBehaviour, IDamageable {

    [SerializeField] int _health;
    
    public void TakeDamage(int damage) {
        _health -= damage;
        if(_health <= 0) {
            Destroy(gameObject);
        }
    }
}

public interface IDamageable {
    public void TakeDamage(int damage);
}
