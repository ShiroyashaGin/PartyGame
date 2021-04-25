using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{

    public static PanelManager instance;

    public Panel gamePanel, lobbyPanel;

    public List<Panel> panelList = new List<Panel>();

    void Awake()
    {
        if (!instance) {
            instance = this;
        }
        else {
            Destroy(this);
        }

        //Add all the panel to a list to make future looping more performant
        foreach(Panel panel in transform.GetComponentsInChildren<Panel>(true)) {
            panelList.Add(panel);
        }
    }

    public void SwitchPanel(Panel panel) {
        foreach(Panel _panel in panelList) {
                _panel.gameObject.SetActive(_panel == panel);
        }
    }

    public void DisablePanel(Panel panel) {
        panel.gameObject.SetActive(false);
    }

    public void EnablePanel(Panel panel) {
        panel.gameObject.SetActive(true);
    }
}
