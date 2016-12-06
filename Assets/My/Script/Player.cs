using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

    public static float power = 1.0f;
    public GameObject bullet;
    [SerializeField] private float accel = 3.0f;
    GameManager gm;
    bool isCooling;

    // Use this for initialization
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().AddForce(
        transform.right * Input.GetAxisRaw("Horizontal") * accel + transform.up * Input.GetAxisRaw("Vertical") * accel,
        ForceMode.Impulse);

        if  (Input.GetKey(KeyCode.JoystickButton0) || Input.GetKey(KeyCode.Space))
        {
            if (!isCooling)
            {
                CreateFire();
            }
        }
    }
    void CreateFire()
    {
        Instantiate(bullet, transform.position, Quaternion.identity);
        isCooling = true;
        Invoke("Cooling", .25f /2);//弾間の時間
    }
        void Cooling()
    {
        isCooling = false;
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "enemybullet")
        {
            var lastPos = col.gameObject.transform.localPosition;
            Destroy(col.gameObject);
            gm.CreatePlayer(lastPos, 1f);
        }
    }
}
