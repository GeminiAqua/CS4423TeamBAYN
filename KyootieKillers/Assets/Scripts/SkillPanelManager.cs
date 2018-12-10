using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanelManager: MonoBehaviour {

    [System.Serializable]
    public class SkillPanel
    {
        public Image skill1;
        public Image skill2;
        public Image skill3;
    }
    [System.Serializable]
    public class SkillIcon
    {
        public Image swirl;
        public Image tripleSwords;
        public Image giantSword;
    }
    private string[] skillNames = new string[] { "Swirl", "Red Hot Tamales", "Giant Post" };
    public GodrickController controller;
    public SkillPanel skillPanel = new SkillPanel();
    public SkillIcon skillIcon = new SkillIcon();
    // Use this for initialization
    void Start()
    {
        controller = GameObject.Find("Godrick").GetComponent<GodrickController>();

    }

    // Update is called once per frame
    void Update()
    {
        CheckSkills();
    }
    void CheckSkills()
    {

        if (controller.skillCD.canCastOne)
        {
            activateIcon(skillPanel.skill1, skillIcon.swirl);
        }
        if (controller.skillCD.canCastTwo)
        {
            activateIcon(skillPanel.skill2, skillIcon.tripleSwords);
        }
        if (controller.skillCD.canCastThree)
        {
            activateIcon(skillPanel.skill3, skillIcon.giantSword);
        }

    }
    void inactivateIcon(Image skillPanel, Image icon)
    {
        skillPanel.color = new Color(1, 1, 1, 0.39f);
        icon.color = new Color(0.19f, 0.19f, 0.19f, 0.19f);
    }
    void activateIcon(Image skillPanel, Image icon)
    {
        skillPanel.color = new Color(0.098f, 0.76f, 0f, 1);
        icon.color = new Color(1, 1, 1, 1);
    }
}
