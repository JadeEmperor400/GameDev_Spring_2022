using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class subtleSelectButton : MonoBehaviour
{

    public Button button;
    // Start is called before the first frame update
    void Start()
    {
        DestinationButton(button);
    }

    public void DestinationButton(Button button)
    {
        button.Select();
    }


}
