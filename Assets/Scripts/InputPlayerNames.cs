using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class InputPlayerNames : MonoBehaviour
{
    [SerializeField]
    TMP_InputField inputfield;

    // Start is called before the first frame update
    void Start()
    {
        inputfield.Select();
    }

    public void EnterName() {
        Debug.Log("check");
        PlayerManager.instance.AddPlayer(inputfield.text);
        inputfield.text = string.Empty;
        Invoke("Select", 0.1f);
        
    }

    [ContextMenu("Select")]
    public void Select() {
        EventSystem.current.SetSelectedGameObject(null);
        inputfield.Select();
    }
}
