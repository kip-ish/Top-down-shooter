using System;
using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour {
    [SerializeField] private Spawner _spawner;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private int enemiesStartCount = 5;
    [SerializeField] private int enemiesIncrement = 2;
    [SerializeField] private float spawnInterval = 0.5f;

    public static WaveManager Instance{get; private set;}

    public Action<int> OnWaveUIChanged;
    private int _currentWave = 0;

    void Awake() {
        if(Instance == null) Instance = this;
        StartCoroutine(WaveLoop());
    }

    IEnumerator WaveLoop()
    {
        while (true)
        {
            _currentWave++;
            OnWaveUIChanged?.Invoke(_currentWave);
            int enemiesToSpawn = enemiesStartCount + (_currentWave - 1) * enemiesIncrement;

            Debug.Log($"Wave {_currentWave} — Spawning {enemiesToSpawn} enemies");

            float speedMultiplier = 1f + (_currentWave - 1) * 0.1f; // e.g. +10% per wave 

            for (int i = 0; i < enemiesToSpawn; i++) {
                _spawner.SpawnRandomEnemy(speedMultiplier);
                yield return new WaitForSeconds(spawnInterval);
            }

            // Wait before next wave
            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }
}
