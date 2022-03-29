using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridManagerDebug : MonoBehaviour
{

    public GridManager gridManager;

    [SerializeField]
    private Slider sliderGridOffset;
    [SerializeField]
    private Text sliderGridOffsetText;

    [SerializeField]
    private Slider sliderGridWidth;
    [SerializeField]
    private Text sliderGridWidthtText;

    [SerializeField]
    private Slider sliderGridHeight;
    [SerializeField]
    private Text sliderGridHeightText;


    // Start is called before the first frame update
    void Start()
    {
        sliderGridOffset.onValueChanged.AddListener((v) =>
        {
            gridManager.SetGridOffset(sliderGridOffset.value);
            sliderGridOffsetText.text = "Grid Offset " + v.ToString("0.00") ;
        });

        sliderGridWidth.onValueChanged.AddListener((v) =>
        {
            gridManager.SetGridWidth((int)sliderGridWidth.value);
            sliderGridWidthtText.text = "Grid WIDTH " + v.ToString("0");
        });

        sliderGridHeight.onValueChanged.AddListener((v) =>
        {
            gridManager.SetGridHeight((int)sliderGridHeight.value);
            sliderGridHeightText.text = "Grid Height " + v.ToString("0");
        });



    }

    // Update is called once per frame
    void Update()
    {





        if (Input.GetKeyDown(KeyCode.T))
        {
            gridManager.RegenerateGridColors(); //represents end turn? 
               
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            gridManager.RegenerateGrid(); //complete regeneration

        }
    }
}
