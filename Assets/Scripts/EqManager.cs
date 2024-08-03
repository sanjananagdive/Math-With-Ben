using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EqManager : MonoBehaviour
{
    public GameObject eq1;
    public GameObject eq2;

    public bool eq1Destroyed= false;

    public static EqManager instance;


    void Awake()
    {
        if(instance ==null)
        {
            instance = this;
        }
    }

    public void DestroyEq1()
    {
        Destroy(eq1, 1.8f);
        eq1Destroyed = true;
    }

    public void DestroyEq2()
    {
        Destroy(eq2, 1.8f);
    }

}
