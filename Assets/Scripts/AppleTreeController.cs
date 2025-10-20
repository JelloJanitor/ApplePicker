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

    // List of created apples
    List<GameObject> appleList = new List<GameObject>();

    // Speed at which the AppleTree moves
    public float speed = 1f;

    // Distance where AppleTree turns around
    public float leftAndRightEdge = 10f;

    // Chance that the AppleTree will change directions
    public float changeDirChance = 0.1f;

    // Seconds between Apples instantiations
    public float appleDropDelay = 1f;

    // Lock for if an apple drop has already been invoked
    private bool isDropQueued;

    // Lock if the game is paused
    private bool canDrop;

    // Subscribe to actions
    private void OnEnable()
    {
        GameManager.Instance.OnGameStart += StartTree;
        GameManager.Instance.OnAppleMiss += DeleteApples;
        GameManager.Instance.OnGamePause += PauseTree;
        GameManager.Instance.OnGameReset += ResetTree;
    }

    // Allows the tree to start dropping apples
    void Start()
    {
        StartTree();
    }

    // Starts dropping apples with a 2s initial delay
    void StartTree()
    {
        canDrop = true;

        isDropQueued = true;
        Invoke("DropApple", 2f);
    }

    // Toggle canDrop
    void PauseTree()
    {
        canDrop = !canDrop;
    }

    // Delete apples and cancel any invokes for apple drops
    private void ResetTree()
    {
        canDrop = false;
        DeleteApples();

        CancelInvoke("DropApple");

        Vector3 pos = transform.position;
        pos.x = 0;
        transform.position = pos;
    }

    // Drop an apple from the tree
    void DropApple()
    {
        if (canDrop)
        {
            GameObject tAppleGO = Instantiate<GameObject>(applePrefab);
            tAppleGO.transform.position = this.transform.position;
            appleList.Add(tAppleGO);

            isDropQueued = false;
        }
    }

    // Destroy all apples and delete apples from the appleList
    void DeleteApples()
    {
        for (int i = appleList.Count - 1; i >= 0; i--)
        {
            Destroy(appleList[i]);
            appleList.RemoveAt(i);
        }
    }

    // Things to run every frame
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

        // Drop apple if no other apple queued and apple dropping is enabled
        if (!isDropQueued && canDrop)
        {
            isDropQueued = true;
            Invoke("DropApple", appleDropDelay);
        }
    }

    // Update the tree's movement
    private void FixedUpdate()
    {
        if (Random.value < changeDirChance)
        {
            speed *= -1;
        }
    }
}
