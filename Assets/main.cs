using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class main : MonoBehaviour {

    public static main main01;
    public GameObject sp1;
    public GameObject[,] mass;
    public int razm = 6;

    // Use this for initialization
    void Start()
    {

        float o1 = 3f;
        float o2 = -3f;
        float items = (float)(razm-1);
        float step = (o2*2)/items;

        main01 = this;

        mass = new GameObject[razm, razm];
        for(int i=0;i<mass.GetLength(0);i++)
        {
            for (int j = 0; j < mass.GetLength(1); j++)
            {
                GameObject sp2 = Instantiate(sp1, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
                mass[i, j] = sp2;
                sp2.GetComponent<SpriteRenderer>().color = Color.red;
                sp2.GetComponent<Transform>().localScale = new Vector3(0.14f, 0.14f, 0.14f);
                sp2.transform.position = new Vector3(o1+((float)i*step), o1 + ((float)j * step), -0.1f);
                sp2.GetComponent<Square>().off = 0;
                sp2.GetComponent<Square>().lastcolor = 99;
            }
        }        
    }

    
    public void click()
    {
       // mass[0,0].GetComponent<Transform>().localScale = new Vector3(2.1f, 2.1f, 0.1f);
        Debug.Log("1");
        //t1.GetComponent<SpriteRenderer>().color = Color.blue;
    }

    public void start_calc()
    {
        Debug.Log("start2");
        pf_container[,] cont = new pf_container[razm, razm];
        for (int i = 0; i < razm; i++)
            for (int j = 0; j < razm; j++)
            {
                cont[i, j] = new pf_container(mass[i,j],new indexv2(i,j));
                
            }
        pf p = new pf();
        ArrayList path= p.calc(cont,new indexv2(6,6),new indexv2(34,34));

        foreach(pf_container node in path)
        {
            node.go.GetComponent<Square>().color = 4;
        }
    }
    public void clear()
    {
        pf_container[,] cont = new pf_container[razm, razm];
        for (int i = 0; i < razm; i++)
            for (int j = 0; j < razm; j++)
            {
                cont[i, j] = new pf_container(mass[i, j], new indexv2(i, j));
            }
        foreach (pf_container node in cont)
        {
            if(node.go.GetComponent<Square>().color!=1)
                node.go.GetComponent<Square>().color = 0;
        }
    }
    private void OnMouseUpAsButton()
    {
        Debug.Log("qwe");
    }
    // Update is called once per frame
    void Update () {
		
	}
}
