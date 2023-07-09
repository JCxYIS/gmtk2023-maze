using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wall : MonoBehaviour
{
    // private const float WALL_THICKNESS = 0.1f;

    WallState _state;

    public void Init(int i, int j, WallState state)
    {
        _state = state;
        transform.localPosition = new Vector3(i, 0, j);

        if (state.HasFlag(WallState.UP))
        {
            transform.Translate(0, 0, 0.5f);
            transform.eulerAngles = new Vector3(0, 90, 0);
        }
        else if (state.HasFlag(WallState.LEFT))
        {
            transform.Translate(-0.5f, 0, 0);
        }
        else if (state.HasFlag(WallState.RIGHT))
        {
            transform.Translate(0.5f, 0, 0);
        }
        else
        {
            transform.Translate(0, 0, -0.5f);
            transform.eulerAngles = new Vector3(0, 90, 0);
        }

        // transform.localScale = new Vector3(WALL_THICKNESS, transform.localScale.y, transform.localScale.z);
        name = $"Wall ({i}. {j}) {state}";
    }

    public void MarkDestroyed()
    {
        Destroy(gameObject);
    }
}