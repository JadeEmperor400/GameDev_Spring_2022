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
