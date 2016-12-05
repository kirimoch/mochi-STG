using UnityEngine;
using System.Collections;

public class Sensor : MonoBehaviour {

    [SerializeField] private SensorType sensorType;

    enum SensorType
    {
        EnemyDestroy,
        BulletDestroy,
        FrontSensor
    }

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnTriggerEnter(Collider col)
    {
        switch(sensorType)
        {
            case SensorType.BulletDestroy:
                if (col.gameObject.tag == "bullet" || col.gameObject.tag == "enemybullet")
                {
                    Destroy(col.gameObject);
                }
                break;
            case SensorType.EnemyDestroy:
                if (col.gameObject.tag == "enemy")
                {
                    Destroy(col.gameObject);
                }
                break;
            case SensorType.FrontSensor:
                if (col.gameObject.tag == "enemy")
                {
                    Enemy em = col.GetComponent<Enemy>();
                    col.gameObject.transform.parent = transform.root;
                    if (em != null)
                    {
                        em.isActive = true;
                    }
                }
                break;
        }
    }
}
