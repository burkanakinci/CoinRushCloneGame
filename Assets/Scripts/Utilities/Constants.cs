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

}

public enum ObjectsLayer
{
    CoinCollectable = 6,
    CoinCollected = 7,
}
public enum UIPanelType
{
    MainMenuPanel = 0,
    HudPanel = 1,
    FinishPanel = 2
}
