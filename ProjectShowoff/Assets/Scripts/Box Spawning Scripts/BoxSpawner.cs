using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//spawner for boxes (still needs visualization)
public class BoxSpawner : MonoBehaviour
{
    private BoxFactory boxFactory;
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
            Box newBox = boxFactory.CreateRandomBox();
            Debug.Log(newBox.Type);
        }
    }
}
