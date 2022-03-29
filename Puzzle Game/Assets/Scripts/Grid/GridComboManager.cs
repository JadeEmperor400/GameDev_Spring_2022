using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridComboManager : MonoBehaviour
{
    
    private Combo combo = new Combo(); //how to garantee one instance of combo?  


    public void AddToCombo(List<GameObject> connectionList) 
    {
       
        //make a connection object based on that list 
        var connection = ConvertToConnection(connectionList);

        //take that newly made connection and add it to the combo
        if(connection != null)
        combo.AddConnection(connection);
      
       

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

    public void ClearCombo()
    {
       
        combo.ClearCombo();
    }

    //GETTERS AND SETTERS
    public Queue<Connection> currentComboQueue()
    {
        return combo.getAllConnections();
    }
    
}
