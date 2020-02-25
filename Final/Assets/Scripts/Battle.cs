using UnityEngine;
using System.Collections;
using UnityEngine.UI; 

public class Health : MonoBehaviour
{
	public static Text txt;	
	public static int x = 0;
	void Start()
	{
		txt = GameObject.Find("Text").GetComponent<Text>();
	}
}