using UnityEngine;


public class CameraController : MonoBehaviour, OnLoadSupport
{
    public int movementSpeed;
    public int rotationAmount;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = SaveData.current.cameraRigPosition;
        transform.rotation = SaveData.current.cameraRigRotation;
        OnLoadSubscriber.current.AddOnLoadSubscriber(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (SaveData.current.gameState != GameState.PLAYING) return;
        HandleMovement();
        HandleRotation();
    }

    private void HandleRotation()
    {
        Quaternion newRotation = transform.rotation;

        if (Input.GetKey(KeyCode.Q))
        {
            newRotation *= Quaternion.Euler(Vector3.down* rotationAmount);
        }

        if (Input.GetKey(KeyCode.E))
        {
            newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime);
        SaveData.current.cameraRigRotation = transform.rotation;
    }

    private void HandleMovement()
    {  
        Vector3 newPosition = transform.position + ( transform.rotation *  new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * movementSpeed);
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime);
        SaveData.current.cameraRigPosition = transform.position;
    }

    public void OnLoad(){
        transform.position = SaveData.current.cameraRigPosition;
        transform.rotation = SaveData.current.cameraRigRotation;
    }

}
