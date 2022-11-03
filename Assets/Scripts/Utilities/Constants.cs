public class Constants
{
    public const string PLAYER_DATA = "PlayerData";
}

public class PooledObjectTags
{
}
public class ObjectTags
{
}

public enum ListOperation
{
    Adding,
    Subtraction
}

public enum CoinStates
{
    IdleCoinState = 0,
    ParticipationPlayerCoinState=1,
    RunCoinState = 2,

}

public enum ObjectsLayer
{
    Collactable = 8,
    Collacted = 9,
    Gun = 10
}
public enum UIPanelType
{
    MainMenuPanel = 0,
    HudPanel = 1,
    FinishPanel = 2
}
