using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTileContent : MonoBehaviour
{
    public GameTileContentFactory OriginFactory { get; set; }

    public GameTileContentType Type => _type;
    [SerializeField] private GameTileContentType _type;


    public void Recycle()
    {
        OriginFactory.Reclaim(this);
    }

    public virtual void GameUpdate()
    {

    }
}
