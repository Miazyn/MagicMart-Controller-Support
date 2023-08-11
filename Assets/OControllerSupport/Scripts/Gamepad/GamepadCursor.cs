using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.UI;
using UnityEngine.InputSystem.Users;

public class GamepadCursor : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;
    [SerializeField] private RectTransform cursorTransform;

    [SerializeField] private float cursorSpeed = 1000f;
    [SerializeField] private RectTransform canvasRectTransform;
    [SerializeField] private Canvas canvas;
    [SerializeField] private float padding = 15f;

    private bool previousMouseState;

    private Mouse virtualMouse;
    private Camera mainCam;

    private Mouse currentMouse;

    private string previousControlScheme = "";

    private const string gamepadScheme = "Gamepad";
    //private const string mouseScheme = "KeyboardMouse";


    public static GamepadCursor instance;

    private void Awake()
    {
        //if (instance == null)
        //{
        //    instance = this;
        //    DontDestroyOnLoad(gameObject);
        //}
        //else
        //{
        //    AssignMissingValues(cursorTransform, canvasRectTransform, canvas, GetComponent<PlayerInput>().uiInputModule, GetComponent<PlayerInput>().camera);
            
        //}

    }
    private void Start()
    {
        OnControlsChanged(playerInput);

    }

    public void AssignMissingValues(RectTransform _cursorTransform, RectTransform _canvasRect, Canvas _canvas, InputSystemUIInputModule _uiInputModule, Camera _cam)
    {
        Debug.Log($"This is my instance here: {instance.name}");

        instance.cursorTransform = _cursorTransform;
        instance.canvasRectTransform = _canvasRect;
        instance.canvas = _canvas;

        PlayerInput playerInput = instance.GetComponent<PlayerInput>();
        playerInput.uiInputModule = _uiInputModule;

        playerInput.defaultControlScheme = gamepadScheme;

        playerInput.camera = _cam;

        
    }

    #region[Move Update Cursor]

    private void UpdateMotion()
    {
        if (virtualMouse == null || Gamepad.current == null)
        {
            return;
        }

        Vector2 deltaValue = Gamepad.current.leftStick.ReadValue();
        deltaValue *= cursorSpeed * Time.deltaTime;

        Vector2 currentPos = virtualMouse.position.ReadValue();
        Vector2 newPos = currentPos + deltaValue;

        newPos.x = Mathf.Clamp(newPos.x, padding, Screen.width - padding);
        newPos.y = Mathf.Clamp(newPos.y, padding, Screen.height - padding);

        InputState.Change(virtualMouse.position, newPos);
        InputState.Change(virtualMouse.delta, deltaValue);

        bool ButtonIsPressed = Gamepad.current.buttonSouth.IsPressed();

        if (previousMouseState != ButtonIsPressed)
        {
            virtualMouse.CopyState<MouseState>(out var mouseState);
            mouseState.WithButton(MouseButton.Left, ButtonIsPressed);
            InputState.Change(virtualMouse, mouseState);

            previousMouseState = ButtonIsPressed;
        }

        AnchorCursor(newPos);
    }

    private void AnchorCursor(Vector2 position)
    {
        Vector2 anchoredPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, position, canvas.renderMode
            == RenderMode.ScreenSpaceOverlay ? null : mainCam, out anchoredPos);

        cursorTransform.anchoredPosition = anchoredPos;
    }

    private void OnControlsChanged(PlayerInput input)
    {
        previousControlScheme = gamepadScheme;

        Debug.Log($"Controls are: {previousControlScheme}");

        cursorTransform.gameObject.SetActive(true);
        Cursor.visible = false;

        //InputState.Change(virtualMouse.position, currentMouse.position.ReadValue());
        //AnchorCursor(currentMouse.position.ReadValue());
    }

#endregion

    #region[Enable Disable]
    private void OnEnable()
    {

        mainCam = Camera.main;
        currentMouse = Mouse.current;

        foreach (var device in InputSystem.devices)
        {
            if (device.name == "VirtualMouse")
            {
                virtualMouse = (Mouse)device;
                break;
            }
        }

        if (virtualMouse == null)
        {
            virtualMouse = (Mouse)InputSystem.AddDevice("VirtualMouse");

        }
        else if (!virtualMouse.added)
        {
            InputSystem.AddDevice(virtualMouse);
        }
        InputUser playerUser = playerInput.user;
        InputUser.PerformPairingWithDevice(virtualMouse, playerInput.user);

        if (cursorTransform != null)
        {
            Vector2 position = cursorTransform.anchoredPosition;
            InputState.Change(virtualMouse.position, position);
        }

        InputSystem.onAfterUpdate += UpdateMotion;
        playerInput.onControlsChanged += OnControlsChanged;
    }
    private void OnDisable()
    {
        playerInput.onControlsChanged -= OnControlsChanged;

        if (virtualMouse != null && virtualMouse.added)
        {
            playerInput.user.UnpairDevice(virtualMouse);
            InputSystem.RemoveDevice(virtualMouse);
        }

        InputSystem.onAfterUpdate -= UpdateMotion;
    }
    #endregion
}
