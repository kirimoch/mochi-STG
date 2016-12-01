using UnityEngine;
using System.Collections;

public class Item : StageObjBase {

    public ItemType itemType;

    public enum ItemType
    {
        PowerUp,
        LifeUp
    } 

    void Start()
    {

    }

    void Update()
    {
        if (isActive)
        {
            Move();
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            switch (itemType)
            {
                case ItemType.PowerUp:
                    Player.power = Player.power * 2;
                    Destroy(gameObject);
                    break;
                case ItemType.LifeUp:
                    GameMaster.life = GameMaster.life + 1;
                    Destroy(gameObject);
                    break;
            }
        }
    }
}
