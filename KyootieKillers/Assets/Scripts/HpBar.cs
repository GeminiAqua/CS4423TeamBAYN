using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{

    [SerializeField] float fillRatio;

    [SerializeField] Image content;
    GodrickController controller;

    void Start()
    {
        controller = GameObject.Find("Godrick").GetComponent<GodrickController>();
        //content = gameObjectGetComponent<Image>();
        content.type = Image.Type.Filled;
        content.fillMethod = Image.FillMethod.Horizontal;
        content.fillOrigin = (int) Image.OriginHorizontal.Left;

    }

    // Update is called once per frame
    void Update()
    {
        fillRatio = controller.gameObject.GetComponent<Health>().GetHealth()/100f;
        updateBar();
    }

    private void updateBar()
    {
        content.fillAmount = fillRatio;

    }
}