public class Cell
{
    public bool isWater;
    public bool isTree;
    public bool isEdge;
    public Cell(bool isWater, bool isTree, bool isEdge)
    {
        this.isWater = isWater;
        this.isTree = isTree;
        this.isEdge = isEdge;
    }

}
