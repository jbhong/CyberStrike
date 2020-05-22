using UnityEngine;

public class LaptopData : MonoBehaviour
{
    private string laptopData;

    public void SetLaptopData(string _laptopData)
    {
        laptopData = _laptopData;
    }

    public string GetLaptopData()
    {
        return laptopData;
    }

}
