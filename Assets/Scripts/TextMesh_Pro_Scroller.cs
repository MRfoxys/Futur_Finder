using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TextMesh_Pro_Scroller : MonoBehaviour
{

    public TextMeshProUGUI textMeshPro;
    public float scrollSpeed = 1.0f;

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
        textRectTransform = textMeshPro.GetComponent<RectTransform>();
        textStartPosition = textRectTransform.anchoredPosition;
        CalculateTextLength();
        
        
    }

    private void Update()
    {
        
            textRectTransform.anchoredPosition += Vector2.left * scrollSpeed * Time.deltaTime;

            if (textRectTransform.anchoredPosition.x < textStartPosition.x - textLength)
            {
                textRectTransform.anchoredPosition = textStartPosition;
                cpt++;
                if(cpt == max_iter)
                {
                    //stop the notif
                    this.gameObject.SetActive(false);
                }
            }

      
    }

    // Function to calculate the length of the current text
    private void CalculateTextLength()
    {
        textLength = textMeshPro.GetPreferredValues().x;
    }

}
