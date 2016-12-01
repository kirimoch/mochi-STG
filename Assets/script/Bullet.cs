using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    private float speed = 2500.00f;

    // Use this for initialization
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(
            (transform.right) * speed);
    }


    // Update is called once per frame
    void Update()
    {

    }
    
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "enemy")
        { 
             Destroy(gameObject);
        }
    }
    
}
