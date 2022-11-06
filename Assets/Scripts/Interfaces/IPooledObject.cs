
public interface IPooledObject
{
    void OnObjectSpawn();
    void OnObjectDeactive();
    CustomBehaviour GetGameObject();
}