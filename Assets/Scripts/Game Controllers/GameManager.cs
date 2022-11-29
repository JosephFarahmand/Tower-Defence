using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float maxHealth =10;
    public float MaxHealth => maxHealth;

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
        TimeScaleHelper.ChangeTimeScale(TimeScaleHelper.TimeScale.x0);
        InitializationControllers();
    }

    private void LoadControllers()
    {
        Stats = new GameStats(MaxHealth);

        EnviromentController ??= FindObjectOfType<EnviromentController>();
        BuildController ??= FindObjectOfType<BuildController>();
        GameData ??= FindObjectOfType<GameData>();

        ProfileController = new ProfileController();

        EnemyController = new EnemyController();

        FindObjectOfType<UserInterfaceManager>().Initialization();
    }

    private void InitializationControllers()
    {
        EnviromentController.Initialization();
        ProfileController.Initialization();
    }

    public void ResetGame()
    {
        Stats.ChangeState(GameStats.State.Reset);   
    }
}