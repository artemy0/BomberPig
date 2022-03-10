using UnityEngine;

[CreateAssetMenu(fileName = "Factory/EntityFactory", menuName = "EntityFactory")]
public class EntityFactory : ScriptableObject
{
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private Enemy _defaultEnemy;
    [SerializeField] private Enemy _specialEnemy;


    public Entity Get(EntityType entityType)
    {
        switch (entityType)
        {
            case EntityType.Player:
                return Get(_playerPrefab);
            case EntityType.DefaultEnemy:
                return Get(_defaultEnemy);
            case EntityType.SpecialEnemy:
                return Get(_specialEnemy);
        }

        return null;
    }

    private T Get<T>(T prefab) where T : Entity
    {
        T instance = Instantiate(prefab);
        instance.OriginFactory = this;
        return instance;
    }


    public void Reclaim(Entity entity)
    {
        Destroy(entity.gameObject);
    }
}