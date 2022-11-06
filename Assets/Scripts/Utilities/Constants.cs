public class Constants
{
    public const string PLAYER_DATA = "PlayerData";
}

public class PooledObjectTags
{
}
public class ObjectTags
{
    public const string CollectedCoin = "CollectedCoin";
    public const string FallTrigger = "FallTrigger";
    public const string HillTrigger = "HillTrigger";
    public const string Obstacle = "Obstacle";
}
public enum ObjectsLayer
{
    CoinCollectable = 6,
    CoinCollected = 7,
    FallCollider = 8,
    HillTrigger = 9,
    Obstacle = 10,
}

public enum ListOperation
{
    Adding,
    Subtraction
}

public enum CollectableCoinStates
{
    IdleCoinState = 0,
    ParticipationPlayerCoinState = 1,
    RunCoinState = 2,

}
public enum MainCoinStates
{
    IdleCoinState = 0,
    RunCoinState = 1,
    FallMainCoinState = 2,

}
public enum UIPanelType
{
    MainMenuPanel = 0,
    HudPanel = 1,
    FinishPanel = 2
}
