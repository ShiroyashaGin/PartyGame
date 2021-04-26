using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

/// <summary>
/// Controls the input field in the lobby screen to enter the player names
/// </summary>
public class InputPlayerNames : MonoBehaviour
{
    [SerializeField]
    TMP_InputField inputfield;
    [SerializeField]
    TMP_Text placeholderText;


    private void Update() {
        inputfield.Select();
    }

    // Start is called before the first frame update
    void Start()
    {
        inputfield.Select();
        PlayerManager.instance.OnPlayerLimitreached += Deactivate;
    }

    public void EnterName() {
        if(inputfield.text != string.Empty) {
            Debug.Log("check");
            PlayerManager.instance.AddPlayer(inputfield.text);
            inputfield.text = string.Empty;
            Select();
            placeholderText.text = string.Format("<i>Enter name</i> <size=36>({0}/{1})", PlayerManager.instance.playersList.Count, PlayerManager.instance.maxAmountOfPlayers);
        }
    }

    public void Select() {
        EventSystem.current.SetSelectedGameObject(null);
        inputfield.Select();
    }

    public void Activate() {
        inputfield.gameObject.SetActive(true);
    }

    public void Deactivate() {
        inputfield.gameObject.SetActive(false);
    }
}
