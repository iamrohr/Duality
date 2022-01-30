using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject _start, _credits, _settings, _coverThingy;
    [SerializeField] Camera _mainCamera;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void StartGame()
    {

    }

    public void Setting()
    {
        ChangeFromMain();
        Invoke(nameof(ActivateSettings), 0.5f + 0.69f);
    }

    public void Credits()
    {
        ChangeFromMain();
        Invoke(nameof(ActivateCredits), 0.5f + 0.69f);
    }

    public void ExitGame()
    {
        Debug.Log("Exit");
        Application.Quit();
    }

    public void BackToMain()
    {
        ChangeFromMain();
        Invoke(nameof(ActiveStart), 0.5f + 0.69f);
    }

    void ChangeFromMain()
    {
        _coverThingy.SetActive(true);
        _coverThingy.GetComponent<Animator>().Play("coverBlack");
        Invoke(nameof(InvertColor), 0.5f);
        Invoke(nameof(DeactivateThingy), 0.5f + 0.69f);
    }

    void InvertColor()
    {
        GetComponent<InvertColors>().InvertColorsBW();
    }

    void DeactivateThingy()
    {
        _coverThingy.SetActive(false);
        _start.SetActive(false);
    }

    void ActivateSettings()
    {
        _settings.SetActive(true);
    }

    void ActivateCredits()
    {
        _credits.SetActive(true);
    }

    void ActiveStart()
    {
        _settings.SetActive(false);
        _credits.SetActive(false);
        _start.SetActive(true);
    }
}
