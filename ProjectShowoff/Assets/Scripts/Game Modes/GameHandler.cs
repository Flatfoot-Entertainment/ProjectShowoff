using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using Cinemachine;

//the base game class, don't even know what to put in here yet

public enum GameState
{
    PackageView,
    Paused,
    Upgrade,
    PlanetView
}

public enum RandomEvent
{
    LightsOff,
    Shake,
    ConveyorOverload,
    GravityRemove
}

public abstract class GameHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private int money;
    [SerializeField] private GameState gameState;

    [SerializeField] private GameObject worldLight, pointLight;

    [SerializeField] private GameObject[] conveyorStopButtons;

    [SerializeField] private GameObject[] leftConveyorBelts, rightConveyorBelts;

    [Header("Random event properties")]

    [SerializeField] private float minRandomShake, maxRandomShake;
    [SerializeField] private float lightsOffDuration, gravityRemoveDuration, conveyorOverloadDuration, conveyorSpeedUpValue;
    [SerializeField] private int randomEventOccurance;
    [SerializeField] private SpawnerController spawnerController;

    [SerializeField] private Transform itemsSpawned;

    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    [SerializeField] private string moneyTextFormat = "¤{0}";

    public static GameHandler Instance;

    public int Money
    {
        get => money;
        set => money = value;
    }
    //TODO use event queue to decouple game modes from game events

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        EventScript.Handler.Subscribe(EventType.ManageMoney, ManageMoney);
        EventScript.Handler.Subscribe(EventType.ManageUpgrade, OnUpgradeBought);
        EventScript.Handler.Subscribe(EventType.ConveyorUpgrade, UpgradeConveyorBelt);
        moneyText.text = string.Format(moneyTextFormat, money.ToString());
        itemsSpawned = GameObject.Find("Items Spawned").transform;
        spawnerController = GetComponent<SpawnerController>();
        StartCoroutine(DoRandomEvent());
    }

    private void OnDestroy()
    {
        OnDestroyCallback();
    }

    private void ManageMoney(Event e)
    {
        if (!(e is ManageMoneyEvent moneyEvent)) return;
        money += moneyEvent.Amount;
        moneyText.text = string.Format(moneyTextFormat, money.ToString());
    }

    private void ManageUpgrade(Event e)
    {
        if (!(e is ManageUpgradeEvent upgradeEvent)) return;
        money -= upgradeEvent.Upgrade.Cost;
        moneyText.text = string.Format(moneyTextFormat, money.ToString());
    }

    protected virtual void OnDestroyCallback()
    {
        EventScript.Handler.Unsubscribe(EventType.ManageMoney, OnBoxSent);
        EventScript.Handler.Unsubscribe(EventType.ManageMoney, OnBoxDelivered);
        EventScript.Handler.Unsubscribe(EventType.ManageUpgrade, OnUpgradeBought);
        EventScript.Handler.Unsubscribe(EventType.ConveyorUpgrade, UpgradeConveyorBelt);
    }

    protected virtual void OnBoxDelivered(Event e)
    {
        ManageMoney(e);
    }

    protected virtual void OnBoxSent(Event e)
    {
        ManageMoney(e);
    }

    protected virtual void OnUpgradeBought(Event e)
    {
        ManageUpgrade(e);
    }


    //pls refactor into smth else
    public void EnableConveyorButton()
    {
        foreach (GameObject conveyorButton in conveyorStopButtons)
        {
            conveyorButton.SetActive(true);
        }
    }
    public void UpgradeConveyorBelt(Event e)
    {
        if (!(e is ConveyorUpgradeEvent upgrade)) return;
        int level = upgrade.Level;
        //TODO into an event, this is horrible
        SpawnerController spawnerController = GetComponent<SpawnerController>();
        if (spawnerController)
        {
            switch (level)
            {
                case 1:
                    rightConveyorBelts[0].SetActive(true);
                    spawnerController.AddSpawner(rightConveyorBelts[0].GetComponentInChildren<ItemSpawner>());
                    spawnerController.AddConveyor(rightConveyorBelts[0].GetComponentInChildren<ConveyorSetupScript>());
                    break;
                case 2:
                    spawnerController.RemoveConveyorAt(0);
                    spawnerController.RemoveSpawnerAt(0);
                    leftConveyorBelts[0].SetActive(false);

                    leftConveyorBelts[1].SetActive(true);
                    spawnerController.AddSpawners(leftConveyorBelts[1].GetComponentsInChildren<ItemSpawner>());
                    spawnerController.AddConveyor(leftConveyorBelts[1].GetComponentInChildren<ConveyorSetupScript>());
                    break;
                case 3:
                    spawnerController.RemoveConveyorAt(0);
                    spawnerController.RemoveSpawnerAt(0);
                    rightConveyorBelts[0].SetActive(false);

                    rightConveyorBelts[1].SetActive(true);
                    spawnerController.AddSpawners(rightConveyorBelts[1].GetComponentsInChildren<ItemSpawner>());
                    spawnerController.AddConveyor(rightConveyorBelts[1].GetComponentInChildren<ConveyorSetupScript>());
                    break;
                default:
                    Debug.Log("dis level not exist");
                    break;
            }
        }
        else Debug.LogError("SpawnerController not found when upgrading conveyor belts");
    }

    //TODO turn the random events into classes
    private IEnumerator DoRandomEvent()
    {
        yield return new WaitForSeconds(randomEventOccurance);
        RandomEvent randomEvent = Extensions.RandomEnumValue<RandomEvent>();
        Debug.Log("random event runs now: " + randomEvent);
        switch (randomEvent)
        {
            case RandomEvent.LightsOff:
                DoLightsOff(lightsOffDuration);
                break;
            case RandomEvent.Shake:
                DoShake();
                break;
            case RandomEvent.GravityRemove:
                DoGravityRemove(gravityRemoveDuration);
                break;
            case RandomEvent.ConveyorOverload:
                //todo for some reason it doesn't affect the upgraded conveyors
                DoConveyorOverload(conveyorOverloadDuration);
                break;
        }
    }

    private void DoLightsOff(float duration)
    {
        StartCoroutine(LightsOff(duration));
    }

    private IEnumerator DoShake()
    {
        foreach (Transform item in itemsSpawned)
        {
            Rigidbody itemRb = item.GetComponent<Rigidbody>();
            if (itemRb != null)
            {
                ApplyRandomForce(itemRb);
            }
        }
        //todo remove magic values, maybe handle in CameraTranslate
        virtualCamera.transform.DOShakePosition(0.5f, 100, 10, 180, false, true);
        yield return new WaitForSeconds(0.1f);
        yield return DoRandomEvent();
    }


    private void DoGravityRemove(float duration)
    {
        StartCoroutine(GravityRemove(duration));
    }

    private void DoConveyorOverload(float duration)
    {
        StartCoroutine(ConveyorOverload(duration));
    }

    private IEnumerator LightsOff(float duration)
    {
        worldLight.SetActive(false);
        pointLight.SetActive(true);
        yield return new WaitForSeconds(duration);
        worldLight.SetActive(true);
        pointLight.SetActive(false);
        yield return DoRandomEvent();
    }

    private IEnumerator GravityRemove(float duration)
    {
        Physics.gravity = -Vector3.up;
        yield return new WaitForSeconds(duration);
        Physics.gravity = new Vector3(0f, -9.81f, 0f);
        yield return DoRandomEvent();
    }

    private IEnumerator ConveyorOverload(float duration)
    {
        spawnerController.ChangeConveyorSpeed((int)conveyorSpeedUpValue);
        yield return new WaitForSeconds(duration);
        spawnerController.ChangeConveyorSpeed(spawnerController.ConveyorInitialSpeed);
        yield return DoRandomEvent();
    }

    private void ApplyRandomForce(Rigidbody rb)
    {
        Vector3 rand = Random.onUnitSphere * Random.Range(minRandomShake, maxRandomShake);
        rb.AddForce(rand, ForceMode.Impulse);
    }

    //TODO make into an event
    public void OnAddShip()
    {
        // fulfillmentCenter.AddShip();
    }
}
