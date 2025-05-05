using UnityEngine;

public class GameManager : MonoBehaviour {
    
    const int WIDTH = 800, HEIGHT = 600;

    void Awake() {
        Screen.SetResolution(WIDTH, HEIGHT, false);
    }
    void Update() {

        



        if(Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
}
