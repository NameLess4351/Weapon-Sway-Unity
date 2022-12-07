using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    public WeaponswaySpring PositionalSpring;
    public WeaponswaySpring RotaionalSpring;

    [Space]
    public Transform SwayTransform;

    Vector3 WeaponSwayPosition;
    Vector3 WeaponSwayRotation;

    Vector3 LastWSPosition;
    Vector3 _LastWSPosition;

    Vector3 LastWSRotation;
    Vector3 _LastWSRotation;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        WeaponSpringSway();
    }

    private void WeaponSpringSway()
    {
        float mousePosX = Input.GetAxisRaw("Mouse X") * PositionalSpring.Sensitivity;
        float mousePosY = Input.GetAxisRaw("Mouse Y") * PositionalSpring.Sensitivity;

        float mouseRotX = -Input.GetAxisRaw("Mouse Y") * RotaionalSpring.Sensitivity;
        float mouseRotY = Input.GetAxisRaw("Mouse X") * RotaionalSpring.Sensitivity;
        float mouseRotZ = -Input.GetAxisRaw("Mouse X") * RotaionalSpring.Sensitivity;

        _LastWSPosition = WeaponSwayPosition;
        _LastWSRotation = WeaponSwayRotation;

        Vector3 TargetPosition = new Vector3(mousePosX, mousePosY, 0);  
        Vector3 TargetRotation = new Vector3(mouseRotX, mouseRotY, mouseRotZ);

        WeaponSwayPosition = Vector3.Lerp(WeaponSwayPosition, TargetPosition, (1 - PositionalSpring.Springyness) * PositionalSpring.Stiffness * Time.fixedDeltaTime);
        WeaponSwayPosition = (1 + PositionalSpring.Springyness) * WeaponSwayPosition - PositionalSpring.Springyness * LastWSPosition;

        WeaponSwayRotation = Vector3.Lerp(WeaponSwayRotation, TargetRotation, (1 - RotaionalSpring.Springyness) * RotaionalSpring.Stiffness * Time.fixedDeltaTime);
        WeaponSwayRotation = (1 + RotaionalSpring.Springyness) * WeaponSwayRotation - RotaionalSpring.Springyness * LastWSRotation;

        SwayTransform.localPosition = WeaponSwayPosition;
        SwayTransform.localRotation = Quaternion.Euler(WeaponSwayRotation);

        LastWSPosition = _LastWSPosition;

        LastWSRotation = _LastWSRotation;
    }
}

[System.Serializable]
public struct WeaponswaySpring
{
    [Header("Spring")]
    public float Sensitivity;
    public float Springyness;
    public float Stiffness;
}
