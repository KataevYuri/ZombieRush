using UnityEngine;
using UnityEngine.InputSystem; // Не забудь добавить!

public class CrosshairController : MonoBehaviour
{
    private Camera mainCamera;
    public InputActionAsset inputActions; // Перетащи сюда PlayerControls
    private InputAction aimAction;

    private void Start()
    {
        Cursor.visible = false;
     }

    private void Awake()
    {
        mainCamera = Camera.main;
        Cursor.visible = false;

        // Находим Action "Aim" в схеме "Player"
        var playerMap = inputActions.FindActionMap("Player");
        aimAction = playerMap.FindAction("Aim");

        // Включаем Action
        aimAction.Enable();
    }

    private void Update()
    {
        // Получаем позицию мыши в экранных координатах
        Vector2 mouseScreenPos = aimAction.ReadValue<Vector2>();
        
        // Конвертируем в мировые координаты
        Vector2 mouseWorldPos = mainCamera.ScreenToWorldPoint(new Vector2(mouseScreenPos.x, mouseScreenPos.y));
        //mouseWorldPos.z = 0; // Фиксируем Z для 2D

        transform.position = mouseWorldPos;
    }

    private void OnDestroy()
    {
        aimAction.Disable();
        Cursor.visible = true;
    }
}