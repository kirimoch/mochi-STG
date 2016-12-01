using UnityEngine;
using System.Collections;

public class Sensor : MonoBehaviour {

    [SerializeField] private SensorType sensorType;

    enum SensorType
    {
        EnemyDestroy,
        BulletDestroy,
        ItemDestroy,
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
            case SensorType.ItemDestroy:
                if(col.gameObject.tag == "item")
                {
                    Destroy(col.gameObject);
                }
                break;
            case SensorType.FrontSensor:
                StageObjBase stageObjBase = col.gameObject.GetComponent<StageObjBase>();
                if (stageObjBase != null)
                {
                    col.gameObject.transform.parent = transform.root;
                    stageObjBase.isActive = true;
                    
                }
                break;
        }
    }
}
