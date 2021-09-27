using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerAngleControler : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    public Camera cam;
    public Player player;
    private int width;
    private int height;

    private Vector2 startPos;
    private Vector3 camAngle;
    private Vector3 playerAngle;

    private void Awake()
    {
        width = Screen.width;
        height = Screen.height;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log(cam.transform.eulerAngles);
        player.transform.eulerAngles = playerAngle + new Vector3(0, -(eventData.position.x - startPos.x) / width * 360, 0);
        cam.transform.localEulerAngles = camAngle + new Vector3(-(eventData.position.y - startPos.y) / height * 360, 0, 0);
        //player.transform.eulerAngles = playerAngle + new Vector3(0, -(eventData.position.x - startPos.x) / width * 360, 0);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(eventData.position);
        startPos = eventData.position;
        camAngle = new Vector3(cam.transform.localEulerAngles.x, cam.transform.localEulerAngles.y, 0);
        playerAngle = new Vector3(0, player.transform.eulerAngles.y, 0);
    }
}
