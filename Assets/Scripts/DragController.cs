using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class DragController : MonoBehaviour
{

    private bool _isDragActive = false;

    private Vector2 _screenPosition;

    private Vector3 _worldPosition;

    private Draggable _lastDragged;

    GameObject Notification;

    TextMeshProUGUI textMeshPro;

    List<int> indicesList = new List<int>();

    public List<GameObject> prefabsList;
    void Awake()
    {

        for (int i = 0; i <2; i++)
        {
            indicesList.Add(i+1);
        }

        DragController[] controllers = FindObjectsOfType<DragController>();
        if (controllers.Length > 1)
        {
            Destroy(gameObject);
        }

        Notification = GameObject.Find("Notification");

        Notification.SetActive(false);

        textMeshPro = Notification.transform.Find("Notif_text").GetComponent<TextMeshProUGUI>();
    }

    // Start is called before the first frame update
    void Start()
    {
        

    }


    // Update is called once per frame
    void Update()
    {

        if (_isDragActive && (Input.GetMouseButtonDown(0) || (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)))
        {
            Drop();
            return;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Input.mousePosition;
            _screenPosition = new Vector2(mousePos.x, mousePos.y);
            Drag();
        }
        else if (Input.touchCount > 0)
        {
            _screenPosition = Input.GetTouch(0).position;
            Drag();
        }
        else
        {
            return;
        }

        _worldPosition = Camera.main.ScreenToWorldPoint(_screenPosition);

        if (!_isDragActive)
        {
            Drag();
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(_worldPosition, Vector2.zero);
            if (hit.collider != null)
            {
                Draggable draggables = hit.transform.gameObject.GetComponent<Draggable>();
                _lastDragged = draggables;
                InitDrag();

            }
        }

    }

    void Drag()
    {
        _isDragActive = true;
    }

    void InitDrag()
    {
        _lastDragged.transform.position = new Vector2(_worldPosition.x, _worldPosition.y);
    }

    void Drop()
    {
        _isDragActive = false;
    }


    public bool getIsDragActive()
    { return _isDragActive; }

    public void NewNotification(string Message)
    {
        Notification.SetActive(false);
        textMeshPro.text = "News : " + Message;
        Notification.SetActive(true);
    }

    public void DestroyMe(GameObject Caller)
    {
        Destroy(Caller);
    }


    private void  Callprafabe()
    {
        int randomIndex = Random.Range(0, prefabsList.Count); // Génère un indice aléatoire dans la plage de la liste.

        // Instancie le préfab correspondant à l'indice aléatoire.
        GameObject spawnedPrefab = Instantiate(prefabsList[randomIndex], transform.position, Quaternion.identity);

        // Supprime l'élément de la liste pour éviter de le réutiliser.
        prefabsList.RemoveAt(randomIndex);

    }

}
