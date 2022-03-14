using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connection
{
    ColorEnum colorType = ColorEnum.NONE; //define it as none to start? 
    private int lengthOfConnection = 0;

    public Connection(int lengthOfConnection, ColorEnum colorType)
    {
       this.lengthOfConnection = lengthOfConnection;
       this.colorType = colorType;
    }

    //GETTERS AND SETTERS 
    public ColorEnum getColorType()
    {
        return colorType;
    }
   public int getLengthOfConnection()
    {
        return lengthOfConnection;
    }
}


public class Combo
{
   private Queue<Connection> connections = new Queue<Connection>();
  


    
    public Queue<Connection> getAllConnections()
    {
        return connections;
    }

    //add connection to queue 
    public void AddConnection(Connection connection)
    {
        connections.Enqueue(connection); 
    }

    //clear combo method 
    public void ClearCombo()
    {

    }



}

public class GridComboManager : MonoBehaviour
{
    private Connection lastConnectionMade = null;//for debug

    [SerializeField]
    private GridManager gridManager; //is this needed? 

    private Combo combo = new Combo(); //how to garantee one instance of combo?  

    public void AddToCombo(List<GameObject> connectionList) //bad name, change it later
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
        Debug.Log("This connection had a length of " + cnt + ". The color was of type " + firstTileColorType);
        Connection newConnection = new Connection(cnt, firstTileColorType);
        setLastConnectionMade(newConnection);//for debug 
        return newConnection;
    }

    //GETTERS AND SETTERS
    public Queue<Connection> currentComboQueue()
    {
        return combo.getAllConnections();
    }
    public void setLastConnectionMade(Connection connection) //for debug
    {
        lastConnectionMade = connection;
    }
    public Connection getLastConnectionMade()//for debug
    {
        return lastConnectionMade;
    }
}
