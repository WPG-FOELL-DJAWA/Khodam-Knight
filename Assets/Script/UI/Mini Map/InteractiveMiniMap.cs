using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class InteractiveMiniMap : MonoBehaviour
{
    [SerializeField] private GameObject _miniMapObject;
    [SerializeField] private PlayerSO _playerSO;

    [Space(10)]
    [Header("Mini map utilities")]
    private Image _miniMapImage;
    [SerializeField] private TextMeshProUGUI _currentMapName;
    [SerializeField] private float _dragSpeed = 1;
    [SerializeField] private float _maxHorizontalOffset = 100f;
    [SerializeField] private float _maxVerticalOffset = 100f;
    [SerializeField] private float _zoomSpeed = .5f;
    [SerializeField] private float _minZoom = 0.5f;
    [SerializeField] private float _maxZoom = 2f;
    private Vector2 _defaultPosition;
    private Vector3 _defaultZoom;


    [Space(10)]
    [Header("Mini map description component")]
    [SerializeField] private GameObject _miniMapDesc;
    [SerializeField] private Image _iconImage;
    [SerializeField] private TextMeshProUGUI _iconName;
    [SerializeField] private TextMeshProUGUI _iconDesc;
    [SerializeField] private Button _descCloseButton;
    [SerializeField] private Button _teleportButton;

    [Space(10)]
    [Header("Mini map component")]
    private GameObject _defaultMap;
    private string _defaultMapName;
    private GameObject _currentMap;

    [System.Serializable]
    struct Map
    {
        public MapName mapName;
        public GameObject map;
    }
    [Space(10)]
    [Header("Mini map")]
    [SerializeField] private List<Map> _map;

    private Vector3 _dragOrigin;
    private MapName _teleportTargetMap;
    private Vector3 _teleportTargetPost;


    private void Awake()
    {
        _teleportButton.onClick.AddListener(() =>
           {
               teleport(_teleportTargetMap, _teleportTargetPost);
           });

        _descCloseButton.onClick.AddListener(() =>
           {
               closeDesc();
           });
    }
    private void Start()
    {
        setUpMap(RoamingMapData.instance.getMapName());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _dragOrigin = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            dragMinimap(Input.mousePosition);
        }

        zoomMinimap();

    }

    private void setUpMap(MapName mapName)
    {
        foreach (var mapObject in _map)
        {
            if (mapObject.mapName == mapName)
            {
                _defaultMap = mapObject.map;
                _miniMapImage = mapObject.map.GetComponent<Image>();
                _defaultMapName = SplitCamelCase(mapObject.mapName.ToString());
                _currentMapName.text = SplitCamelCase(mapObject.mapName.ToString());

                _currentMap = mapObject.map;
                _defaultPosition = mapObject.map.GetComponent<RectTransform>().anchoredPosition;
                _defaultZoom = mapObject.map.GetComponent<RectTransform>().localScale;
                break;
            }
        }
    }

    public static string SplitCamelCase(string input)
    {
        return System.Text.RegularExpressions.Regex.Replace(input, "(\\B[A-Z])", " $1");
    }

    private void dragMinimap(Vector3 currentMouse)
    {
        Vector3 mouseDelta = currentMouse - _dragOrigin;

        Vector2 newPosition = _miniMapImage.rectTransform.anchoredPosition + new Vector2(mouseDelta.x, mouseDelta.y) * _dragSpeed * Time.deltaTime;

        newPosition.x = Mathf.Clamp(newPosition.x, -_maxHorizontalOffset, _maxHorizontalOffset);

        newPosition.y = Mathf.Clamp(newPosition.y, -_maxVerticalOffset, _maxVerticalOffset);

        _miniMapImage.rectTransform.anchoredPosition = newPosition;

        _dragOrigin = currentMouse;
    }

    private void zoomMinimap()
    {
        float zoom = -Input.GetAxis("Mouse ScrollWheel");
        float newScale = Mathf.Clamp(_miniMapImage.rectTransform.localScale.x - zoom * _zoomSpeed, _minZoom, _maxZoom);
        _miniMapImage.rectTransform.localScale = new Vector3(newScale, newScale, 1f);
    }

    /// <summary>
    /// change map
    /// </summary>
    /// <param name="mapMap"></param>
    /// <param name="mapName"></param>
    public void changeMap(MapName mapName)
    {
        _currentMap.SetActive(false);
        if (_miniMapDesc.activeSelf)
            _miniMapDesc.SetActive(false);

        // reset previously image first
        _miniMapImage.rectTransform.anchoredPosition = _defaultPosition;
        _miniMapImage.rectTransform.localScale = _defaultZoom;

        //then change current image to new image
        foreach (var mapObject in _map)
        {
            if (mapObject.mapName == mapName)
            {
                _miniMapImage = mapObject.map.GetComponent<Image>();
                _currentMap = mapObject.map;
                _currentMapName.text = mapObject.mapName.ToString();
                mapObject.map.SetActive(true);
                break;
            }
        }
    }
    public void openInteractiveMap()
    {
        _miniMapObject.SetActive(true);
    }

    public void closeInteractiveMap()
    {
        _miniMapObject.SetActive(false);

        //close current map
        _currentMap.SetActive(false);
        if (_miniMapDesc.activeSelf)
            _miniMapDesc.SetActive(false);

        //change all to default
        _miniMapImage.rectTransform.anchoredPosition = _defaultPosition;
        _miniMapImage.rectTransform.localScale = _defaultZoom;

        _miniMapImage = _defaultMap.GetComponent<Image>();
        _currentMapName.text = _defaultMapName;
        _currentMap = _defaultMap;
        _currentMap.SetActive(true);
    }

    private void closeDesc()
    {
        _miniMapDesc.SetActive(false);
    }

    /// <summary>
    /// open map icon description
    /// </summary>
    /// <param name="iconImage"></param>
    /// <param name="iconName"></param>
    /// <param name="iconDesc"></param>
    /// <param name="isTeleport"></param>
    /// <param name="targetScene"></param>
    /// <param name="targetPost"></param>
    public void openIcon(Sprite iconImage, string iconName, string iconDesc, bool isTeleport, MapName targetScene, Vector3 targetPost)
    {
        _iconImage.sprite = iconImage;
        _iconName.text = iconName;
        _iconDesc.text = iconDesc;

        if (isTeleport)
        {
            _teleportButton.gameObject.SetActive(true);
            _teleportTargetMap = targetScene;
            _teleportTargetPost = targetPost;
        }
        else _teleportButton.gameObject.SetActive(false);

        _miniMapDesc.SetActive(true);
    }

    private void teleport(MapName targetScene, Vector3 targetPost)
    {
        _playerSO.lastScene = targetScene;
        _playerSO.lastPosition = targetPost;
        closeInteractiveMap();
        LoadScene.instance.loadScene(targetScene);

    }

    public void autoNavigation(ConditionalTask task, MiniMapIconTask targetIcon) // noted : masih belum beres
    {
        changeMap(task.getMapTarget());

        Vector2 currentMapPost = _miniMapImage.rectTransform.anchoredPosition;
        Vector2 iconPost = targetIcon.GetComponent<Image>().rectTransform.anchoredPosition;

        Vector2 MapPostDelta = currentMapPost - iconPost;

        Vector2 newPosition = _miniMapImage.rectTransform.anchoredPosition + new Vector2(MapPostDelta.x, MapPostDelta.y);

        newPosition.x = Mathf.Clamp(newPosition.x, -_maxHorizontalOffset, _maxHorizontalOffset);

        newPosition.y = Mathf.Clamp(newPosition.y, -_maxVerticalOffset, _maxVerticalOffset);

        _miniMapImage.rectTransform.anchoredPosition = newPosition;

        targetIcon.showIconDesc();
    }
}