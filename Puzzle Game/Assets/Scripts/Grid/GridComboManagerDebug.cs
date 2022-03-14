using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridComboManagerDebug : MonoBehaviour
{
    [SerializeField]
    private Text connectionText;

    [SerializeField]
    private GridComboManager gridComboManager;

    private void Update()
    {
        if (gridComboManager.getLastConnectionMade() != null)
        {
            connectionText.text = "LAST CONNECTION MADE:" + gridComboManager.getLastConnectionMade().getColorType() + " - " + gridComboManager.getLastConnectionMade().getLengthOfConnection();
        }
    }

}
