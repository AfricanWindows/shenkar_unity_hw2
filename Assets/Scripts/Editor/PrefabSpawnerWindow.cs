using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PrefabSpawnerWindow : EditorWindow
{
    private static bool _isSpawningEnabled = false;
    private int _selectedIndex = 0;
    private GUIStyle _labelStyle;
    private Dictionary<string, GameObject> _prefabDictionary;

    // The list comes from TilePalette, there is no duplicated array of names here.
    private string[] _dropDownOptions = new string[0];

    [MenuItem("Tools/Prefab Spawner")]
    public static void ShowWindow()
    {
       var window = GetWindow<PrefabSpawnerWindow>();
       window.titleContent = new GUIContent("Prefab Spawner");
       window.Show();
    }

    private void OnEnable()
    {
        _labelStyle = new GUIStyle();
        _labelStyle.normal.textColor = Color.white;

        SceneView.duringSceneGui += OnSceneGUI;

        LoadPrefabs();
    }

    private void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneGUI;
    }

    private void OnGUI()
    {
        EditorGUILayout.Space();

        if(_dropDownOptions.Length == 0)
        {
            EditorGUILayout.HelpBox("No palette prefab was found in Assets/Resources/Tiles.", MessageType.Warning);
            if(GUILayout.Button("Refresh List"))
                LoadPrefabs();

            return;
        }

        _selectedIndex = EditorGUILayout.Popup("Select Option",_selectedIndex,_dropDownOptions);
        EditorGUILayout.Space();

        if (GUILayout.Button("Toggle Prefab Spawning"))
            TogglePrefabSpawning();

        if (GUILayout.Button("Refresh List"))
            LoadPrefabs();

        GUILayout.Label("Prefab Spawning Status: " + (_isSpawningEnabled ? "<color=yellow>Enabled</color>" : "<color=red>Disabled</color>"), _labelStyle);
    }

    private void TogglePrefabSpawning()
    {
       _isSpawningEnabled = !_isSpawningEnabled;
    }

    private void OnSceneGUI(SceneView sceneView)
    {
        if(_isSpawningEnabled && _prefabDictionary != null &&
            _selectedIndex < _dropDownOptions.Length &&
            _prefabDictionary.ContainsKey(_dropDownOptions[_selectedIndex]))
        {
            Event current = Event.current;
            if(current.type == EventType.MouseDown && current.button == 1)
            {
                Debug.Log("_selectedIndex " + _selectedIndex);
                Ray ray = HandleUtility.GUIPointToWorldRay(current.mousePosition);
                Vector3 mouseWorldPos = ray.origin;
                Vector3 mouseWorldPosRounded = new Vector3(Mathf.RoundToInt(mouseWorldPos.x), Mathf.RoundToInt(mouseWorldPos.y), 0);
                // PrefabUtility keeps the link to the prefab, plain Instantiate does not.
                GameObject spawned = PrefabUtility.InstantiatePrefab(
                    _prefabDictionary[_dropDownOptions[_selectedIndex]]) as GameObject;

                if(spawned != null)
                {
                    spawned.transform.position = mouseWorldPosRounded;
                    Undo.RegisterCreatedObjectUndo(spawned,"Spawn " + spawned.name);
                    Selection.activeGameObject = spawned;
                }

                Debug.Log("Mouse Position in Scene " + mouseWorldPosRounded);
            }
        }
    }

    private void LoadPrefabs()
    {
        _prefabDictionary = new Dictionary<string, GameObject>();
        List<string> loadedNames = new List<string>();

        // Prefabs that do not exist yet are skipped, so they never reach the drop down.
        foreach (string n in TilePalette.GetAllPrefabNames())
        {
            GameObject prefab = Resources.Load<GameObject>("Tiles/" + n);
            if(prefab == null)
                continue;

            _prefabDictionary.Add(n,prefab);
            loadedNames.Add(n);
        }

        _dropDownOptions = loadedNames.ToArray();
        _selectedIndex = Mathf.Clamp(_selectedIndex,0,Mathf.Max(0,_dropDownOptions.Length - 1));
    }
}
