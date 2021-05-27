using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetToolScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<BoxCollider>().enabled = false;
    }
}
