using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatePlatform : MonoBehaviour
{
    private bool myBool;
    public GameObject prefabToSpawn; 
    public GameObject eq2;

    
    
    void Update()
    {
        myBool = EquationScript.instance.setPlatformActive;
        // Instantiate the prefab at the specified position and rotation when the space key is pressed
        if (myBool)
        {
            if(eq2==null)
            {
                Instantiate(prefabToSpawn, transform.position, transform.rotation);
    

                myBool=false;
            }
            
        }
    }
}
