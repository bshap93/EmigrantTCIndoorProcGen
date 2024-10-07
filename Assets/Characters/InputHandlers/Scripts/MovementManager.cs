using Characters.NPCs.Scripts.Commands.Move;
using Characters.Scripts.Commands.Move;
using UnityEngine;

namespace Characters.InputHandlers.Scripts
{
    public class MovementManager : MonoBehaviour
    {
        // Reference to the player object
        public GameObject player;
        // Reference to the main camera
        public Camera mainCamera;

        // Character controller replaces the rigidbody for movement
        public CharacterController controller;

        // Movement speeds for walking and running
        public float runSpeed = 2f;

        public float walkSpeed = 1f;
        // Movement commands for each direction
        MoveCommand _moveDownCommand;
        MoveCommand _moveLeftCommand;
        MoveCommand _moveRightCommand;
        MoveCommand _moveUpCommand;

        // Singleton instance
        public static MovementManager Instance { get; private set; }

        // Awake is called when the script instance is being loaded
        void Awake()
        {
            // Check if there is already an instance of this class
            if (Instance == null)
            {
                // If not, set the instance to this
                Instance = this;
                DontDestroyOnLoad(gameObject); // Keep this instance across scenes
            }
            else
            {
                // If instance already exists, destroy this
                Destroy(gameObject);
            }
        }

        void Start()
        {
            if (player == null)
                player = GameObject.FindWithTag("Player");

            if (mainCamera == null)
                mainCamera = Camera.main;

            if (controller == null)
                controller = player.GetComponent<CharacterController>();

            // Initialize movement commands with the camera's transform
            _moveUpCommand = new MoveForwardCommand(mainCamera.transform, controller, walkSpeed);
            _moveDownCommand = new MoveBackwardCommand(mainCamera.transform, controller, walkSpeed);
            _moveLeftCommand = new MoveLeftCommand(mainCamera.transform, controller, walkSpeed);
            _moveRightCommand = new MoveRightCommand(mainCamera.transform, controller, walkSpeed);
        }

        public void ExecuteMoveUpCommand()
        {
            _moveUpCommand.Execute(player);
        }

        public void ExecuteMoveDownCommand()
        {
            _moveDownCommand.Execute(player);
        }

        public void ExecuteMoveLeftCommand()
        {
            _moveLeftCommand.Execute(player);
        }

        public void ExecuteMoveRightCommand()
        {
            _moveRightCommand.Execute(player);
        }
    }
}
