using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float mMouseSensitivity = 3.0f;

    private float mRotationX;

    [SerializeField]
    private Transform mTarget;

    [SerializeField]
    private float mDistanceFromTarget = 3.0f;

    private Vector3 mCurrentRotation;
    private Vector3 mSmoothVelocity = Vector3.zero;

    [SerializeField]
    private float mSmoothTime = 0.2f;

    [SerializeField]
    private Vector2 mRotationXMinMax = new Vector2(-40, 40);

    void Update()
    {
        float lMouseY = Input.GetAxis("Mouse Y") * mMouseSensitivity;

        mRotationX += lMouseY;

        // Apply clamping for x rotation 
        mRotationX = Mathf.Clamp(mRotationX, mRotationXMinMax.x, mRotationXMinMax.y);

        Vector3 lNextRotation = new Vector3(mRotationX, 0);

        // Apply damping between rotation changes
        mCurrentRotation = Vector3.SmoothDamp(mCurrentRotation, lNextRotation, ref mSmoothVelocity, mSmoothTime);
        transform.localEulerAngles = mCurrentRotation;

        // Substract forward vector of the GameObject to point its forward vector to the target
        transform.position = mTarget.position - transform.forward * mDistanceFromTarget;



    }

}
