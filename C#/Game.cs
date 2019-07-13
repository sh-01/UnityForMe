using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    [SerializeField] public KeyCode quitKey;

    void Start ()
    {
        Cursor.visible = false;
        //Screen.fullScreen = true;
    }

    void Quit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;

        #elif UNITY_STANDALONE
            UnityEngine.Application.Quit();
        #endif
    }

    void Update()
    {
        if (Input.GetKey(quitKey)) Quit();
    }
}
