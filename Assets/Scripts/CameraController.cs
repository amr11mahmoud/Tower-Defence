using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

// will Use to move the Camera
public class CameraController : MonoBehaviour
{
    private bool doMovement = true;
    
    // Camera Moving Speed
    public float panSpeed = 30f;
    // Space from top of screen which if we hover the mouse there, Camera will move forward
    public float panBorderThickness = 10f;
    // Control the speed of scrolling 
    public float scrollSpeed = 5f;
    // Variables to determine min and max zoom 
    public float minY = 10f;
    public float maxY = 80f;


    void Update()
    {
        // stop all camera movement if the game is over
        if (GameManager.gameIsOver)
        {
            this.enabled = false;
            return;
        }
        // Set movement Toggle for Debugging
        if (Input.GetKeyDown(KeyCode.Space))
        {
            doMovement = !doMovement;
        }
        
        if (!doMovement)
        {
            return;
        }
        
        // Move Forward, [ Input.mousePosition tells you where the cursor is in screen in Vector3 ]
        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            // [ Vector3.forward == new Vectore3 ( 0f, 0f, 1f )]
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        } 
        
        // Move Back [ Space.World is Important !!]
        else if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        
        // Move Right
        else if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);

        }
        
        // Move Left
        else if (Input.GetKey("a" ) || Input.mousePosition.x <= panBorderThickness)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);

        }
        
        // Zoom In and Out movement [ Input.GetAxis Mouse ScrollWheel gets called after any change on the wheel ]
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        // get Current position and store it in [ Pos variable ]
        Vector3 pos = transform.position;
        // Change the Y axis in that variable
        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        // Mathf.Climp will destrict value between two numbers [ value we want to destrict ,min, max]
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        // Now assign that new position to be the current position 
        transform.position = pos;
    }
}
