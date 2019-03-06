using UnityEngine;
using System;

[Serializable]
public enum DriveType
{
	RearWheelDrive,
	FrontWheelDrive,
	AllWheelDrive
}

public class WheelDrive : MonoBehaviour
{
    [Tooltip("Maximum steering angle of the wheels")]
	public float maxAngle = 30f;
	[Tooltip("Maximum torque applied to the driving wheels")]
	public float maxTorque = 300f;
	[Tooltip("Maximum brake torque applied to the driving wheels")]
	public float brakeTorque = 30000f;
	[Tooltip("If you need the visual wheels to be attached automatically, drag the wheel shape here.")]
	public GameObject wheelShape;

	[Tooltip("The vehicle's speed when the physics engine can use different amount of sub-steps (in m/s).")]
	public float criticalSpeed = 5f;
	[Tooltip("Simulation sub-steps when the speed is above critical.")]
	public int stepsBelow = 5;
	[Tooltip("Simulation sub-steps when the speed is below critical.")]
	public int stepsAbove = 1;

	[Tooltip("The vehicle's drive type: rear-wheels drive, front-wheels drive or all-wheels drive.")]
	public DriveType driveType;

    private WheelCollider[] m_Wheels;

    public GameObject signal;
    RaycastHit hitInfo = new RaycastHit();
    float angle1 = new float();
    float torque = new float();
    float angle = new float();
    float handBrake = new float();

    // Find all the WheelColliders down in the hierarchy.
    void Start()
	{
		m_Wheels = GetComponentsInChildren<WheelCollider>();

		for (int i = 0; i < m_Wheels.Length; ++i) 
		{
			var wheel = m_Wheels [i];

			// Create wheel shapes only when needed.
			if (wheelShape != null)
			{
				var ws = Instantiate (wheelShape);
				ws.transform.parent = wheel.transform;
			}
		}
	}

	// This is a really simple approach to updating wheels.
	// We simulate a rear wheel drive car and assume that the car is perfectly symmetric at local zero.
	// This helps us to figure our which wheels are front ones and which are rear.
	void Update()
	{
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        
        if(Physics.Raycast(ray,out hitInfo))
        {    
            if(Input.GetMouseButtonDown(0))
            {
                signal.transform.position = hitInfo.point;
                Debug.Log(hitInfo.point);              
            } 
        }

        if (Vector3.Distance(transform.position, signal.transform.position) > 10f)
        {
            torque = 200f;
            handBrake = 0;
            Debug.Log("First gear");
            Vector3 direction = transform.position - signal.transform.position;
            Vector3 velocity = Quaternion.Inverse(transform.rotation) * direction;
            //对目标向量进行反向旋转，得到的新向量与z轴的夹角即为目标向量与当前
            //物体方向的夹角		
            angle1 = Mathf.Atan2(velocity.x, velocity.z) * Mathf.Rad2Deg;


            if (angle1 > 0 && angle1 - 175 < 0)
                angle = -20f;
            else if (angle1 < 0 && angle1 + 175 > 0)
                angle = 20f;
            else
                angle = 0f;


        }
        else if (Vector3.Distance(transform.position, signal.transform.position) >5f)
        {
            torque = 50f;
            handBrake = 0;
            Debug.Log("Second gear");
            Vector3 direction = transform.position - signal.transform.position;
            Vector3 velocity = Quaternion.Inverse(transform.rotation) * direction;
            //对目标向量进行反向旋转，得到的新向量与z轴的夹角即为目标向量与当前
            //物体方向的夹角		
            angle1 = Mathf.Atan2(velocity.x, velocity.z) * Mathf.Rad2Deg;


            if (angle1 > 0 && angle1 - 175 < 0)
                angle = -20f;
            else if (angle1 < 0 && angle1 + 175 > 0)
                angle = 20f;
            else
                angle = 0f;


        }
        else if (Vector3.Distance(transform.position, signal.transform.position) < 5f)
        {
            handBrake = 3000f;
            Debug.Log("pause");

        }



        m_Wheels[0].ConfigureVehicleSubsteps(criticalSpeed, stepsBelow, stepsAbove);

		//float angle = maxAngle * Input.GetAxis("Horizontal");
		//float torque = maxTorque * Input.GetAxis("Vertical");

		//float handBrake = Input.GetKey(KeyCode.X) ? brakeTorque : 0;

		foreach (WheelCollider wheel in m_Wheels)
		{
			// A simple car where front wheels steer while rear ones drive.
			if (wheel.transform.localPosition.z > 0)
				wheel.steerAngle = angle;

			if (wheel.transform.localPosition.z < 0)
			{
				wheel.brakeTorque = handBrake;
			}

			if (wheel.transform.localPosition.z < 0 && driveType != DriveType.FrontWheelDrive)
			{
				wheel.motorTorque = torque;
			}

			if (wheel.transform.localPosition.z >= 0 && driveType != DriveType.RearWheelDrive)
			{
				wheel.motorTorque = torque;
			}

			// Update visual wheels if any.
			if (wheelShape) 
			{
				Quaternion q;
				Vector3 p;
				wheel.GetWorldPose (out p, out q);

				// Assume that the only child of the wheelcollider is the wheel shape.
				Transform shapeTransform = wheel.transform.GetChild (0);
				shapeTransform.position = p;
				shapeTransform.rotation = q;
			}
		}
	}
    
}
