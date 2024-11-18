using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyController : MonoBehaviour
{
    public Button playButton;
    public GameObject LevelSelection;

    private void Awake()
    {
        playButton.onClick.AddListener(StartGame);
    }

    public void StartGame()
    {
        SoundManager.Instance.Play(Sounds.ButtonClicks);
        LevelSelection.SetActive(true);
    }
}
