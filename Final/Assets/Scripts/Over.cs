using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Over : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(OverGame);

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OverGame()
    {
        SceneManager.LoadScene("Play");
    }
}
