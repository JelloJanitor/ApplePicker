using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;

public class AppleTreeController : MonoBehaviour
{
    [Header("Inscribed")]
    // Prefab for instantiating apples
    public GameObject applePrefab;
    List<GameObject> appleList = new List<GameObject>();

    // Speed at which the AppleTree moves
    public float speed = 1f;

    // Distance where AppleTree turns around
    public float leftAndRightEdge = 10f;

    // Chance that the AppleTree will change directions
    public float changeDirChance = 0.1f;

    // Seconds between Apples instantiations
    public float appleDropDelay = 1f;

    private float lastAppleDropTime;
    private bool isDropQueued;
    private bool canDrop;


    private void OnEnable()
    {
        GameManager.Instance.OnGameStart += StartTree;
        GameManager.Instance.OnAppleMiss += DeleteApples;
        GameManager.Instance.OnGamePause += PauseTree;
        GameManager.Instance.OnGameReset += ResetTree;
    }
    void Start()
    {
        // Start dropping apples
        StartTree();
        //Invoke("DropApple", 2f);
    }

    void StartTree()
    {
        canDrop = true;

        lastAppleDropTime = Time.time;
        isDropQueued = true;
        Invoke("DropApple", 2f);
    }

    void PauseTree()
    {
        canDrop = !canDrop;
    }

    private void ResetTree()
    {
        canDrop = false;
        DeleteApples();

        Vector3 pos = transform.position;
        pos.x = 0;
        transform.position = pos;
    }

    void DropApple()
    {
        //GameObject apple = Instantiate<GameObject>(applePrefab);
        //apple.transform.position = transform.position;
        //Invoke("DropApple", appleDropDelay);
        
        if (canDrop)
        {
            GameObject tAppleGO = Instantiate<GameObject>(applePrefab);
            tAppleGO.transform.position = this.transform.position;
            appleList.Add(tAppleGO);

            lastAppleDropTime = Time.time;
            isDropQueued = false;
        }

        //Invoke("TryDropApple", appleDropDelay);
    }

    void DeleteApples()
    {
        for (int i = appleList.Count - 1; i >= 0; i--)
        {
            Destroy(appleList[i]);
            appleList.RemoveAt(i);
            //GameObject tAppleGo = appleList[i];
            //appleList.Remove();
            //Destroy(tAppleGo);
        }
    }

    void Update()
    {
        // Basic Movement
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;

        // Changing Direction
        if (pos.x < -leftAndRightEdge)
        {
            speed = Mathf.Abs(speed); // Move right
        }
        else if (pos.x > leftAndRightEdge)
        {
            speed = -Mathf.Abs(speed); // Move left
        }

        if (!isDropQueued && canDrop)
        {
            isDropQueued = true;
            Invoke("DropApple", appleDropDelay);
        }
    }

    private void FixedUpdate()
    {
        if (Random.value < changeDirChance)
        {
            speed *= -1;
        }
    }
}
