using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{

    public PanelManager instance;

    public List<Panel> panelList = new List<Panel>();

    void Awake()
    {
        if (!instance) {
            Debug.Log("Set instance " + " PlayerCardManager");
            instance = this;
        }
        else {
            Destroy(this);
        }

        //Add all the panel to a list to make future looping more performant
        foreach(Panel panel in transform.GetComponentsInChildren<Panel>()) {
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
