using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class Viewership:MonoBehaviour
{
    Slider ViewershipSlider;

    void Start()
    {
        ViewershipSlider = GetComponent<Slider>();

        float maxViewership = 100.0f;
        float nowViewership = 50.0f;

        //スライダーの最大値
        ViewershipSlider.maxValue = maxViewership;

        //スライダーの現在地
        ViewershipSlider.value = nowViewership;
    }

    void Update()
    {

    }
}
