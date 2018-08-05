using UnityEngine;
using System.Collections;
using Rewired;

[RequireComponent(typeof(CharacterController))]
public class MyCharacter : MonoBehaviour
{

    public int playerId; // The Rewired player id of this character

    public float moveSpeed = 3.0f;

    private Rewired.Player player; // The Rewired Player
    private CharacterController cc;
    private Vector3 moveVector;

    void Awake()
    {
        // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
        player = ReInput.players.GetPlayer(playerId);

        // Get the character controller
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        GetInput();
        ProcessInput();
    }

    private void GetInput()
    {
        // Get the input from the Rewired Player. All controllers that the Player owns will contribute, so it doesn't matter
        // whether the input is coming from a joystick, the keyboard, mouse, or a custom controller.

        moveVector.x = player.GetAxis("Horizontal"); // get input by name or action id
    }

    private void ProcessInput()
    {
        // Process movement
        if (moveVector.x != 0.0f)
        {
            cc.Move(moveVector * moveSpeed * Time.deltaTime);
        }
    }
}
