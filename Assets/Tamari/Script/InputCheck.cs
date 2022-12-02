using UnityEngine;
using System.Collections;

public class InputCheck : MonoBehaviour
{
    private Vector3 acceleration;
    private Compass compass;
    private Quaternion gyro;
    private GUIStyle labelStyle;

    void Start()
    {
        //フォント生成
        this.labelStyle = new GUIStyle();
        this.labelStyle.fontSize = Screen.height / 22;
        this.labelStyle.normal.textColor = Color.white;

        Input.compass.enabled = true;

        Debug.Log(string.Format("<b>精度</b>：{0}", Input.compass.headingAccuracy));
        Debug.Log(string.Format("<b>タイムスタンプ</b>：{0}", Input.compass.timestamp));

        Input.gyro.enabled = true;
    }

    void Update()
    {
        this.acceleration = Input.acceleration;
        this.compass = Input.compass;
        this.gyro = Input.gyro.attitude;
    }

    void OnGUI()
    {
        if (acceleration != null)
        {
            float x = Screen.width / 10;
            float y = 0;
            float w = Screen.width * 8 / 10;
            float h = Screen.height / 20;

            for (int i = 0; i < 12; i++)
            {
                y = Screen.height / 10 + h * i;
                string text = string.Empty;

                switch (i)
                {
                    case 0://X
                        text = string.Format("accel-X:{0}", this.acceleration.x);
                        break;
                    case 1://Y
                        text = string.Format("accel-Y:{0}", this.acceleration.y);
                        break;
                    case 2://Z
                        text = string.Format("accel-Z:{0}", this.acceleration.z);
                        break;
                    case 3://X
                        text = string.Format("comps-X:{0}", this.compass.rawVector.x);
                        break;
                    case 4://Y
                        text = string.Format("comps-Y:{0}", this.compass.rawVector.y);
                        break;
                    case 5://Z
                        text = string.Format("comps-Z:{0}", this.compass.rawVector.z);
                        break;
                    case 6://Z
                        text = string.Format("magneticHeading:{0}", this.compass.magneticHeading);
                        break;
                    case 7://Z
                        text = string.Format("trueHeading:{0}", this.compass.trueHeading);
                        break;
                    case 8://Y
                        text = string.Format("gyro-x:{0}", this.gyro.x);
                        break;
                    case 9://Y
                        text = string.Format("gyro-y:{0}", this.gyro.y);
                        break;
                    case 10://Y
                        text = string.Format("gyro-z:{0}", this.gyro.z);
                        break;
                    case 11://Y
                        text = string.Format("gyro-w:{0}", this.gyro.w);
                        break;
                    default:
                        throw new System.InvalidOperationException();
                }

                GUI.Label(new Rect(x, y, w, h), text, this.labelStyle);
            }
        }
    }
}