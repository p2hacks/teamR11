using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class date : MonoBehaviour
{
    
    //[SerializeField]
    public TextMeshPro textbox;
    // Start is called before the first frame update
    void Start()
    {
        var dt = DateTime.Now;

        Debug.Log(dt.Year);         // 現在日付の年部分を取得
        Debug.Log(dt.Month);        // 現在日付の月部分を取得
        Debug.Log(dt.Day);          // 現在日付の日部分を取得
        Debug.Log(dt.DayOfWeek);  // 現在日付の曜日部分を取得
        textbox = this.GetComponent<TextMeshPro>();
        textbox.text = dt.Year +"/"+ dt.Month + "/"+ dt.Day;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
