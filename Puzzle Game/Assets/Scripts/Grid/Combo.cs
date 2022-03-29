using System.Collections.Generic;

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
       //just make a new empty queue ? 
       connections = new Queue<Connection>();

    }

    
}
