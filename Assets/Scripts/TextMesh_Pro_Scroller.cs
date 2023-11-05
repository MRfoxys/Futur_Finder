using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TextMesh_Pro_Scroller : MonoBehaviour
{


    private bool active;
    private Vector2 iniPos;
    private Vector2 iniPos_text;
    private Vector2 targetPos;
    [SerializeField] public float speed_down = 500f;
    private float scrollAmount;
    private GameObject notif;




    public TextMeshProUGUI textMeshPro;
    [SerializeField] public float scrollSpeed = 200f;
    private RectTransform notifRectTransform;

    private RectTransform textRectTransform;
    private Vector3 textStartPosition;
    private float textLength;
    [SerializeField] private const int max_iter = 2;
    private int cpt = 0 ;


    private void OnEnable()
    {
        notifRectTransform.anchoredPosition = new Vector2(notifRectTransform.anchoredPosition.x, iniPos.y);
        textRectTransform.anchoredPosition = new Vector2(iniPos_text.x, iniPos_text.y);
        cpt = 0;
    }


    private void Start()
    {
        notif = this.gameObject;
        notifRectTransform= notif.GetComponent<RectTransform>();
        scrollAmount = notifRectTransform.rect.height;
        iniPos = new Vector2(notifRectTransform.anchoredPosition.x, notifRectTransform.anchoredPosition.y + scrollAmount);
        



        Debug.Log(scrollAmount);
        notifRectTransform.anchoredPosition = new Vector2(notifRectTransform.anchoredPosition.x, notifRectTransform.anchoredPosition.y);
        textRectTransform = textMeshPro.GetComponent<RectTransform>();
        textStartPosition = textRectTransform.anchoredPosition;
        iniPos_text = new Vector2(textRectTransform.anchoredPosition.x, textRectTransform.anchoredPosition.y);
        CalculateTextLength();
        targetPos = new Vector2(textRectTransform.anchoredPosition.x, notifRectTransform.anchoredPosition.y );
        
    }

    private void Update()
    {
        if (cpt == max_iter)
        {

            if(notifRectTransform.anchoredPosition.y < iniPos.y)
            {
                notifRectTransform.anchoredPosition += Vector2.up * speed_down * Time.deltaTime;
            }
            else
            {
                //stop the notif
                this.gameObject.SetActive(false);
            }


        }
        else 
        { 

            if (notifRectTransform.anchoredPosition.y < targetPos.y)
            {
                notifRectTransform.anchoredPosition = new Vector2(notifRectTransform.anchoredPosition.x, targetPos.y);
            }
            else if (notifRectTransform.anchoredPosition.y == targetPos.y)
            {

                textRectTransform.anchoredPosition += Vector2.left * scrollSpeed * Time.deltaTime;

                if (textRectTransform.anchoredPosition.x < textStartPosition.x - textLength)
                {
                    textRectTransform.anchoredPosition = textStartPosition;
                    cpt++;



                }

        }
        else
        {
            notifRectTransform.anchoredPosition += Vector2.down * speed_down * Time.deltaTime;
        }
    }
        
           

      
    }

    // Function to calculate the length of the current text
    private void CalculateTextLength()
    {
        textLength = textMeshPro.GetPreferredValues().x;
    }

}
