using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TextMesh_Pro_Scroller : MonoBehaviour
{


    private bool active;
    private Vector2 iniPos;
    private Vector2 targetPos;
    public float speed_down = 500f;
    private float scrollAmount;
    private GameObject notif;




    public TextMeshProUGUI textMeshPro;
    public float scrollSpeed = 1.0f;
    private RectTransform notifRectTransform;

    private RectTransform textRectTransform;
    private Vector3 textStartPosition;
    private float textLength;
    [SerializeField] private const int max_iter = 3;
    private int cpt = 0 ;


    private void OnEnable()
    {
        cpt = 0;
    }


    private void Start()
    {
        notif = this.gameObject;
        notifRectTransform= notif.GetComponent<RectTransform>();
        scrollAmount = notifRectTransform.rect.height;
        iniPos = new Vector2(notifRectTransform.anchoredPosition.x, notifRectTransform.anchoredPosition.y);

        Debug.Log(scrollAmount);
        notifRectTransform.anchoredPosition = new Vector2(notifRectTransform.anchoredPosition.x, notifRectTransform.anchoredPosition.y + scrollAmount);
        textRectTransform = textMeshPro.GetComponent<RectTransform>();
        textStartPosition = textRectTransform.anchoredPosition;
        CalculateTextLength();
       iniPos= new Vector2(textRectTransform.anchoredPosition.x, textRectTransform.anchoredPosition.y + scrollAmount);
        
    }

    private void Update()
    {
        if (notifRectTransform.anchoredPosition.y < iniPos.y)
        {
            notifRectTransform.anchoredPosition = new Vector2(notifRectTransform.anchoredPosition.x, iniPos.y);
        }
        else if (notifRectTransform.anchoredPosition.y == iniPos.y )
        {

            textRectTransform.anchoredPosition += Vector2.left * scrollSpeed * Time.deltaTime;

            if (textRectTransform.anchoredPosition.x < textStartPosition.x - textLength)
            {
                textRectTransform.anchoredPosition = textStartPosition;
                cpt++;
                if (cpt == max_iter)
                {
                    //stop the notif
                    this.gameObject.SetActive(false);
                }
            }

        }
        else
        {
            notifRectTransform.anchoredPosition += Vector2.down * speed_down * Time.deltaTime;
        }
        
           

      
    }

    // Function to calculate the length of the current text
    private void CalculateTextLength()
    {
        textLength = textMeshPro.GetPreferredValues().x;
    }

}
