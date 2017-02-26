using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{

    public GameObject Panel;

    // Use this for initialization
    void Start()
    {
        Panel.SetActive(false);
        this.gameObject.GetComponent<Button>().onClick.AddListener(PanelOnOff);
    }

    // Update is called once per frame
 	public void PanelOnOff()
    {
		if (Panel.activeSelf)
		{
			Panel.SetActive(false);
			return;
		}
		Panel.SetActive(true);
    }
}
