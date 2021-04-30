using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    private SampleBoxFactory boxFactory;
    // Start is called before the first frame update
    void Start()
    {
        boxFactory = new SampleBoxFactory();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            boxFactory.CreateRandomBox();
        }
    }
}
