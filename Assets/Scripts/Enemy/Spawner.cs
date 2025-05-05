using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {

    [SerializeField] List<Enemy> _enemies = new();

    [SerializeField] float _spawnCooldown;
    [SerializeField] bool _canSpawn = true;

    [SerializeField] float _xPosOffset;
    [SerializeField] float _yPosOffset;


    public static Spawner Instance {get; private set;}

    public enum SpawnSide {
        Top,
        Bottom,
        Left,
        Right
    }

    void Awake() {
        if(Instance == null) Instance = this;
    }

    void Start() {
        StartCoroutine(SpawnEnemyCoroutine());
    }


    IEnumerator SpawnEnemyCoroutine() {
        while (_canSpawn) {
            yield return new WaitForSeconds(_spawnCooldown);

            if (_enemies == null || _enemies.Count == 0) {
                Debug.LogWarning("No enemies to spawn!");
                continue;
            }


            int randomSpawnDirection = Random.Range(0, 4);
            SpawnSide side = (SpawnSide)randomSpawnDirection;

            RandomSpawnDirection(side, 
                side == SpawnSide.Left ? -_xPosOffset :
                side == SpawnSide.Right ? _xPosOffset : 0f,
                
                side == SpawnSide.Top ? _yPosOffset :
                side == SpawnSide.Bottom ? -_yPosOffset : 0f
            );

        }
    }


    void RandomSpawnDirection(SpawnSide side, float xOffset, float yOffset) {

        Debug.Log($"Spawning from {side}");

        int randomEnemySpawn = Random.Range(0, _enemies.Count);
        Vector2 spawnPos = SpawnerPosition(new Vector2(xOffset, yOffset), side);

        Enemy enemy = Instantiate(_enemies[randomEnemySpawn], spawnPos, Quaternion.identity);
        enemy.gameObject.AddComponent<DestroyOffscreen>();

        if(enemy is Wanderer || enemy is ShapeShifter) {
            switch(side) {
                case SpawnSide.Top:
                    enemy.SetMoveDirection(Vector3.down);
                    break;
                case SpawnSide.Bottom:
                    enemy.SetMoveDirection(Vector3.up);
                    break;
                case SpawnSide.Left:
                    enemy.SetMoveDirection(Vector3.right);
                    break;
                case SpawnSide.Right:
                    enemy.SetMoveDirection(Vector3.left);
                    break;
            }
        }
    }
    

    Vector2 SpawnerPosition(Vector2 offset, SpawnSide spawnSide) {
        Vector2 screenBounds =
            Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        float xPos = 0f;
        float yPos = 0f;

        switch (spawnSide) {
            case SpawnSide.Top:
                xPos = Random.Range(-screenBounds.x, screenBounds.x);
                yPos = screenBounds.y + offset.y;
                break;

            case SpawnSide.Bottom:
                xPos = Random.Range(-screenBounds.x, screenBounds.x);
                yPos = -screenBounds.y + offset.y;
                break;

            case SpawnSide.Right:
                xPos = screenBounds.x + offset.x;
                yPos = Random.Range(-screenBounds.y, screenBounds.y);
                break;

            case SpawnSide.Left:
                xPos = -screenBounds.x + offset.x;
                yPos = Random.Range(-screenBounds.y, screenBounds.y);
                break;
        }

        return new Vector2(xPos, yPos);
    }

}
