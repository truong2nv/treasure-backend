namespace OnePiece.WebAPI.Models;

public class HuntRequest
{
    public int N { set; get; }
    public int M { set; get; }
    public int P { set; get; }
    public List<List<int>> Matrix { set; get; }
}