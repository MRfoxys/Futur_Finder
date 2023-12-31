using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    [SerializeField] private string Message;
        
    private GameObject ColorBlind_Destroy;

    private GameObject ColorBlind_Keep;

    DragController dragController;

    private Vector2 referenceResolution;

    [SerializeField]private float scaleFactor;

    private Camera main ;


    [SerializeField] private bool isNegative= true;




    private void Awake()
    {
        dragController = GameObject.Find("Drag_controller").GetComponent<DragController>();
        
        ColorBlind_Destroy = dragController.getBlindDestroy();
        ColorBlind_Keep = dragController.getBlingdKeep();


    }
    



    // Start is called before the first frame update
    void Start()
    {
        main = Camera.current;
        ColorBlind_Destroy.SetActive(false);
        ColorBlind_Keep.SetActive(false);



        referenceResolution = new Vector2(1080, 1920);
        //scaleFactor = 0.3f;

        float widthRatio = Screen.width / referenceResolution.x;
        float heightRatio = Screen.height / referenceResolution.y;
        float minRatio = Mathf.Min(widthRatio, heightRatio);
        transform.localScale = new Vector3(minRatio * scaleFactor, minRatio * scaleFactor, 1f);


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


                if(isNegative)
                {
                    dragController.NewNotification(Message);
                }

                dragController.DestroyMe(this.gameObject);

                return;
            }

        }
        else if (collision.gameObject.CompareTag("collider_keeper"))
        {
            if (!dragController.getIsDragActive())
            {
                this.gameObject.SetActive(false);

                if(!isNegative)
                {
                    dragController.NewNotification(Message);
                }

                dragController.DestroyMe(this.gameObject);

                return;
            }
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("collider_Delete"))
        {
            ColorBlind_Destroy.SetActive(false);

        }
        else if(collision.gameObject.CompareTag("collider_keeper"))
        {
            ColorBlind_Keep.SetActive(false);

        }
    }




}
