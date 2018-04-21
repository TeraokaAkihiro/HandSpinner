using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotationCounter : MonoBehaviour {

  public GameObject textObj;
  private Text _text;

  private Transform _transform;

  private int forwardCount;
  private int reverceCount;
  private int rotationCount;
  private int rotationPart;
  private int prevRotationPart;

  // Use this for initialization
  void Start () {
    _transform = this.transform;
    _text = textObj.GetComponent<Text>();
  }
  
  // Update is called once per frame
  void FixedUpdate()
  {
    float angleZ = _transform.eulerAngles.z;
    if(angleZ < 120f)
    {
      rotationPart = 1;
    }
    else if(angleZ < 240f)
    {
      rotationPart = 2;
    }
    else
    {
      rotationPart = 3;
    }

    if ((rotationPart == prevRotationPart + 1)|(rotationPart == prevRotationPart - 2))
    {
      if (rotationCount>0)
      {
        rotationCount=0;
      }
      else{
        rotationCount-=1;
      }
    }
    else if ((rotationPart == prevRotationPart - 1)|(rotationPart == prevRotationPart + 2))
    {
      if (rotationCount<0)
      {
        rotationCount=0;
      }
      else
      {
        rotationCount+=1;
      }
    }
    if(rotationCount>=3)
    {
      rotationCount = 0;
      forwardCount+=1;
    }else if(rotationCount<=-3)
    {
      rotationCount = 0;
      reverceCount+=1;
    }

    if(_text!=null)
    {
      _text.text = (forwardCount+reverceCount).ToString();
    }

    prevRotationPart = rotationPart;

  }
}
