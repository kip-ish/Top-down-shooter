using UnityEngine;

public class HealthSystem : MonoBehaviour, IDamageable {

    [SerializeField] int _health = 3;
    
    
    public void TakeDamage(int damageAmount) {
        _health -= damageAmount;
    }

    public void Heal(int healAmount) {
        _health += healAmount;
    }

    public bool IsDead() {
        return _health <= 0;
    }

    void Update() {
        if(IsDead()) {
            Destroy(gameObject);
        }
    }
}

public interface IDamageable {
    public void TakeDamage(int damageAmount);
    public void Heal(int healAmount);
    public bool IsDead();
}
