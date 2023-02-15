using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSway : MonoBehaviour
{
    public float amount;
    
    public float maxAmount;

    public float amount_2;

    public float maxAmount_2;

    public float smoothing;

    public float smoothing_2;

    public GameObject bhg;

    private Vector3 startPosition;

    private Vector3 startPosition_2;

    private Quaternion startPosition_3;


    void Start()
    {
        startPosition = transform.localPosition;

        startPosition_2 = transform.localPosition;

        startPosition_3 = transform.localRotation;

    }

    // Update is called once per frame
    void Update()
    {
        float movementX = -Input.GetAxis("Mouse X") * amount; // Grabs the mouse movements
        float movementY = -Input.GetAxis("Mouse Y") * amount;

        movementX = Mathf.Clamp(movementX, -maxAmount, maxAmount); //clamps the movement between two points
        movementY = Mathf.Clamp(movementY, -maxAmount, maxAmount);

        Vector3 finalPosition = new Vector3(movementX, movementY, 0);
        transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition + startPosition, Time.deltaTime * smoothing); 
    }

    public void GunKickBack()
    {
        float movementX_2 = -Input.GetAxis("Mouse X") * amount; // Grabs the mouse movements
        float movementY_2 = -Input.GetAxis("Mouse Y") * amount;
     

         movementX_2 = Mathf.Clamp(movementX_2, -maxAmount_2, maxAmount_2); //clamps the movement between two points
         movementY_2 = Mathf.Clamp(movementY_2, -maxAmount_2, maxAmount_2);

        Vector3 finalPosition = new Vector3(movementX_2, movementY_2, -5);

        bhg.transform.localPosition = Vector3.Lerp(bhg.transform.localPosition, finalPosition + startPosition_2, Time.deltaTime * smoothing_2);

    }
}
