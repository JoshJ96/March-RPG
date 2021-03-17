using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    //Constructor
    public GameData(string fileName)
    {
        level = 0;
        currency = 0;
        this.fileName = fileName;
        inventoryMaxSize = 15;
        inventory = new int[inventoryMaxSize];
        inventory[0] = 3;
        inventory[1] = 33;
        inventory[2] = 323;
        inventory[3] = 399;
        inventory[4] = 32;
        playerPosition[0] = 0;
        playerPosition[1] = 0;
        playerPosition[2] = 0;
    }

    //Data
    public int level;
    public int currency;
    public int inventoryMaxSize;
    public int[] inventory;
    public string fileName;

    public float[] playerPosition = new float[3];



    public Dictionary<int, int> items = new Dictionary<int, int> {
        {1,33},
        {2,31}
    };
}