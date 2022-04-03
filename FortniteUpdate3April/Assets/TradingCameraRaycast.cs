using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class TradingCameraRaycast : MonoBehaviour
{

    NewInputSystem playerControls;

    public GameObject CanvasTrading;


    public Camera fpsCamera;
    // Start is called before the first frame update

    private void Awake()
    {
        playerControls = new NewInputSystem();
    }

    void Start()
    {

    }

    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }


    // Update is called once per frame
    void Update()
    {
        RayCastHit();
    }



   void RayCastHit()
    {
       RaycastHit _hit;
        Ray ray = fpsCamera.ViewportPointToRay(new Vector3(0.54f, 0.58f));
        if (Physics.Raycast(ray, out _hit, 100f))            //New Addition
        {
            if (_hit.transform.gameObject.tag == "Player")
            {

                Debug.Log("Found :" + _hit.transform.GetComponent<PhotonView>().Owner.NickName);
                // Debug.DrawRay(RaycastPos.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                if (playerControls.Player.Trade.triggered && playerControls.Player.Trade.ReadValue<float>() > 0)
                {
                    CanvasTrading.SetActive(true);
                    //TradeSystem(hit.transform.gameObject);

                }
            }
            else if (CanvasTrading.activeInHierarchy)
            {
                CanvasTrading.SetActive(false);
            }
        }
    }
}
