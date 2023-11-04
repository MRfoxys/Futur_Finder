using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    [SerializeField] private GameObject ColorBlind_Destroy;

    [SerializeField] private GameObject ColorBlind_Keep;

    DragController dragController;

    



    private void Awake()
    {
        dragController = GameObject.Find("Drag_controller").GetComponent<DragController>();

        ColorBlind_Destroy = GameObject.Find("Color_Blind_Destroy");
        ColorBlind_Keep = GameObject.Find("Color_Blind_Keep");

    }

    // Start is called before the first frame update
    void Start()
    {
        ColorBlind_Destroy.SetActive(false);
        ColorBlind_Keep.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


       Debug.Log("hum tu me touche batar");
        
        if (collision.gameObject.CompareTag("collider_Delete"))
        {
            ColorBlind_Destroy.SetActive(true);
        }
        else if (collision.gameObject.CompareTag("collider_keeper"))
        {

            ColorBlind_Keep.SetActive(true);
        }
        else
        {

        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("collider_Delete"))
        {
            if (!dragController.getIsDragActive())
            {
               this.gameObject.SetActive(false) ;


               // dragController.NewNotification("Je suis une patate j'aime la terre et si t'est pas content nike toi je veux un message plus long pour plus de test car je suis un bon devs");
                dragController.NewNotification("Kamoulox");

                return;
            }

        }
        else if (collision.gameObject.CompareTag("collider_keeper"))
        {
            if (!dragController.getIsDragActive())
            {
                //Debug.Log(" on Keep cette element");
                this.gameObject.SetActive(false);
                Debug.Log("je me desactive");

                return;
            }
        }
        Debug.Log("hum tu me touche batar");

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("collider_Delete"))
        {
            ColorBlind_Destroy.SetActive(false);
            Debug.Log("je me desactive 2");

        }
        else if(collision.gameObject.CompareTag("collider_keeper"))
        {
            ColorBlind_Keep.SetActive(false);
            Debug.Log("je me desactive 3");

        }
        //Debug.Log(" on desactive le blind");
    }




}
