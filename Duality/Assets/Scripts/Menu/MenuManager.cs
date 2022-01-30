using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject _start, _credits, _settings, _coverThingy;
    [SerializeField] Camera _mainCamera;
    float _coverThingyScale = 7;


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
    }

    public void Credits()
    {

    }

    public void ExitGame()
    {
        Debug.Log("Exit");
        Application.Quit();
    }

    void ChangeFromMain()
    {
        _coverThingy.SetActive(true);
        _coverThingy.GetComponent<Animator>().Play("coverBlack");
    }

    void ChangeToWhite()
    {

    }
}
