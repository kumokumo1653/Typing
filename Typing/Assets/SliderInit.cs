using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
public class SliderInit : MonoBehaviourPunCallbacks,NetworkObjectInit

{
    // Start is called before the first frame update
    private RoomManager room;
    [SerializeField] private GameObject master;
    void Awake()
    {
        room = GameObject.Find("RoomManager").GetComponent<RoomManager>();
        ObjectInit();
    }
    void Start()
    {
        
        this.gameObject.GetComponent<Slider>().maxValue = master.GetComponent<MasterClient>().postNumber;
    }

    // Update is called once per frame
    void Update()
    {
        if(room.status == STATUS.FINISHED){
            this.gameObject.SetActive(false);
        } 
    }

    public void ObjectInit(){

        gameObject.name = "Slider" + photonView.OwnerActorNr;
        var disp = GameObject.Find("Canvas").GetComponent<Canvas>();
        gameObject.transform.SetParent(disp.transform);
        RectTransform rect = gameObject.transform as RectTransform;
        rect.localPosition = new Vector3(0, -240 - (photonView.OwnerActorNr - 1) * 30, 0);
        rect.localScale = new Vector3(1, 1, 1); 
    }
}
