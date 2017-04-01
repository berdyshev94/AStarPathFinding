using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour {


    [SerializeField]
    public int off = 0;

    [SerializeField]
    public int lastcolor = 99;
    [SerializeField]
    public int color = 0;//0-пусто(белый) 1-стенка(черный) 2-рассчитывается(зеленый) 3-проходит(красный)
    [SerializeField]
    public main m=main.main01;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (off == 0)
        {
            if (lastcolor != color)
            {
                if (color == 0)
                {
                    lastcolor = 0;
                    gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                }
                if (color == 1)
                {
                    lastcolor = 1;
                    gameObject.GetComponent<SpriteRenderer>().color = Color.black;
                }
                if (color == 2)
                {
                    lastcolor = 2;
                    gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                }
                if (color == 3)
                {
                    lastcolor = 3;
                    gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                }
                if (color == 4)
                {
                    lastcolor = 4;
                    gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                }
            }
        }
	}

    private void OnMouseUpAsButton()
    {
        Debug.Log("ch col2");
        if (off == 0)
        {
            if (color != 0)
                color = 0;
            else
                color = 1;
        }
    }
}
