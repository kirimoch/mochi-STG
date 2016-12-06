using UnityEngine;
using System.Collections;

public class Sensor : MonoBehaviour {

    [SerializeField] private SensorType sensorType;

    enum SensorType
    {
        BulletDestroy,
        StageObjDestroy,
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
        string tag = col.gameObject.tag;
        switch(sensorType)
        {
            case SensorType.BulletDestroy:
                if (tag == "bullet" || tag == "enemybullet")
                {
                    Destroy(col.gameObject);
                }
                break;
            case SensorType.StageObjDestroy:
                if (tag == "enemy" || tag == "item")
                {
                    Destroy(col.gameObject);
                }
                break;
            case SensorType.FrontSensor:
                if (tag == "enemy" || tag == "item")
                {
                    StageObject em = col.GetComponent<StageObject>();
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
