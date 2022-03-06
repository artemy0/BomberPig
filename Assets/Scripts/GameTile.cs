using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTile : MonoBehaviour
{
    private GameTileContent _content;

    public GameTileContent Content
    {
        get
        {
            return _content;
        }
        set
        {
            _content = value;
            _content.transform.localPosition = transform.localPosition;
        }
    }
}
