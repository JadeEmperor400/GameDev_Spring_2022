using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridComboManager : MonoBehaviour
{
    
    private Combo combo = new Combo(); //how to garantee one instance of combo?  
    [SerializeField]
    private float fallTimer = 0.0f;
    [SerializeField]
    private float fallTimeMax = 3.0f;
    private bool countFall = false;
    [SerializeField]
    private ComboIndicator comboIndicator;

    public void AddToCombo(List<GameObject> connectionList) 
    {
       
        //make a connection object based on that list 
        var connection = ConvertToConnection(connectionList);

        //take that newly made connection and add it to the combo
        if(connection != null)
        combo.AddConnection(connection);
        comboIndicator.Boost(currentComboQueue().Count);

        if (!countFall)
        {
            countFall = true;
            fallTimer = 0;
            if(currentComboQueue().Count == 1)
                comboIndicator.SetColor(connection);
        }
        else {

            fallTimer -= 0.5f;
            if (fallTimer < 0) {
                fallTimer = 0;
            }
        }
    }

    public void Update()
    {
        if (countFall) {
            fallTimer += Time.deltaTime;
            if (fallTimer >= fallTimeMax) {
                ResetCountFall();
                FindObjectOfType<GridManager>().PlayFallAnimation();
            }
        }
    }

    private Connection ConvertToConnection(List<GameObject> connectionList)
    {
        // (bad) assumption that the ends of the connection will be of the same color type 
       ColorEnum firstTileColorType;
       var firstTile = connectionList[0].gameObject.GetComponent<Tile>();

        if (firstTile != null)
            firstTileColorType = firstTile.GetTileColorIdentity();
        else
            return null;


        int cnt = 0;
        for (int i = 1; i < connectionList.Count; i++)
        {
            if (!GameObject.ReferenceEquals(connectionList[i], connectionList[i -1]))
            {             
                cnt++;
            }           
        }
       
        Connection newConnection = new Connection(cnt, firstTileColorType);
      
        return newConnection;
    }

    public void ResetCountFall()
    {
        countFall = false;
        fallTimer = 0;
    }

    public void ClearCombo()
    {
        combo.ClearCombo();
        comboIndicator.ClearComboIndicator();
    }

    //GETTERS AND SETTERS
    public Queue<Connection> currentComboQueue()
    {
        return combo.getAllConnections();
    }
    
}
