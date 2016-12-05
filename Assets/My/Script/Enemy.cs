using UnityEngine;
using System.Collections;

public class Enemy : StageObjBase {

    public GameObject bullet;
    public float hitPoint;
    public bool isHoming;
    public int enemyBulletNum;
    public float BulletSpeed = 500f;
    public float fireInterval = 0.5f;
    public float score = 100f;

    ScoreText scoreText;
    GameManager gm;
    GameObject objPlayer;
    GameObject cannon;

    bool isCooling;

    // Use this for initialization
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        cannon = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (objPlayer == null)
        {
            objPlayer = gm.objPlayer;
        }

        if (isActive)
        {
            Move();
            CreatEnemyBullet();
        }
    }

    void CreatEnemyBullet()
    {
        //発射処理
        if (!isCooling)
        {
            if (objPlayer == null) return;

            for (int i = -enemyBulletNum / 2; i <= enemyBulletNum / 2; i++)
            {
                if (isHoming == true)
                {
                    cannon.transform.LookAt(objPlayer.transform);//transformをプレイヤーの方向に初期化
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
                objBullet.GetComponent<Rigidbody>().AddForce(
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
            hitPoint = hitPoint - Player.power;
        }
        if (hitPoint <= 0)
        {
            ScoreText.totalScore = score + ScoreText.totalScore;
            Destroy(gameObject);

        }
    }
}

