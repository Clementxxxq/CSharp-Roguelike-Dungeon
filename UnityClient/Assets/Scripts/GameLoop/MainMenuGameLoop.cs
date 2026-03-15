using UnityEngine;
using MainMenuScene;

public class MainMenuGameLoop : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Facade facade;
    void Start()
    {
        facade = new Facade();

    }

    // Update is called once per frame
    void Update()
    {
        facade.GameUpdate();
    }
}
