using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenuControl : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            SceneManager.LoadScene(0);
        }

        if (Input.GetKey(KeyCode.W))
        {
            SceneManager.LoadScene(0);
        }
    }
}
