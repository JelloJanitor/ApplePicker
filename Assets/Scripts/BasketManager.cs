using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BasketManager : MonoBehaviour
{
    // Input reference for movement
    public InputActionReference move;
    public InputActionReference pause;

    // Checks for if input can be taken
    bool canPause = false; // Check for pause input
    bool isTakingInput = false; // Check for mouse input

    // Basket prefab and list
    public GameObject basketPrefab;
    List<GameObject> basketList;
    
    // Subscribe to actions
    private void OnEnable()
    {
        GameManager.Instance.OnGameStart += CreateBaskets;
        GameManager.Instance.OnGamePause += TogglePlayerControl;
        GameManager.Instance.OnAppleMiss += DeleteBasket;
        GameManager.Instance.OnGameReset += RemovePauseAbility;
    }

    // Create baskets
    void CreateBaskets()
    {
        // Initialize basketList
        basketList = new List<GameObject>();
        // Create numBaskets number of baskets
        for (int i = 0; i < GameManager.Instance.numBaskets; i++)
        {
            GameObject tBasketGO = Instantiate<GameObject>(basketPrefab);
            Vector3 pos = Vector3.zero;
            pos.y = GameManager.Instance.basketBottomY + (GameManager.Instance.basketSpacingY * i);
            tBasketGO.transform.position = pos;
            basketList.Add(tBasketGO);
        }

        canPause = true;
    }

    void Awake()
    {
        // Enable input action
        move.action.Enable();
        pause.action.Enable();
    }

    void RemovePauseAbility()
    {
        canPause = false;
    }

    void TogglePlayerControl()
    {
        isTakingInput = !isTakingInput;
    }

    // Destroy and delete top basket
    void DeleteBasket()
    {
        if (basketList.Count > 0)
        {
            Destroy(basketList[basketList.Count - 1]);
            basketList.RemoveAt(basketList.Count - 1);
        }
    }

    // Runs every frame
    void Update()
    {
        // Check for pause button
        if (canPause && pause.action.WasPressedThisFrame())
        {
            GameManager.Instance.TogglePause();
        }

        // Get the current screen position of the mouse from Input
        Vector3 mousePos2D = move.action.ReadValue<Vector2>();

        // The Camera's z position sets how far to push the mouse into 3D
        // If this line causes a NullReferenceException, select the Main Camera
        //  in the Hierarchy and set its tag to MainCamera in the Inspector.
        mousePos2D.z = -Camera.main.transform.position.z;

        // Convert the point from 2D screen space into 3D game world space
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        // Move baskets if game is not paused
        if (isTakingInput)
        {
            for (int i = 0; i < basketList.Count; i++)
            {
                // Move the x position of this Basket to the x position of the Mouse
                Vector3 pos = basketList[i].transform.position;
                pos.x = mousePos3D.x;
                basketList[i].transform.position = pos;
            }
        }
    }
}
