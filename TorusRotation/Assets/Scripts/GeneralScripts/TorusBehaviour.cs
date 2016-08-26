using UnityEngine;
using System.Collections;

public class TorusBehaviour : MonoBehaviour 
{
    private const string TorusName = "Torus";
    private const string TorusParentName = "TorusParent";
    [SerializeField] private int _segmentCounterSmall = 5;    
    [SerializeField] private int _segmentCounterBig = 5;

    [SerializeField] private float _smallRadius = 5f;
    [SerializeField] private float _bigRadius = 15f;

    [SerializeField] private GameObject _patternPoint;

    [SerializeField] private Color[] _colors;

    [SerializeField] private float _rotationSpeed = 2f;
    [SerializeField] private float _parentRotationSpeed = 2f;

    private Transform[] _torusPoints;
    private Transform _parent;

	// Use this for initialization
	void Start () 
    {
        CreateAndInitAllPoints();	
        StartCoroutine(Rotation());
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private void CreateAndInitAllPoints()
    {
        _torusPoints = new Transform[_segmentCounterBig];
        var torus = new GameObject();
        torus.name = TorusName;

        for (int j = 0; j < _segmentCounterBig; j++)
        {
            var parent = new GameObject();
            parent.name = TorusParentName + j.ToString();
            float angleParent = j * 360f / _segmentCounterBig;
            angleParent = angleParent * (Mathf.PI / 180f);
            float xParent = _bigRadius * Mathf.Cos(angleParent) + _bigRadius * Mathf.Sin(angleParent);
            float yParent = 0f;
            float zParent = (-1) * _bigRadius * Mathf.Sin(angleParent) + _bigRadius * Mathf.Cos(angleParent);

            parent.transform.position = new Vector3(xParent, yParent, zParent);
        
            for (int i = 0; i < _segmentCounterSmall; i++)
            {
                var go = Instantiate(_patternPoint);
                float angle = i * 360f / _segmentCounterSmall;
                angle = angle * (Mathf.PI / 180f);

                float x = _smallRadius * Mathf.Cos(angle) + _smallRadius * Mathf.Sin(angle);
                float y = _smallRadius * Mathf.Sin(angle) - _smallRadius * Mathf.Cos(angle);
                float z = 0f;
                go.transform.SetParent(parent.transform);
                go.transform.localPosition = new Vector3(x, y, z);

                var mesh = go.GetComponent<MeshRenderer>();
                if (mesh != null)
                {
                    int index = i % _colors.Length;
                    mesh.material.color = _colors[index];
                }
            }
            parent.transform.SetParent(torus.transform);
            angleParent = angleParent * (180f / Mathf.PI);
            parent.transform.rotation = Quaternion.Euler(0f, angleParent - 45, 0f);
            _torusPoints[j] = parent.transform;
        }
        _parent = torus.transform;
    }

    private IEnumerator Rotation()
    {
        float angle = 0f;
        float parentAngle = 0f;
        while (true)
        {
            angle += Time.deltaTime * _rotationSpeed;
            parentAngle += Time.deltaTime * 2 * _parentRotationSpeed;
            if (angle > 360f)
            {
                angle -= 360f;
            }

            if (parentAngle > 360f)
            {
                parentAngle -= 360f;
            }

            for (int i = 0; i < _torusPoints.Length; i++)
            {
                
                var prevAngle = _torusPoints[i].rotation.eulerAngles;
                _torusPoints[i].rotation = Quaternion.Euler(prevAngle.x, prevAngle.y, angle);
            }
            var parentPrevAngle =_parent.rotation.eulerAngles;
            _parent.rotation = Quaternion.Euler(parentPrevAngle.x, parentAngle, parentPrevAngle.z);
            yield return null;
        }
    }
}
