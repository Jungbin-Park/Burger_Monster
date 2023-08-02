using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class UI_HPBar : MonoBehaviour
{
    Stat stat;

    // Start is called before the first frame update
    void Start()
    {
         stat = transform.parent.GetComponent<Stat>();
    }

    // Update is called once per frame
    void Update()
    {
        Transform parent = transform.parent;
        transform.position = parent.position + Vector3.up * 2.0f;
        transform.rotation = Camera.main.transform.rotation;

        float ratio = stat.Hp / (float)stat.MaxHp;
        SetHpRatio(ratio);
    }

    public void SetHpRatio(float ratio)
    {
        GetComponentInChildren<Slider>().value = ratio;
    }
}
