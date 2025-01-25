using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerShow : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    void Update()
    {
        text.text = Timer.elapsedTime.ToString();
    }
}
