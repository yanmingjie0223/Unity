using UnityEngine;

public class KeyboardManager : MonoBehaviour
{

    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            EventManager.GetInstance().Dispatch(GameEvent.GAME_UP_DOWN);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            EventManager.GetInstance().Dispatch(GameEvent.GAME_DOWN_DOWN);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            EventManager.GetInstance().Dispatch(GameEvent.GAME_LEFT_DOWN);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            EventManager.GetInstance().Dispatch(GameEvent.GAME_RIGHT_DOWN);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            EventManager.GetInstance().Dispatch(GameEvent.GAME_UP_DOWN);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            EventManager.GetInstance().Dispatch(GameEvent.GAME_DOWN_DOWN);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            EventManager.GetInstance().Dispatch(GameEvent.GAME_LEFT_DOWN);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            EventManager.GetInstance().Dispatch(GameEvent.GAME_RIGHT_DOWN);
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            EventManager.GetInstance().Dispatch(GameEvent.GAME_UP_UP);
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            EventManager.GetInstance().Dispatch(GameEvent.GAME_DOWN_UP);
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            EventManager.GetInstance().Dispatch(GameEvent.GAME_LEFT_UP);
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            EventManager.GetInstance().Dispatch(GameEvent.GAME_RIGHT_UP);
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            EventManager.GetInstance().Dispatch(GameEvent.GAME_UP_UP);
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            EventManager.GetInstance().Dispatch(GameEvent.GAME_DOWN_UP);
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            EventManager.GetInstance().Dispatch(GameEvent.GAME_LEFT_UP);
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            EventManager.GetInstance().Dispatch(GameEvent.GAME_RIGHT_UP);
        }
    }

}
