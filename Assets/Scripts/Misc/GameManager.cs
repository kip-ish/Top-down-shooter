using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    
    const int WIDTH = 800, HEIGHT = 600;


    public static GameManager Instance {get; private set;}
    void Awake() {
        if(Instance == null) Instance = this;

        Screen.SetResolution(WIDTH, HEIGHT, false);
    }
    void Update() {
        if(Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        if(PlayerIsDead && Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(0);
    }

    public bool PlayerIsDead => !Player.Instance;
        
    
}
