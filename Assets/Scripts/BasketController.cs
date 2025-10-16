using UnityEngine;
using UnityEngine.InputSystem;

public class BasketController : MonoBehaviour
{
    public InputActionReference move;
    private GameUIController gameUI;

    void Start()
    {
        move.action.Enable();

        GameObject gameUIGo = GameObject.Find("GameUI");
        gameUI = gameUIGo.GetComponent<GameUIController>();
    }

    void Update()
    {
        // Get the current screen position of the mouse from Input
        Vector3 mousePos2D = move.action.ReadValue<Vector2>();
        //Vector3 mousePos2D = Input.mousePosition;

        // The Camera's z position sets how far to push the mouse into 3D
        // If this line causes a NullReferenceException, select the Main Camera
        //  in the Hierarchy and set its tag to MainCamera in the Inspector.
        mousePos2D.z = -Camera.main.transform.position.z;

        // Convert the point from 2D screen space into 3D game world space
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        // Move the x position of this Basket to the x position of the Mouse
        Vector3 pos = this.transform.position;
        pos.x = mousePos3D.x;
        this.transform.position = pos;
    }

    private void OnCollisionEnter(Collision coll)
    {
        // Find out what hit this basket
        GameObject collidedWith = coll.gameObject;
        if (collidedWith.CompareTag("Apple"))
        {
            Destroy(collidedWith);
            // Increase the score
            gameUI.score += 100;
            HighScoreController.TRY_SET_HIGH_SCORE(gameUI.score);
        }
    }
}
