using UnityEngine;

public class Player : MonoBehaviour {

    PlayerInput _input;
    public static Player Instance {get; private set;}

    [Header("Movement Settings")]
    [SerializeField] float _clampPosOffset;
    [SerializeField] float _acceleration;
    [SerializeField] float _drag;
    [SerializeField] float _maxSpeed;
    Vector2 _velocity;


    [Space]
    [Header("Bullet Settings")]
    [SerializeField] Transform _bulletSpawnPos;
    [SerializeField] bool _unlimitedBullet = true;
    [SerializeField] float _bulletSpeed;
    [SerializeField] float _bulletSpread;
    [SerializeField] int _bulletAmount;
    [SerializeField] Bullet _bullet;

    [Space]
    [Header("Fire Settings")]
    [SerializeField] float _fireCooldown;
    

    int _missile;

    float _lastFire;

    void Awake() {
        if(Instance == null) Instance = this;
        _input = new();
    }

    void Update() {
        Move();
        RotateTowardsMouse();
        FireBullet();
        LaunchMissile();
    }

    void Move() {
        Vector2 inputDir = _input.Move;
        if(inputDir.sqrMagnitude > 0.01f) {
            _velocity += inputDir.normalized * _acceleration * Time.deltaTime;
        }

        _velocity = Vector2.ClampMagnitude(_velocity, _maxSpeed);

        _velocity = Vector2.Lerp(_velocity, Vector2.zero, _drag * Time.deltaTime);


        transform.position += Time.deltaTime * (Vector3)_velocity;

        ClampPosition();

    }

    void ClampPosition() {
        Vector3 screenToWorld = Camera.main.ScreenToWorldPoint(new(Screen.width, Screen.height));

        Vector3 clampPos = transform.position;
        clampPos.x = Mathf.Clamp (
            clampPos.x,
            -screenToWorld.x + _clampPosOffset,
            screenToWorld.x - _clampPosOffset
        );
        
        clampPos.y = Mathf.Clamp (
            clampPos.y,
            -screenToWorld.y + _clampPosOffset, 
            screenToWorld.y - _clampPosOffset
        );
        
        transform.position = clampPos;
    }

    void RotateTowardsMouse() {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(_input.Look);
        Vector2 dir = (mousePos - (Vector2)transform.position).normalized;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        float smoothSpeed = 10f;

        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, smoothSpeed * Time.deltaTime);
    }

    void FireBullet() {
        if(_input.Fire && (Time.time > _lastFire + _fireCooldown) && _bulletAmount > 0) {
            Bullet bullet = Instantiate(_bullet, _bulletSpawnPos.position, transform.rotation);

            float spreadAngle = Random.Range(-_bulletSpread, _bulletSpread);
            Vector3 direction = Quaternion.Euler(0, 0, spreadAngle) * transform.up;

            bullet.SetSender(this);
            bullet.SetBulletSpeed(_bulletSpeed);
            bullet.SetBulletDirection(direction);

            if(!_unlimitedBullet) _bulletAmount--;
            
            _lastFire = Time.time;
        }
    }

    void LaunchMissile() {
        if(_input.LaunchMissile && CanLaunchMissile) {
            Debug.Log("Missile Launched!!");
        }
    }

    public void ChargeMissile(int chargeAmount) {
        if(CanLaunchMissile) return;
        _missile += chargeAmount;
    }

    bool CanLaunchMissile => _missile >= 30;

    public void DestroyPlayer() {
        Destroy(gameObject);
    }

    void OnDisable() {
        _input.DisableInputActions();
    }
}

public class PlayerInput {
    InputActions _inputActions;

    public PlayerInput() {
        _inputActions = new();
        _inputActions.Player.Enable();
    }

    public Vector2 Move => _inputActions.Player.Move.ReadValue<Vector2>().normalized;
    public Vector2 Look => _inputActions.Player.Look.ReadValue<Vector2>();
    public bool Fire => _inputActions.Player.Fire.IsPressed();
    public bool LaunchMissile => _inputActions.Player.LaunchMissile.WasPerformedThisFrame();
    
    public void DisableInputActions() {
        _inputActions.Player.Disable();   
    }
}
