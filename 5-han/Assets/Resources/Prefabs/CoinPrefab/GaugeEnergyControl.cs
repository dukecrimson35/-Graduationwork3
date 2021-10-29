using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaugeEnergyControl : MonoBehaviour
{
    private float easingCount = 0;
    //private bool easingFlag = false;
    public int num = 80;
    private int num2 = 1200;
    private int num3 = 400;
    public float maxSpeed = 40;
    private float maxSpeed2 = 15;
    private float speed = 0.0f;
    private float speed2 = 0.0f;
    private Vector3 playerPos;
    private Vector3 velocity;

    private GameObject player;
    //private GameObject gauge;

    //private GameObject target;
    private float speedRotate = 0.01f;
    float step = 0;

    private bool hit = false;
    float angleans;
    float angleans2;
    float radian = 0;

    public GameObject aaa;

   

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //gauge = GameObject.FindGameObjectWithTag("GaugeTarget");
        //target = GameObject.FindGameObjectWithTag("GaugeTarget2");
        //playerPos = target.transform.position;
        
        Vector3 diff = (playerPos - this.transform.position);
        var screenPos = Camera.main.WorldToScreenPoint(transform.position);
        var targetscreenPos = Camera.main.WorldToScreenPoint(playerPos);
        var direction = targetscreenPos - screenPos;
        var angle = GetAim(Vector3.zero, direction);
        transform.localEulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, angle - 180);
    }

    public float GetAim(Vector2 from, Vector2 to)
    {
        float dx = to.x - from.x;
        float dy = to.y - from.y;
        float rad = Mathf.Atan2(dy, dx);
        return rad * Mathf.Rad2Deg;
    }


    // Update is called once per frame
    void Update()
    {
        //ポーズの時に止める
        if (Time.timeScale <= 0) return;

        playerPos = player.transform.position;
        //playerPos = gauge.transform.position;

        var screenPos = Camera.main.WorldToScreenPoint(transform.position);
        var targetscreenPos = Camera.main.WorldToScreenPoint(playerPos);
        var direction = targetscreenPos - screenPos;



        Vector3 diff = (playerPos - this.transform.position);

        this.transform.rotation = Quaternion.FromToRotation(Vector3.up, diff);

        if (!hit)
        {
            speed = Easing.SineInOut(easingCount, num, speed, maxSpeed);
            float speed2 = Easing.QuartIn(easingCount, num2, speed, maxSpeed);

            speed = speed - speed2/50;

            if (Vector3.Distance(targetscreenPos, screenPos) < 200)
            {

                if (angleans2 != transform.rotation.z)
                {

                    var screenPos2 = Camera.main.WorldToScreenPoint(transform.position);
                    var targetscreenPos2 = Camera.main.WorldToScreenPoint(player.transform.position);
                    var direction2 = targetscreenPos2 - screenPos2;
                    angleans2 = GetAim(Vector3.zero, direction2);
                    transform.localEulerAngles += new Vector3(transform.rotation.x, transform.rotation.y, 180) * Time.deltaTime;
                }
            }


        }
        else
        {
            speed = Easing.SineInOut(easingCount, num3, speed, maxSpeed2);
            
            playerPos = player.transform.position;

            if (angleans < transform.rotation.z)
            {
                transform.localEulerAngles += new Vector3(transform.rotation.x, transform.rotation.y, 90) * Time.deltaTime;
            }

            if (aaa.transform.localPosition.x>0)
            {
                aaa.transform.localPosition -= new Vector3(1, 0, 0)*Time.deltaTime;
            }
            //

        }

        velocity = Move();
        transform.position += velocity * speed * Time.deltaTime * 2;


        easingCount = easingCount + 90 *Time.deltaTime;
    }


    public Vector3 Move()
    {
        float x = this.transform.position.x - playerPos.x;
        float y = this.transform.position.y - playerPos.y;

        Vector3 v = new Vector3(-x, -y, 0);
        v.Normalize();

        return v;
    }
    public void SetPosition(Vector3 pos)
    {
        this.transform.position = pos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }

        if (other.gameObject.tag == "GaugeTarget")
        {
            //player.GetComponent<PlayerControl>().GaugeUp();
            Destroy(this.gameObject);
        }

        if (other.gameObject.tag == "GaugeTarget2" && hit == false)
        {
            //playerPos = gauge.transform.position;
            //easingCount = 0;
            //speed = 0;
            //hit = true;

            //var screenPos = Camera.main.WorldToScreenPoint(transform.position);
            //var targetscreenPos = Camera.main.WorldToScreenPoint(playerPos);
            //var direction = targetscreenPos - screenPos;
            //angleans = GetAim(Vector3.zero, direction);

            /*transform.localEulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, angle - 180)*/;

        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "GaugeTarget")
        {
            //player.GetComponent<PlayerControl>().GaugeUp();
            Destroy(this.gameObject);
        }

        if (col.gameObject.tag == "GaugeTarget2" && hit == false)
        {
            //playerPos = gauge.transform.position;
            //easingCount = 0;
            //speed = 0;
            //hit = true;

            //player.GetComponent<PlayerControl>().GaugeUp();
            Destroy(this.gameObject);
        }
    }


}
