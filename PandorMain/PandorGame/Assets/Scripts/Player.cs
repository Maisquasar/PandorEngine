using System;
using System.Collections.Generic;
using Pandor;

public class Player : ScriptComponent
{
    public GameObject camera;
    public ItemAnimation itemSlot;
    private Rigidbody rb;

    public float sensitivity = 0.1f;
    public float maxVelocity = 10;
    public float accelerationTime = 0.2f;
    public float decelerationTime = 0.1f;
    private readonly float velocityToStop = 0.01f;

    Vector2 defaultCursorPos;
    float angle;
    float pitch;

    bool playing = false;
    bool introGotPlayed = false;
    public bool hasAxe = false;
    public bool hasRadio = false;
    public bool hasSkull = false;

    public GameObject pauseMenu;
    public GameObject inGameUI;

    void OnCreate()
    {
        rb = gameObject.GetComponent<Rigidbody>();

        angle = -transform.eulerAngles.y;
        pitch = camera.transform.localEulerAngles.x;
        pauseMenu.active = false;
        inGameUI.active = true;
    }

    void OnUpdate(float dt)
    {
        if (playing)
        {
            if (Input.IsKeyPressed(Key.Key_Escape))
            {
                playing = false;
                Input.EnableCursor();
                return;
            }
            if (Input.IsKeyPressed(Key.Key_P))
            {
                TogglePause();
            }

            if (Application.timeScale <= 0)
                return;
            UpdateRotation(dt);
            UpdateMovements(dt);
        }
        else if (Input.IsKeyPressed(Key.Key_MouseMiddle))
        {
            Input.DisableCursor();
            Input.FixCursor();
            defaultCursorPos = Input.GetMousePos();
            playing = true;
        }
    }

    void UpdateRotation(float dt)
    {
        Input.DisableCursor();
        Vector2 mousePos = Input.GetMousePos();

        if (defaultCursorPos == mousePos) return;

        float offsetX = defaultCursorPos.x - mousePos.x;
        float offsetY = mousePos.y - defaultCursorPos.y;

        Input.FixCursor();
        defaultCursorPos = Input.GetMousePos();

        offsetX *= sensitivity;
        offsetY *= sensitivity;

        angle -= offsetX;
        pitch += offsetY;

        if (pitch > 89)
            pitch = 89;

        if (pitch < -89)
            pitch = -89;

        camera.transform.localRotation = Quaternion.Euler(pitch, 0, 0);
        transform.rotation = Quaternion.Euler(0, -angle, 0);
    }

    void UpdateMovements(float dt)
    {
        Vector3 direction = Vector3.zero;

        if (Input.IsKeyDown(Key.Key_W) || Input.IsKeyDown(Key.Key_Z))
        {
            direction += transform.forward;
        }
        if (Input.IsKeyDown(Key.Key_S))
        {
            direction -= transform.forward;
        }
        if (Input.IsKeyDown(Key.Key_A) || Input.IsKeyDown(Key.Key_Q))
        {
            direction += transform.right;
        }
        if (Input.IsKeyDown(Key.Key_D))
        {
            direction -= transform.right;
        }

        Vector3 currentVelocity = rb.velocity;
        currentVelocity.y = 0;

        if (direction != Vector3.zero)
        {
            if(!introGotPlayed)
            {
                AudioManager.PlaySoundByName("Sounds/Intro.mp3");
                introGotPlayed = true;
            }
            // Acceleration
            Vector3 targetVelocity = direction * maxVelocity;
            Vector3 acceleration = (targetVelocity - currentVelocity) / accelerationTime;
            currentVelocity += acceleration * dt;

            // Limit velocity to maxVelocity
            if (currentVelocity.Length() > maxVelocity)
                currentVelocity = currentVelocity.GetNormalized() * maxVelocity;

            itemSlot.playerMoving = true;
            rb.velocity = new Vector3(currentVelocity.x, rb.velocity.y, currentVelocity.z);
        }
        else
        {
            // Deceleration
            Vector3 deceleration = currentVelocity / -decelerationTime;
            currentVelocity += deceleration * dt;

            // Stop movement if velocity becomes too small
            if (currentVelocity.Length() < velocityToStop)
                currentVelocity = Vector3.zero;

            itemSlot.playerMoving = false;
            rb.velocity = new Vector3(currentVelocity.x, rb.velocity.y, currentVelocity.z);
        }
    }

    public void PickupAxe(ref AxeSlice axe)
    {
        if (!hasAxe)
        {
            if (axe != null)
            {
                hasAxe = true;
                axe.gameObject.SetParent(camera.GetChild(0));
                Transform axeTransform = axe.transform;
                axeTransform.localPosition = new Vector3(0.8f, -0.81f, 1.63f);
                axeTransform.localRotation = Quaternion.Euler(-90, 0, 0);
                axeTransform.localScale = Vector3.one * 3f;
                axe.gameObject.transform = axeTransform;
                axe.SetRot();
                axe.enable = true;
            }
        }
    }

    private void TogglePause()
    {
        Application.timeScale = Application.timeScale == 0 ? 1f : 0f;
        pauseMenu.active = Application.timeScale == 0;
        inGameUI.active = Application.timeScale != 0;
        defaultCursorPos = Input.GetMousePos();
    }

    public void ContinueButton()
    {
        TogglePause();
    }

    public void QuitButton()
    {
        TogglePause();
        SceneManager.LoadScene("MainMenu");
    }

    public GameObject TakeCar(ref GameObject car)
    {
        if (car != null)
        {
            Vector3 camPos = camera.transform.position;
            Quaternion camRot = camera.transform.rotation;
            camera.ClearParent();
            camera.transform.position = camPos;
            camera.transform.rotation = camRot;
            List<GameObject> children = camera.GetChildren();
            foreach (GameObject child in children)
            {
                child.Destroy();
            }
            rb.velocity = new Vector3(99999999, 0, 0);
            enable = false;
            return camera;
        }
        return null;
    }
    public void PickupRadio(ref Radio radio)
    {
        hasRadio = true;
        radio.gameObject.active = false;
    }

    public void PickupSkull(ref Skull skull)
    {
        hasSkull = true;
        skull.gameObject.active = false;
    }
}

