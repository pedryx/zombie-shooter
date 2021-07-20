using System;

using UnityEngine;


public class GameOver : MonoBehaviour
{

    public Hitable Hitable;
    public GameObject endGamePanel;

    private void Start()
    {
        Hitable.OnDead += Hitable_OnDead;
    }

    private void Hitable_OnDead(object sender, EventArgs e)
    {
        endGamePanel.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }

}
