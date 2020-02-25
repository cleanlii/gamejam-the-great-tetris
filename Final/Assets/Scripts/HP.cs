using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UI;

using System.Runtime.InteropServices;
using System;

public class HP : MonoBehaviour
{
    public int Health = 10;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<HP>().Health <= 0)
        {
            if (gameObject.GetComponent<CellObject>().moveAble == true)
            {
                Messagebox.MessageBox(IntPtr.Zero, "你被干掉了！", "嘻嘻", 0);
                Destroy(gameObject);
                SceneManager.LoadScene("Over");
            }
            else 
            {
                Messagebox.MessageBox(IntPtr.Zero, "恭喜胜利！", "嘻嘻", 0);
                Destroy(gameObject);
            }
        }

    }

    void OverGame()
    {
        SceneManager.LoadScene("Over");
    }
}
