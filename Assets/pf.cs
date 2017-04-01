using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pf : MonoBehaviour {


    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update() { }

    public float dist(Vector2 v1,Vector2 v2)
    {
        return Mathf.Sqrt(Mathf.Pow((v1.x-v2.x),2)+ Mathf.Pow((v1.y - v2.y), 2));
    }

    public float dist(indexv2 v1, indexv2 v2)
    {
        return Mathf.Sqrt(Mathf.Pow((float)(v1.x - v2.x), 2) + Mathf.Pow((float)(v1.y - v2.y), 2));
    }

    public float dist2(indexv2 v1, indexv2 v2)
    {
        const float yo = 14.14f;
        const float xo = 10f;
        int dx = v1.x - v2.x;
        if (dx < 0)
            dx = -dx;
        int dy = v1.y - v2.y;
        if (dy < 0)
            dy = -dy;
        int min = Mathf.Min(dx, dy);
        int max = Mathf.Max(dx, dy);
        int j = max - min;
        return (float)j * xo + (float)(max - j) * yo;
    }

    public bool isoutfromborder(indexv2 point,indexv2 borders)
    {
        if (point.x < 0 || point.y < 0||(borders.x-1)<point.x||(borders.y-1)<point.y)
            return true;
        return false;
    }

    public indexv2 mass_to_index(pf_container[,] scont)
    {
        return new indexv2(scont.GetLength(0),scont.GetLength(1));
    }

    public ArrayList allnear(pf_container[,] scont, indexv2 point)
    {
        ArrayList near = new ArrayList();
        indexv2 t = new indexv2(0);

        t = new indexv2(1, 0);
        if (!isoutfromborder(point.add(t), mass_to_index(scont)))
            near.Add(scont[point.x + t.x, point.y + t.y]);

        t = new indexv2(-1, 0);
        if (!isoutfromborder(point.add(t), mass_to_index(scont)))
            near.Add(scont[point.x + t.x, point.y + t.y]);

        t = new indexv2(0, 1);
        if (!isoutfromborder(point.add(t), mass_to_index(scont)))
            near.Add(scont[point.x + t.x, point.y + t.y]);

        t = new indexv2(0, -1);
        if (!isoutfromborder(point.add(t), mass_to_index(scont)))
            near.Add(scont[point.x + t.x, point.y + t.y]);

        t = new indexv2(1, 1);
        if (!isoutfromborder(point.add(t), mass_to_index(scont)))
            near.Add(scont[point.x + t.x, point.y + t.y]);

        t = new indexv2(1, -1);
        if (!isoutfromborder(point.add(t), mass_to_index(scont)))
            near.Add(scont[point.x + t.x, point.y + t.y]);

        t = new indexv2(-1, 1);
        if (!isoutfromborder(point.add(t), mass_to_index(scont)))
            near.Add(scont[point.x + t.x, point.y + t.y]);

        t = new indexv2(-1, -1);
        if (!isoutfromborder(point.add(t), mass_to_index(scont)))
            near.Add(scont[point.x + t.x, point.y + t.y]);

        return near;
    }

    public ArrayList calc(pf_container[,] scont,indexv2 sstartpoint,indexv2 sendpoint )
    {
        ArrayList open = new ArrayList();
        ArrayList closed =new ArrayList();
        ArrayList path =new ArrayList();
        bool win = false;
        //задаем начальную точку
        open.Add(scont[sstartpoint.x,sstartpoint.y]);
        (open[0] as pf_container).mincost = 0f;
        (open[0] as pf_container).hcost = dist2(sstartpoint,sendpoint);
        (open[0] as pf_container).fincost = (open[0] as pf_container).mincost +
                                            (open[0] as pf_container).hcost;

        while (open.Count != 0)
        {
            pf_container min_item = open[0] as pf_container;
            #region ищем минимальное значение
            float min = float.MaxValue;


            for (int i = 0; i < open.Count; i++)
                if ((open[i] as pf_container).fincost < min)
                {
                    min_item = open[i] as pf_container;
                    min = min_item.fincost;
                }
            #endregion

            open.Remove(min_item);
            closed.Add(min_item);


            foreach (pf_container sosed in allnear(scont, min_item.ind))
            {
                if (!sosed.ini) //если не инициирован, то инициируем
                {
                    if (sosed.ind == sendpoint)//если таргет
                    {
                        win = true;
                        sosed.parent = min_item;
                        break;

                    }
                    sosed.ini = true;
                    if (sosed.go.GetComponent<Square>().color == 1)
                    {
                        sosed.isbrick = true;
                    }
                    else
                    {
                        sosed.mincost = min_item.mincost + dist2(min_item.ind, sosed.ind);
                        sosed.hcost = dist2(sosed.ind, sendpoint);
                        sosed.fincost = sosed.hcost + sosed.mincost;
                        sosed.parent = min_item;
                    }
                }
                if (!sosed.isbrick) //если стенка, то пропускаем
                {
                    float d = min_item.mincost + dist2(min_item.ind, sosed.ind);
                    if (d < sosed.mincost)
                    {
                        sosed.parent = min_item;
                        sosed.mincost = d;
                        sosed.fincost = sosed.hcost + sosed.mincost;
                    }
                    if (!closed.Contains(sosed))//если уже прошлись, то пропускаем
                    {
                        if (!open.Contains(sosed))
                        {
                            open.Add(sosed);
                        }
                    }
                }
            }
            if (win)
            {
                break;
            }
        }

        if (!win)
            return path;

        pf_container cur = scont[sendpoint.x, sendpoint.y];
        while(cur.parent!=scont[sstartpoint.x,sstartpoint.y])
        {
            path.Add(cur);
            cur = cur.parent;
        }

        path.Add(cur);
        cur = cur.parent;
        path.Add(cur);

        foreach (pf_container node in open)
        {
            node.go.GetComponent<Square>().color = 2;
        }

        foreach (pf_container node in closed)
        {
            node.go.GetComponent<Square>().color = 3;
        }
        
        return path;
    }
}

public struct indexv2
{
    public int x ;
    public int y ;
    public indexv2(int i)
    { x = 0;y = 0; }
    public indexv2(int sx,int sy)
    {
        x = sx;
        y = sy;
    }
    public indexv2 add(int i,char c)
    {
        // КОСТЫЛЬ БЛЯТЬ КАКОЙ ТО
        if (c == 'x' || c == 'X')
            return new indexv2(this.x+i,this.y);
        if (c == 'y' || c == 'Y')
            return new indexv2(this.x, this.y+i);
        return this;
    }
    public indexv2 add(int i,int j)
    {
        return new indexv2(this.x + i, this.y+j);
    }
    public indexv2 add(indexv2 v2)
    {
        return new indexv2(this.x+v2.x, this.y+v2.y);
    }

    public static bool operator ==(indexv2 m1, indexv2 m2)
    {
        return (m1.x == m2.x) && (m1.y == m2.y);
    }
    public static bool operator !=(indexv2 m1, indexv2 m2)
    {
        return (m1.x != m2.x) || (m1.y != m2.y);
    }
}

public class pf_container
{
    public indexv2 ind;
    public GameObject go;
    public bool ini = false;
    public float hcost;
    public float fincost;
    public float mod = 1f;
    public float mincost= float.MaxValue; 
    public bool isbrick = false;
    public pf_container parent;

    public pf_container(GameObject sgo,indexv2 si)
    {
        go = sgo;
        ind = si;
    }

    public pf_container(GameObject sgo,indexv2 si,float smod)
    {
        go = sgo;
        mod = smod;
        ind = si;
    }
    public override bool Equals(object o)
    {
        if (o is pf_container)
            return (o as pf_container).ind == this.ind;
        return false;
    }
    public static bool operator ==(pf_container m1, pf_container m2)
    {
        return m1.ind==m2.ind;
    }
    public static bool operator !=(pf_container m1, pf_container m2)
    {
        return m1.ind != m2.ind;
    }
}

