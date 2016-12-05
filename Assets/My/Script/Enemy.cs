using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public float speed;
    public float HitPoint;
    public float BulletSpeed = 500f;
    public EnemyMoveType enemyType;
    public bool isHoming;
    public int EnemyBulletNum;
    public float fireInterval = 0.5f;
    public float searching_enemy;
    public GameObject bullet;

    GameManager GM;

    GameObject player;
    GameObject cannon;

    bool isCooling;
    bool hasFound;
    bool hasItweenMoving;
    [HideInInspector]public bool isActive;
    
    public enum EnemyMoveType
    {
        chase,
        straight,
        itween
    }
    // Use this for initialization
    void Start() {
        GM = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        player = GameObject.Find("player");
        cannon = transform.GetChild(0).gameObject;
        Debug.Log(EnemyBulletNum);
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null)
        {
            player = GM.playerPre;
        }
        if (isActive)
        {
            Move();
            CreatEnemyBullet();
        }
    }

    void Move()
    {
        switch (enemyType)
        {
            case EnemyMoveType.chase:
                //playerの認識(一定距離まで近づいたらif文内を実行)
                if (player != null && Vector2.Distance(player.transform.position, transform.position) < searching_enemy)
                {
                    Vector2 distance = player.transform.position - transform.position;
                    Vector2 direction = distance / distance.magnitude;
                    transform.Translate(direction * speed * Time.deltaTime, Space.World);
                }
                else
                {
                    transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
                }
                break;
            case EnemyMoveType.straight:
                transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
                break;
            case EnemyMoveType.itween:
                if (hasItweenMoving) return;

                MyiTweenPath iPath = GetComponent<MyiTweenPath>();
                if (iPath == null) break;

                //pathを現在位置から相対座標に加工
                for (int i = 0; i < iPath.nodeCount; i++)
                {
                    iPath.nodes[i] = (iPath.nodes[i] - transform.position) + transform.localPosition;
                }
                string pathName = iPath.pathName;

                hasItweenMoving = true;
                iTween.MoveTo(gameObject, iTween.Hash("movetopath", false, 
                                                      "path", iTweenPath.GetPath(pathName),
                                                      "time", 3,
                                                      "easetype", iTween.EaseType.easeOutSine,
                                                      "islocal", true));
                break;
        }

    }


    void CreatEnemyBullet()
    {  
        //発射処理
        if (!isCooling)
        {
            if (player == null) return;

            for (int i = -EnemyBulletNum / 2; i <= EnemyBulletNum / 2; i++)
            {
                if (isHoming == true )
                {
                    cannon.transform.LookAt(player.transform);//transformをプレイヤーの方向に初期化
                }
                else
                {
                    //まっすぐの処理
                    //左向きに初期化
                    cannon.transform.rotation = transform.rotation;
                }
                cannon.transform.Rotate(30 * i, 0, 0); //発射角度　transformを上書き
                GameObject objBullet = Instantiate(bullet, cannon.transform.position, cannon.transform.rotation) as GameObject; //bulletの生成、enemyのz軸方向に進む
                objBullet.transform.parent = transform.root;
                objBullet. GetComponent<Rigidbody>().AddForce(
                    (objBullet.transform.forward) * BulletSpeed); //forwardでz軸に発射
            }
            isCooling = true;
            Invoke("Cooling", fireInterval);//弾間の時間
        }
   }

    void Cooling()
    {
        isCooling = false;
    }


    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "bullet")
        {
            HitPoint--;
        }
        if (HitPoint == 0)
        {
            Destroy(gameObject);
        }
    }
}
