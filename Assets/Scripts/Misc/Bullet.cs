using UnityEngine;

public class Bullet : MonoBehaviour {

    Vector3 _dir;
    float _bulletSpeed;

    [SerializeField] float _bulletRadius = 0.15f;

    Player _player;

    void Update() {
        transform.position += _bulletSpeed * Time.deltaTime * _dir;

        Vector3 screenToWorld = Camera.main.ScreenToWorldPoint(new(Screen.width, Screen.height));
        
        if(transform.position.x < -screenToWorld.x || transform.position.x > screenToWorld.x
            || transform.position.y < -screenToWorld.y || transform.position.y > screenToWorld.y 
        ) {
            DestroyBullet();
        }

        Collider2D hitSomething = Physics2D.OverlapCircle(transform.position, _bulletRadius);
        if(hitSomething != null && hitSomething.TryGetComponent(out IDamageable component)) {
            component.TakeDamage(1);
            _player.ChargeMissile(1);
            DestroyBullet();
        }
    }

    

    void OnDrawGizmos() {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, _bulletRadius);    
    }

    void DestroyBullet() {
        Destroy(gameObject);
    }

    public void SetSender(Player sender) {
        _player = sender;
    }

    public void SetBulletDirection(Vector3 dir) => _dir = dir.normalized;
    public void SetBulletSpeed(float bulletSpeed) => _bulletSpeed = bulletSpeed;
}
