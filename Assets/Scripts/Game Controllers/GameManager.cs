using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public EnviromentController Controller { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;

            LoadControllers();

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LoadControllers()
    {
        Controller ??= FindObjectOfType<EnviromentController>();
    }
}