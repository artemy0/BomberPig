using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Factory/GameTileContentFactory", menuName = "GameTileContentFactory")]
public class GameTileContentFactory : ScriptableObject
{
    [SerializeField] private GameTileContent _emptyPrefab;
    [SerializeField] private GameTileContent _stonePrefab;
    [SerializeField] private GameTileContent _bombPrefab;


    public GameTileContent Get(GameTileContentType contentType)
    {
        switch (contentType)
        {
            case GameTileContentType.Empty:
                return Get(_emptyPrefab);
            case GameTileContentType.Stone:
                return Get(_stonePrefab);
            case GameTileContentType.Bomb:
                return Get(_bombPrefab);
        }

        return null;
    }

    private T Get<T>(T prefab) where T : GameTileContent
    {
        T instance = Instantiate(prefab);
        instance.OriginFactory = this;
        return instance;
    }


    public void Reclaim(GameTileContent content)
    {
        Destroy(content.gameObject);
    }
}
