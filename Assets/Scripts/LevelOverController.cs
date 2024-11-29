using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelOverController : MonoBehaviour
{
    public GameObject levelCompletePanel;
    public Button lobbyButton;

    private void Awake()
    {
        lobbyButton.onClick.AddListener(Lobby);
    }

    private void Lobby()
    {
        SceneManager.LoadScene("Lobby");
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            Debug.Log("Level is Complete!");
            LevelManager.Instance.MakerCurrentLevelComplete();
            levelCompletePanel.SetActive(true);
        }
    }

}
