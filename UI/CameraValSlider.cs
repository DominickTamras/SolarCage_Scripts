using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CameraValSlider : MonoBehaviour
{
    public PlayerCameraSens camValSlider;

    public TextMeshProUGUI valShow;

    private void Update()
    {
        valShow.text = camValSlider.camsensX.ToString();
        gameObject.GetComponent<Slider>().value = camValSlider.camsensX;
    }
    public void SensChange(float newSens)
    {
        camValSlider.camsensX = newSens;

        camValSlider.camsensY = newSens;


    }
}
