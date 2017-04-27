using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.UI.Button))]
public class ReloadButton : MonoBehaviour {
    private void Start()
    {
        GetComponent<UnityEngine.UI.Button>().onClick.AddListener(UserOnClick);
    }

    void UserOnClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }
}
