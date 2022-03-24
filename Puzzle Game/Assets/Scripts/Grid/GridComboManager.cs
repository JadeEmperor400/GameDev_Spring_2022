using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridComboManager : MonoBehaviour
{
    //debug variables 
    public bool DEBUG_FLAG = true;
    public Text currentComboText;
    [SerializeField]
    private Text connectionText;

    private Connection lastConnectionMade = null;//for debug

    

    private Combo combo = new Combo(); //how to garantee one instance of combo?  
    [SerializeField]
    private float fallTimer = 0.0f;
    [SerializeField]
    private float fallTimeMax = 6.0f;
    private bool countFall = false;

    private void Update()
    {
        if (getLastConnectionMade() != null && DEBUG_FLAG)
        {
            connectionText.text = "LAST CONNECTION MADE:" + getLastConnectionMade().getColorType() + " - " + getLastConnectionMade().getLengthOfConnection();
        }

        if (countFall) {
            fallTimer += Time.deltaTime;

            if (fallTimer >= fallTimeMax) {
                //Call GridManager to Activate Fall Animation
                countFall = false;
                GridManager gridManager = GameObject.FindObjectOfType<GridManager>();
                fallTimer = 0;
                gridManager.PlayFallAnimation();
            }
        }
    }

    public void AddToCombo(List<GameObject> connectionList) 
    {
       
        //make a connection object based on that list 
        var connection = ConvertToConnection(connectionList);

        //take that newly made connection and add it to the combo
        if(connection != null)
        combo.AddConnection(connection);
      
        if(DEBUG_FLAG)
        {           
            currentComboText.text += "\n Color: " + connection.getColorType() + " " + connection.getLengthOfConnection(); 
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
        setLastConnectionMade(newConnection);//for debug 
        return newConnection;
    }

    public void ClearCombo()
    {
        if (DEBUG_FLAG)
        {
            currentComboText.text = "";          
        }
        combo.ClearCombo();
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
