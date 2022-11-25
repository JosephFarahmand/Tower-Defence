using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public EnviromentController EnviromentController { get; private set; }
    public BuildController BuildController { get; private set; }
    public GameData GameData { get; private set; }

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
        EnviromentController ??= FindObjectOfType<EnviromentController>();
        BuildController ??= FindObjectOfType<BuildController>();
        GameData??= FindObjectOfType<GameData>();
    }
}