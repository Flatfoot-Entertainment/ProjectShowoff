using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SimpleConveyor : MonoBehaviour
{
    public float Speed
    {
        get => speed;
        set => speed = value;
    }

    public float InitialSpeed
    {
        get => initialSpeed;
        set => initialSpeed = value;
    }

    [SerializeField] private float speed;
    [SerializeField] private float initialSpeed = 2.0f;
    private Rigidbody rb;


    // Start is called before the first frame update
    private void Awake()
    {
        speed = initialSpeed;
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 oldPos = rb.position;
        rb.position += (-transform.forward) * speed * Time.fixedDeltaTime;
        rb.MovePosition(oldPos);
    }

    //just no, change the parameters required (or the code in general)
    public IEnumerator StopConveyor(ItemSpawner spawner, float delay)
    {
        spawner.CanSpawn = false;
        speed = 0f;
        yield return new WaitForSeconds(delay);
        speed = initialSpeed;
        spawner.CanSpawn = true;
        
    }
}
