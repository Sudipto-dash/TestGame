using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class TreeCutting : MonoBehaviour
{

    
    public GameObject treeTrunk;

    GameObject temp;
    
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
    
    private float freezTime = 50f;

    bool DoneTheJob = false;

    bool StartTime = false;

    bool DestroyNow = false;

    bool CanCollect;

   

    public float RespawnTime;

    float testTime;

    float _jumpTimeoutDelta;

    NewInputSystem playerControl;

    Rigidbody rg;


    private void Awake()
    {
        playerControl = new NewInputSystem();
        rg = gameObject.GetComponent<Rigidbody>();
        countHit = 0;
        currentPos = transform.position;
        CanCollect = true;
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

    void Start()
    {
        currentPos = new Vector3(currentPos.x, 2.75f, currentPos.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (_jumpTimeoutDelta > 0.0f)
        {
            _jumpTimeoutDelta -= Time.deltaTime;
            isCollectionNeeded = true;
        }
        else
            isCollectionNeeded = false;


        if (freezTime > 0.0f)
        {
            freezTime -= Time.deltaTime;
        }
        /*else
            rg.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;*/


        if(StartTime)
        {
            testTime += Time.deltaTime;
        }

        if(testTime>RespawnTime)
        {
            SpawnAndDestory();
        }





    }

    void OnCollisionEnter(Collision other)
    {
        if (CanCollect)
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
                    gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    countHit = 0;
                    Show();
                }

            }
            else
            {

                isCollectionNeeded = false;
            }
        }
        
       

    }

    void Show()
    {
        temp = new GameObject();
        temp = Instantiate(treeTrunk, currentPos, Quaternion.identity);
        StartTime = true;
        CanCollect = false;
    }

    void SpawnAndDestory()
    {
        Destroy(temp);
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        testTime = 0;
        StartTime = false;
        CanCollect = true;

    }

    
   
}
