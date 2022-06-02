using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConnectionUIManager : MonoBehaviour
{
    [SerializeField]
    private Button ServerButton;
    [SerializeField]
    private Button HostButton;
    [SerializeField]
    private Button ClientButton;
    [SerializeField]
    private TMP_InputField joinCodeInput;

    private void Start()
    {
        ServerButton?.onClick.AddListener(() =>
        {
            if (NetworkManager.Singleton.StartClient())
            {
                Debug.Log("Server has started");
                //SceneManager.LoadScene("LobbyScene");
            }
        });

        HostButton?.onClick.AddListener(async() =>
        {
            if (RelayManager.Instance.IsRelayEnabled)
            {
                Debug.Log("SetupRelay has been called");
                await RelayManager.Instance.SetupRelay();
            }

            if (NetworkManager.Singleton.StartHost()) { 
                Debug.Log("Host has started");
                //SceneManager.LoadScene("LobbyScene");
            }

        });

        ClientButton?.onClick.AddListener(async() =>
        {
            if (RelayManager.Instance.IsRelayEnabled && !string.IsNullOrEmpty(joinCodeInput.text))
                await RelayManager.Instance.JoinRelay(joinCodeInput.text);

            if (NetworkManager.Singleton.StartClient())
            {
                Debug.Log("Client has started");
                //SceneManager.LoadScene("LobbyScene");
            }
        });
    }
}
