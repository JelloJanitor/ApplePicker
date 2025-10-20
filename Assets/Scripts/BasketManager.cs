using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

public class BasketManager : MonoBehaviour
{
    public InputActionReference move;
    public InputActionReference pause;
    bool canPause = false;
    bool isTakingInput = false;

    public GameObject basketPrefab;
    List<GameObject> basketList;
    
    private void OnEnable()
    {
        GameManager.Instance.OnGameStart += CreateBaskets;
        GameManager.Instance.OnGamePause += TogglePlayerControl;
        GameManager.Instance.OnAppleMiss += DeleteBasket;
        GameManager.Instance.OnGameReset += RemovePauseAbility;
    }

    void CreateBaskets()
    {
        basketList = new List<GameObject>();
        for (int i = 0; i < GameManager.Instance.numBaskets; i++)
        {
            //Debug.Log("Incramenting numBaskets");

            GameObject tBasketGO = Instantiate<GameObject>(basketPrefab);
            //GameObject tBasketGO = Instantiate<GameObject>();
            Vector3 pos = Vector3.zero;
            pos.y = GameManager.Instance.basketBottomY + (GameManager.Instance.basketSpacingY * i);
            tBasketGO.transform.position = pos;
            //OnBasketAdd?.Invoke();
            basketList.Add(tBasketGO);
        }

        canPause = true;
    }

    void Awake()
    {
        move.action.Enable();
        pause.action.Enable();

        //GameManager.Instance.OnGameStart += CreateBaskets;
        //GameManager.Instance.OnGamePause += TogglePlayerControl;
        //GameManager.Instance.OnAppleMiss += DeleteBasket;
        //move.action.Enable();

        //GameManager.Instance.IncramentNumBaskets();
    }

    void RemovePauseAbility()
    {
        canPause = false;
    }

    void TogglePlayerControl()
    {
        //if (isActive)
        //{
        //    move.action.Disable();
        //}
        //else
        //{
        //    move.action.Enable();
        //}

        isTakingInput = !isTakingInput;
    }

    //void IncreaseBasketLayer()
    //{
    //    basketLayer++;
    //}

    void DeleteBasket()
    {
        if (basketList.Count > 0)
        {
            Destroy(basketList[basketList.Count - 1]);
            basketList.RemoveAt(basketList.Count - 1);
        }
    }

    void Update()
    {
        if (canPause && pause.action.WasPressedThisFrame())
        {
            GameManager.Instance.TogglePause();
        }

        // Get the current screen position of the mouse from Input
        Vector3 mousePos2D = move.action.ReadValue<Vector2>();
        //Vector3 mousePos2D = Input.mousePosition;

        // The Camera's z position sets how far to push the mouse into 3D
        // If this line causes a NullReferenceException, select the Main Camera
        //  in the Hierarchy and set its tag to MainCamera in the Inspector.
        mousePos2D.z = -Camera.main.transform.position.z;

        // Convert the point from 2D screen space into 3D game world space
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

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
