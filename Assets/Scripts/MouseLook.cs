using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public enum RotationAxes
    {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2
    }

    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityHorizontal = 9.0f;
    public float sensitivityVertical = 9.0f;

    public float minimumVertical = -45.0f;
    public float maximumVertical = 45.0f;

    private float _rotationX = 0;

    void Start()
    {
        Rigidbody body = GetComponent<Rigidbody>();
        if (body != null)
            body.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (axes == RotationAxes.MouseX)
        {
            // это поворот в горизонтальной плоскости
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHorizontal, 0);
        }
        else if (axes == RotationAxes.MouseY)
        {
            // это поворот в вертикальной плоскости
            _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVertical;

            _rotationX = Mathf.Clamp(_rotationX, minimumVertical, maximumVertical);

            var rotationY = transform.localEulerAngles.y;

            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        }
        else
        {
            // это комбинированный поворот
            _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVertical;
            _rotationX = Mathf.Clamp(_rotationX, minimumVertical, maximumVertical);
            var delta = Input.GetAxis("Mouse X") * sensitivityHorizontal;
            var rotationY = transform.localEulerAngles.y + delta;
            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        }
    }
}