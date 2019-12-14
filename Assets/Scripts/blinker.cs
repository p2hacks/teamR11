using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blinker : MonoBehaviour
{    
    // 第二引数をTrueにするとHDRカラーパネルになる
    [ColorUsage(false, true)] public Color color1;
    [ColorUsage(false, true)] public Color color2;
    private MeshRenderer r;
    private float timer;
    private bool isSwitch;
    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<MeshRenderer>();
        // マテリアルの変更するパラメータを事前に知らせる
        r.material.EnableKeyword("_EMISSION");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        // 1秒ごとに色変える
        if (timer > 1f)
        {
            if (isSwitch)
            {
                r.material.SetColor("_EmissionColor", color1);
                isSwitch = false;
            }
            else
            {
                r.material.SetColor("_EmissionColor", color2);
                isSwitch = true;
            }
            // タイマーリセット
            timer = 0f;
        }
    }
}
