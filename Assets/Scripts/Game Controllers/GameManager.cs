using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float MaxHealth = 6;

    public static GameManager Instance { get; private set; }

    public EnviromentController EnviromentController { get; private set; }
    public BuildController BuildController { get; private set; }
    public GameData GameData { get; private set; }
    public ProfileController ProfileController { get; private set; }
    public EnemyController EnemyController { get; private set; }
    public GameStats Stats { get; private set; }

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

    private void Start()
    {
        InitializationControllers();
    }

    private void LoadControllers()
    {
        EnviromentController ??= FindObjectOfType<EnviromentController>();
        BuildController ??= FindObjectOfType<BuildController>();
        GameData ??= FindObjectOfType<GameData>();

        ProfileController = new ProfileController();

        Stats = new GameStats(MaxHealth);
        EnemyController = new EnemyController();

        FindObjectOfType<UserInterfaceManager>().Initialization();
    }

    private void InitializationControllers()
    {
        ProfileController.Initialization();
    }
}