using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenuDisplayController : MonoBehaviour
{
    [SerializeField]
    private Text cashText;

    private void Update()
    {
        cashText.text = "cash: " + GameObject.Find("MenusController").GetComponent<UpgradeMenuController>().getCash().ToString();
    }
}
