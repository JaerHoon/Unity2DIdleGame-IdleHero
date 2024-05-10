using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoundSlider : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI sliderText;

    // Start is called before the first frame update
    void Start()
    {
        slider.onValueChanged.AddListener(Function_Slider);
    }

    private void Function_Slider(float _value)//0 ~ -40
    {
        sliderText.text = Mathf.FloorToInt((_value + 40) / 40 * 100).ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
