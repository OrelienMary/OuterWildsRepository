using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Planet : MonoBehaviour
{
    public string path;
    string prefabPath;

    [Range(2,256)]
    public int resolution = 10;
    public bool autoUpdate = true;
    public enum FaceRenderMask { All, Top, Bottom, Left, Right, Front, Back};
    public FaceRenderMask faceRenderMask;

    public ShapeSettings shapeSettings;
    public ColorSettings colorSettings;

    [HideInInspector]
    public bool shapeSettingsFoldOut;
    [HideInInspector]
    public bool colorSettingsFoldOut;

    ShapeGenerator shapeGenerator = new ShapeGenerator();
    ColorGenerator colorGenerator = new ColorGenerator();

    [SerializeField, HideInInspector]
    MeshFilter[] meshFilters;
    TerrainFace[] terrainFaces;

    private void Start()
    {
        //GeneratePlanet();
    }

    void Initialize()
    {
        shapeGenerator.UpdateSettings(shapeSettings);
        colorGenerator.UpdateSettings(colorSettings);

        if(meshFilters == null || meshFilters.Length == 0)
        {
            meshFilters = new MeshFilter[6];
        }

        terrainFaces = new TerrainFace[6];

        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

        for (int i = 0; i < 6; i++)
        {
            if(meshFilters[i] == null)
            {
                GameObject meshObj = new GameObject("mesh");

                meshObj.transform.parent = transform;

                meshObj.AddComponent<MeshRenderer>();
                meshFilters[i] = meshObj.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();

                meshObj.AddComponent<MeshCollider>();

                meshObj.layer = LayerMask.NameToLayer("Ground");
            }
            meshFilters[i].GetComponent<MeshRenderer>().sharedMaterial = colorSettings.planetMaterial;

            terrainFaces[i] = new TerrainFace(shapeGenerator ,meshFilters[i].sharedMesh, resolution, directions[i]);

            bool renderFace = faceRenderMask == FaceRenderMask.All || (int)faceRenderMask - 1 == i;
            meshFilters[i].gameObject.SetActive(renderFace);
        }
    }

    public void GeneratePlanet()
    {
        Initialize();
        GenerateMesh();
        GenerateColors();
    }

    public void OnShapeSettingsUpdated()
    {
        if(autoUpdate)
        {
            Initialize();
            GenerateMesh();
        }
        
    }

    public void OnColorSettingsUpdated()
    {
        if (autoUpdate)
        {
            Initialize();
            GenerateColors();
        }
    }

    void GenerateMesh()
    {
        for(int i = 0; i < 6; i++)
        {
            if(meshFilters[i].gameObject.activeSelf)
            {
                terrainFaces[i].ConstructMesh();
            }
        }

        colorGenerator.UpdateElevation(shapeGenerator.elevationMinMax);
    }

    void GenerateColors()
    {
        colorGenerator.UpdateColors();
    }

    public void SavePrefabAndMeshes()
    {
#if UNITY_EDITOR
        string originalPath = path;

        AssetDatabase.CreateFolder(path, transform.gameObject.name);

        path = path + "/" + transform.gameObject.name;

        AssetDatabase.CreateAsset(colorGenerator.texture, path + "/" + transform.gameObject.name + "Texture" + ".asset");

        Material mat = new Material(colorSettings.planetMaterial);

        mat.SetTexture("_planetTexture", colorGenerator.texture);

        AssetDatabase.CreateAsset(mat, path + "/" + transform.gameObject.name + "Material" + ".mat");

        //Enregistrement Meshes
        for (int i = 0; i < 6; i++)
        {
            meshFilters[i].GetComponent<MeshRenderer>().sharedMaterial = mat;
            SaveMesh(terrainFaces[i].mesh, transform.gameObject.name + "Mesh_" + (i + 1), path);
        }        

        //Enregistrement Prefab
        prefabPath = path;

        prefabPath = prefabPath + "/" + transform.gameObject.name + ".prefab";
        prefabPath = AssetDatabase.GenerateUniqueAssetPath(prefabPath);

        if(PrefabUtility.IsPartOfRegularPrefab(gameObject))
            PrefabUtility.UnpackPrefabInstance(transform.gameObject, PrefabUnpackMode.Completely, InteractionMode.UserAction);

        PrefabUtility.SaveAsPrefabAssetAndConnect(transform.gameObject, prefabPath, InteractionMode.UserAction);
        AssetDatabase.SaveAssets();

        path = originalPath;

        DestroyImmediate(this);
#endif
    }

    void SaveMesh(Mesh mesh, string name, string path)
    {
#if UNITY_EDITOR
        path = path + "/" + name + ".asset";

        AssetDatabase.CreateAsset(mesh, path);
        AssetDatabase.SaveAssets();
#endif
    }

}
