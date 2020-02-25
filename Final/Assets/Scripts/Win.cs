using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(WinGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void WinGame()
    {
        SceneManager.LoadScene("Start");
    }
}
