using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSpinner : MonoBehaviour
{

  private Transform _transform;
  private Vector3 mousePosition;
  private Vector3 screenPosition;
  private Vector3[] prevPosition = new Vector3[5];  // nフレームの平均をとる
  private int prevPositionIndex;  // nフレームの平均をとる
  private Vector3 rotationPreset;
  private float rotationSpeed = 5f;
  private float rotationReduceSpeed = 0.1f;
  private float rotationDeltaSpeed;

  void Start()
  {
    _transform = this.transform;
  }

  void Update ()
  {
    mousePosition = Input.mousePosition;
    mousePosition.z = 10f;
    screenPosition = Camera.main.ScreenToWorldPoint(mousePosition);
    Vector3 diff = (screenPosition - _transform.position);
    if(Input.GetMouseButton(0))
    {
      _transform.rotation = Quaternion.FromToRotation (Vector3.up, diff);
    }
    else if(Input.GetMouseButtonUp(0))
    {
      rotationDeltaSpeed = GetAvgDeltaSpeed(prevPosition);
      rotationPreset = new Vector3(0,0,_transform.rotation.z);
    }
    else
    {
      _transform.Rotate(new Vector3(0, 0, rotationDeltaSpeed) * Time.deltaTime * rotationSpeed, Space.World);
      if(rotationDeltaSpeed>0f)
      {
        rotationDeltaSpeed -= rotationReduceSpeed;
      }
      else if(rotationDeltaSpeed<0f)
      {
        rotationDeltaSpeed += rotationReduceSpeed;
      }
      
    }
    if(prevPositionIndex >= prevPosition.Length-1)
    {
      prevPositionIndex=0;
    }else{
      prevPositionIndex+=1;
    }
    prevPosition[prevPositionIndex] = screenPosition;
  }

  public float GetDiff(Vector2 p1, Vector2 p2)
  {
    float dx1 = 0f - p1.x;
    float dy1 = 0f - p1.y;
    float dx2 = 0f - p2.x;
    float dy2 = 0f - p2.y;
    float rad1 = Mathf.Atan2(dy1, dx1);
    float rad2 = Mathf.Atan2(dy2, dx2);
    float diff = rad1 * Mathf.Rad2Deg - rad2 * Mathf.Rad2Deg;
    return diff;
  }

  public float GetAvgDeltaSpeed(Vector3[] pos)
  {
    float sum = 0f;
    for(int i=0;i<pos.Length;i++)
    {
      sum += GetDiff(screenPosition, pos[i]);
    }
    return sum/pos.Length;
  }
}