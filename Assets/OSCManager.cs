using UnityEngine;
using OSCQuery.UnityOSC;

public class OSCManager : MonoBehaviour
{
    public Player2DMovement player;
    public int port = 7000;
    public float inputTimeout = 0.1f; // how long before input is considered stopped

    private OSCReceiver receiver;
    private float lastMoveXTime = 0f;

    void Start()
    {
        receiver = new OSCReceiver();
        receiver.Open(port);
        Debug.Log("OSC listening on port " + port);
    }

    void Update()
    {
        // If no /moveX received recently, stop the character
        if (Time.time - lastMoveXTime > inputTimeout)
        {
            player.SetOSCInput(0f);
        }

        while (receiver.hasWaitingMessages())
        {
            OSCMessage message = receiver.getNextMessage();
            Debug.Log("OSC received: " + message.Address + " | " + message.Data[0]);

            if (message.Address == "/moveX")
            {
                float value = (float)message.Data[0];
                lastMoveXTime = Time.time; // update last received time

                if (value > 0)
                {
                    player.SetOSCInput(1f);
                    player.FlipCharacter(-1f); 
                }
                else if (value < 0)
                {
                    player.SetOSCInput(-1f);
                    player.FlipCharacter(1f); 
                }
                else
                {
                    player.SetOSCInput(0f);
                }
            }

            if (message.Address == "/moveY")
            {
                float value = (float)message.Data[1]; 
                Debug.Log("moveY value: " + value);

                if (value > 0)
                    player.TriggerJump();
            }
        }
    }

    void OnDestroy()
    {
        receiver.Close();
    }
}