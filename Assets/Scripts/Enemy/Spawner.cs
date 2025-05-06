using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {

    [SerializeField] List<Enemy> _enemies = new();


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


  


    public void SpawnRandomEnemy(float speedMultiplier = 1f) {
        int randomIndex = Random.Range(0, _enemies.Count);
        SpawnSide side = (SpawnSide)Random.Range(0, 4);

        Vector2 offset = new Vector2(
            side == SpawnSide.Left ? -_xPosOffset : side == SpawnSide.Right ? _xPosOffset : 0f,
            side == SpawnSide.Top ? _yPosOffset : side == SpawnSide.Bottom ? -_yPosOffset : 0f
        );

        Vector2 spawnPos = SpawnerPosition(offset, side);
        Enemy enemy = Instantiate(_enemies[randomIndex], spawnPos, Quaternion.identity);
        enemy.SetMoveDirection(GetDirectionFromSide(side), speedMultiplier); // Set direction + scale

        enemy.gameObject.AddComponent<DestroyOffscreen>();
    }

    Vector2 GetDirectionFromSide(SpawnSide side) {
        switch (side) {
            case SpawnSide.Top: return Vector2.down;
            case SpawnSide.Bottom: return Vector2.up;
            case SpawnSide.Left: return Vector2.right;
            case SpawnSide.Right: return Vector2.left;
            default: return Vector2.zero;
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
