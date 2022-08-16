using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class MenuScript : MonoBehaviour
{
    [Header("Buttons")]
    public List<Button> allMenuButtons;

    [Header("Selector")]
    public GameObject selector;

    public virtual void Activate()
    {
        gameObject.SetActive(true);
        GeneralManager.manager.selector = selector;
        GeneralManager.manager.SetUpMenu(allMenuButtons);
        GeneralManager.gameState = GameState.inMenu;
    }
}
