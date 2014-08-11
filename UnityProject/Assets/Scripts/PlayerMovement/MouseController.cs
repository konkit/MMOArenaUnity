using UnityEngine;
using System.Collections;

public class MouseController : MonoBehaviour {

    bool isEnabled;

	public float mouseSensX = 2f;
	public float mouseSensY = 1f;
	
	public Camera mainCamera;
	public Vector3 cameraLocalPos = new Vector3(0.86f, 2f, -1.8f);
	
	public float cntCameraYaw;
	public float cntCameraPitch;
	
	public Texture crosshairTexture;
	
	Vector3 prevMousePos = Vector3.zero;
	Vector3 prevPlrPos = Vector3.zero;

	Quaternion yawQuaternion;
	
	void Awake() {
		mainCamera = Camera.main;	
	}
	
	// Use this for initialization
	void Start () {
        Enable();
	}
	
	void OnDestroy() {
        Disable();
	}
	
	// Update is called once per frame
	void Update () {
        if (!isEnabled)
        {
            return;
        }

		Vector3 cntPos = Input.mousePosition;
		Vector3 diffVec = new Vector3( Input.GetAxis("Mouse X") * mouseSensX, 
									   -Input.GetAxis("Mouse Y") * mouseSensY, 
									   0 );
		
		
		
		cntCameraYaw += diffVec.x * mouseSensX;
		cntCameraPitch += diffVec.y * mouseSensY;
		
		yawQuaternion = Quaternion.AngleAxis(cntCameraYaw, new Vector3(0f, 1f, 0f) );
		Quaternion pitchQuaternion = Quaternion.AngleAxis(cntCameraPitch, new Vector3(1f, 0f, 0f) );		
		mainCamera.transform.rotation = GetRotation();
		
		mainCamera.transform.position = yawQuaternion * cameraLocalPos + transform.position;
		
		if( (transform.position - prevPlrPos).sqrMagnitude > 0.001 ) {
			transform.localRotation = yawQuaternion;
		}
		
		
		prevMousePos = cntPos;
		prevPlrPos = transform.position;	
	}

	public void AlignRotation() {
		transform.localRotation = yawQuaternion;
	}

			
	void OnGUI () {
		Rect crosshairRect = new Rect( Screen.width/2 - crosshairTexture.width/2,
									   Screen.height/2 - crosshairTexture.height/2,
										crosshairTexture.width,
										crosshairTexture.height );
		GUI.DrawTexture(crosshairRect, crosshairTexture);
	}
	
	public Quaternion GetRotation() {
		Quaternion yawQuaternion = Quaternion.AngleAxis(cntCameraYaw, new Vector3(0f, 1f, 0f) );
		Quaternion pitchQuaternion = Quaternion.AngleAxis(cntCameraPitch, new Vector3(1f, 0f, 0f) );		
		return yawQuaternion * pitchQuaternion;
	}

    public void Disable()
    {
        isEnabled = false;

        Screen.showCursor = true;
        Screen.lockCursor = false;
    }

    public void Enable()
    {
        isEnabled = true;

        Screen.showCursor = false;
        Screen.lockCursor = true;
    }
}
