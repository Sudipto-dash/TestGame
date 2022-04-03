using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockControll : MonoBehaviour
{
    [SerializeField]
    public float speed = 1f;
    public float amount = 1f;
    public int countHit = 0;
    public int destroyHit = 3;
    [SerializeField]
    public OpenWorldInventory openWorldInventory;

    public bool isCollectionNeeded = false;
    Vector3 currentPos;
    Vector3 positionRock;
    
    [SerializeField]
    Vector3 floatingPos;
    private float freezTime = 50f;

    bool DoneTheJob = false;

    bool startTime = false;

    bool canCollect;

    float _jumpTimeoutDelta;

    float testTime;

    public float RespawnTime;

    NewInputSystem playerControl;

    Rigidbody rg;

    public GameObject Rock;



    void Awake()
    {
        /*
        positionRock = transform.position;
        currentPos = positionRock;
        */
        currentPos = transform.position;
        playerControl = new NewInputSystem();
        //OWI = new OpenWorldInventory();
        rg = gameObject.GetComponent<Rigidbody>();
        countHit = 0;
        canCollect = true;
        testTime = 0f;
    }
    private void OnEnable()
    {
        playerControl.Enable();
    }
    private void OnDisable()
    {
        playerControl.Disable();
    }


    void Update()
    {
        if(_jumpTimeoutDelta>0.0f)
        {
            _jumpTimeoutDelta-=Time.deltaTime;
            isCollectionNeeded = true;
        }
        else
           isCollectionNeeded = false;


        if(freezTime>0.0f)
        {
            freezTime-= Time.deltaTime;
        }

        if (startTime)
        {
            testTime += Time.deltaTime;
        }

        if (testTime > RespawnTime)
        {
            SpawnAndDestory();
        }


    }

   
    void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Axe" && playerControl.Player.Hit.ReadValue<float>() == 1)
        {
            Debug.Log("Axe hitted");
            countHit++;
            isCollectionNeeded = true;
            Debug.Log("Count: " + countHit);
        
            _jumpTimeoutDelta = 5f;


            if (countHit > destroyHit)
            {
                Debug.Log("Destroy");
                foreach (Transform child in transform)
                {
                    child.gameObject.SetActive(false);
                }
               // gameObject.transform.GetChild(0).gameObject.SetActive(false);
                countHit = 0;
                Show();
            }

        }
        else
        {

            isCollectionNeeded = false;
        }

    }

    void Show()
    {
        startTime = true;
        canCollect = false;
    }

    void SpawnAndDestory()
    {

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
       // gameObject.transform.GetChild(0).gameObject.SetActive(true);
        testTime = 0;
        startTime = false;
        canCollect = true;

    }
}
