using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CoffeeTypeClientList
{
    public CoffeeType typeOfCoffee;
    public List<Sprite> clientSprites;
}

[RequireComponent(typeof(SpriteRenderer))]
public class SugarClient : MonoBehaviour
{
    //Gets updated with new coffee
    //The pipeline is : select new coffee then select new client from among the ones that can order this coffee

    [SerializeField]
    CoffeeTypeClientList[] coffeeClients; //Set this up in prefab and never change afterwards

    private Dictionary<CoffeeType, List<Sprite>> clientsByCoffeeType = new Dictionary<CoffeeType, List<Sprite>>();

    private SpriteRenderer render;
    private void Start()
    {
        render = GetComponent<SpriteRenderer>();

        foreach(CoffeeTypeClientList cf in coffeeClients)
        {
            clientsByCoffeeType.Add(cf.typeOfCoffee, cf.clientSprites);
        }
    }

    public void RefreshClient(CoffeeType type)
    {
        int i = Random.Range(0, clientsByCoffeeType[type].Count - 1);

        render.sprite = clientsByCoffeeType[type][i];
    }
}
