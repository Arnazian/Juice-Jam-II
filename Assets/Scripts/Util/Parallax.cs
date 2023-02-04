using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float parallaxSpeed = 0.5f;
    public Transform background;
    private float parallaxScale;
    public float smoothing = 1f;

    private Transform cam;
    private Vector3 previousCamPos;

    void Awake()
    {
        cam = Camera.main.transform;
    }

    void Start()
    {
        previousCamPos = cam.position;
        parallaxScale = background.position.z * -1;
    }

    void Update()
    {
        float parallax = (previousCamPos.x - cam.position.x) * parallaxSpeed;
        float backgroundTargetPosX = background.position.x + parallax;
        Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, background.position.y, background.position.z);
        background.position = Vector3.Lerp(background.position, backgroundTargetPos, smoothing * Time.deltaTime);

        previousCamPos = cam.position;
    }
}
