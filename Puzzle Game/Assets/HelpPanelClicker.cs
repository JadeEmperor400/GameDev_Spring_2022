using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpPanelClicker : MonoBehaviour
{

    public UltimateJoystick joystick;
    public GameObject rulesPanel;
    public BattleManager battleManager;
   
    // Start is called before the first frame update
    void Start()
    {
        rulesPanel.SetActive(false);

        if (joystick == null)
            joystick = FindObjectOfType<UltimateJoystick>();

        if(battleManager == null)
            battleManager = FindObjectOfType<BattleManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && rulesPanel.activeSelf)
            TurnRulePanelOff();
        
    }

   public void TurnRulePanelOff()
    {
        if (battleManager.state != State.Off)
        {

        }
        else
        joystick.gameObject.SetActive(true);

        rulesPanel.SetActive(false);
    }

   public void TurnRulePanelOn()
    {

       
            joystick.gameObject.SetActive(false);


        rulesPanel.SetActive(true);
    }
}
