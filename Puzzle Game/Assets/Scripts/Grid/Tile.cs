using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    private ColorEnum colorIdentity;

    [SerializeField]
    private SpriteRenderer _renderer;

    [SerializeField]
    private GameObject lineObjectPrefab;


    public void Init() //for the init, perhaps change it to ethier, red green blue or empty 
    {
        int randNum = Random.Range(0, 5);

        switch(randNum)
        {
                case 0:
                colorIdentity = ColorEnum.RED;
                break; 

                case 1:
                colorIdentity = ColorEnum.BLUE;
                break;

                case 2:
                colorIdentity = ColorEnum.GREEN;
                break; 

                case 3:
                colorIdentity = ColorEnum.NONE;
                break;

                case 4:
                colorIdentity = ColorEnum.NONE;
                break;

                default:
                Debug.Log("Default case reached in Tile class");

                colorIdentity = ColorEnum.NONE;
                break;
    

        }
        setColor();
    }

    private void setColor()
    {
        switch (colorIdentity)
        {
            case ColorEnum.RED:
                _renderer.color = Color.red;
                createLineObject();
                break;

            case ColorEnum.BLUE:
                _renderer.color = Color.blue;
                createLineObject();
                break;
            case ColorEnum.GREEN:
                _renderer.color = Color.green;
                createLineObject();
                break;
            case ColorEnum.NONE:
                _renderer.color = Color.black;
                break;

            default:
                Debug.Log("Default case reached in Tile class, SetColor()");
               
                break;
        }
    }

    private void createLineObject()
    {
        var spawnedLineObject = Instantiate(lineObjectPrefab, transform.position, Quaternion.identity);
        spawnedLineObject.name = name + " lineObject";
        spawnedLineObject.transform.parent = transform;

        var linerenderer = GetComponent<LineRenderer>();
        if (linerenderer != null)
        {
            Debug.Log("line renderer is created, changing the color ");
            linerenderer.startColor = Color.yellow;
            linerenderer.endColor = Color.yellow;
        }
    }
    
  

}