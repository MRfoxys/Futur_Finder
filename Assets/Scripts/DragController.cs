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

    //private string pre_prefabname = "profil_";

    private string prefabName = "profil_";

    private GameObject ColorBlind_Destroy;

    private GameObject ColorBlind_Keep;



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

        ColorBlind_Destroy = GameObject.Find("Color_Blind_Destroy");
        ColorBlind_Keep = GameObject.Find("Color_Blind_Keep");

        Notification = GameObject.Find("Notification");

        Notification.SetActive(false);

        textMeshPro = Notification.transform.Find("Notif_text").GetComponent<TextMeshProUGUI>();

       // tempo_prefab();
    }

    // Start is called before the first frame update
    void Start()
    {
        ColorBlind_Destroy.SetActive(false);
        ColorBlind_Keep.SetActive(false);

        tempo_prefab();

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


    public GameObject getBlindDestroy()
    {
        return ColorBlind_Destroy;
    }

    public GameObject getBlingdKeep()
    {
        return ColorBlind_Keep;
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
        tempo_prefab();
        Destroy(Caller);
    }


    private void  Callprefabe()
    {
        int randomIndex = Random.Range(0, indicesList.Count); // Génère un indice aléatoire dans la plage de la liste.

        // Instancie le préfab correspondant à l'indice aléatoire.
        GameObject prefab = Resources.Load("Prefab/" + prefabName + indicesList[randomIndex]) as GameObject;
       // GameObject spawnedPrefab = Instantiate(prefabsList[randomIndex], transform.position, Quaternion.identity);



        // Supprime l'élément de la liste pour éviter de le réutiliser.
        prefabsList.RemoveAt(randomIndex);


        if (prefab != null)
        {
            // Instanciez le prefab dans la scène
            Instantiate(prefab, transform.position, transform.rotation);
        }
        else
        {
            UnityEngine.Debug.LogError("Prefab introuvable : " + prefabName + indicesList[randomIndex]);
        }

    }


    private void tempo_prefab()
    {
        // GameObject prefab = Resources.Load("Prefab/" + prefabName + "3") as GameObject;

        GameObject prefab = Resources.Load("profil_"+"3") as GameObject;


        if (prefab != null)
        {
            // Instanciez le prefab dans la scène
            Instantiate(prefab, new Vector2(0, 1.7f), transform.rotation);
        }
        else
        {
            UnityEngine.Debug.LogError("Prefab introuvable : " + prefabName + "3");
        }

    }

}
