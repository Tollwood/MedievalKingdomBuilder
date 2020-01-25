using System;
using UnityEngine;


public class CameraZoom : MonoBehaviour, OnLoadSupport
{
    public new Transform camera;
    public Vector3 zoomAmount;
    public float speed;
    public float minZoom;
    public float maxZoom;

    // Start is called before the first frame update
    void Start()
    {
        camera.position = SaveData.current.zoom;
        OnLoadSubscriber.current.AddOnLoadSubscriber(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (SaveData.current.gameState != GameState.PLAYING) return;
        Vector3 newZoom = camera.position;

        if (Input.GetKey(KeyCode.X) && ShouldZoomOut())
        {
            newZoom += zoomAmount;
        }

        if (Input.GetKey(KeyCode.Y) && ShouldZoomIn())
        {
            newZoom -= zoomAmount;
        }

        camera.position = Vector3.Lerp(camera.position, newZoom, Time.deltaTime * speed);
        SaveData.current.zoom = camera.position;
    }

    private bool ShouldZoomIn()
    {
        return camera.position.y - zoomAmount.y > minZoom;
    }

    private bool ShouldZoomOut()
    {
        return camera.position.y + zoomAmount.y < maxZoom;
    }
    public void OnLoad(){
        camera.position = SaveData.current.zoom;
    }
}
