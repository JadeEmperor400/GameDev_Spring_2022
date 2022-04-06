using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpPanelClicker : MonoBehaviour
{


    public GameObject rulesPanel;
   
    // Start is called before the first frame update
    void Start()
    {
        rulesPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            TurnRulePanelOff();
        
    }

   public void TurnRulePanelOff()
    {
        rulesPanel.SetActive(false);
    }

   public void TurnRulePanelOn()
    {
        rulesPanel.SetActive(true);
    }
}
