using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

[InitializeOnLoad]
public class AutoSetupCompleteProject
{
    private const string SetupKey = "Proyecto_Unity_Completo_RV_Videojuegos_SetupDone";

    static AutoSetupCompleteProject()
    {
        EditorApplication.delayCall += SetupSceneOnce;
    }

    static void SetupSceneOnce()
    {
        if (EditorPrefs.GetBool(SetupKey, false))
            return;

        string scenePath = "Assets/Scenes/Nivel_Prototipo_Completo.unity";

        if (!File.Exists(scenePath))
        {
            CreateScene(scenePath);
        }

        EditorPrefs.SetBool(SetupKey, true);
    }

    [MenuItem("Proyecto Final/Crear o regenerar proyecto completo")]
    public static void RegenerateCompleteProject()
    {
        string scenePath = "Assets/Scenes/Nivel_Prototipo_Completo.unity";
        CreateScene(scenePath);
        EditorPrefs.SetBool(SetupKey, true);
        Debug.Log("Proyecto completo regenerado correctamente.");
    }

    static Material CreateMaterial(string name, Color color)
    {
        string folder = "Assets/Materials";
        if (!AssetDatabase.IsValidFolder(folder))
        {
            AssetDatabase.CreateFolder("Assets", "Materials");
        }

        string path = folder + "/" + name + ".mat";
        Material mat = AssetDatabase.LoadAssetAtPath<Material>(path);

        if (mat == null)
        {
            mat = new Material(Shader.Find("Standard"));
            mat.color = color;
            AssetDatabase.CreateAsset(mat, path);
        }

        return mat;
    }

    static GameObject Cube(string name, Vector3 position, Vector3 scale, Material material)
    {
        GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        obj.name = name;
        obj.transform.position = position;
        obj.transform.localScale = scale;

        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.sharedMaterial = material;
        }

        return obj;
    }

    static Text CreateText(string name, Transform parent, Vector2 anchorMin, Vector2 anchorMax, Vector2 anchoredPosition, Vector2 size, int fontSize, TextAnchor alignment)
    {
        GameObject textObj = new GameObject(name);
        textObj.transform.SetParent(parent);

        RectTransform rect = textObj.AddComponent<RectTransform>();
        rect.anchorMin = anchorMin;
        rect.anchorMax = anchorMax;
        rect.anchoredPosition = anchoredPosition;
        rect.sizeDelta = size;

        Text text = textObj.AddComponent<Text>();
        text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        text.fontSize = fontSize;
        text.alignment = alignment;
        text.color = Color.white;

        return text;
    }

    static void CreateScene(string scenePath)
    {
        Scene scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

        Material floorMat = CreateMaterial("MAT_01_Suelo_Gris", new Color(0.45f, 0.45f, 0.45f));
        Material wallMat = CreateMaterial("MAT_02_Pared_Azul", new Color(0.12f, 0.28f, 0.65f));
        Material obstacleMat = CreateMaterial("MAT_03_Obstaculo_Naranja", new Color(1f, 0.45f, 0.08f));
        Material keyMat = CreateMaterial("MAT_04_Llave_Amarilla", new Color(1f, 0.85f, 0.05f));
        Material doorMat = CreateMaterial("MAT_05_Puerta_Roja", new Color(0.8f, 0.05f, 0.05f));
        Material goalMat = CreateMaterial("MAT_06_Meta_Verde", new Color(0.05f, 0.75f, 0.25f));
        Material pathMat = CreateMaterial("MAT_07_Camino_Claro", new Color(0.72f, 0.72f, 0.72f));

        // Piso y camino principal
        Cube("Suelo_General_Blocking", new Vector3(0, -0.05f, 0), new Vector3(24f, 0.1f, 36f), floorMat);
        Cube("Camino_Principal", new Vector3(0, 0.01f, 0), new Vector3(6f, 0.05f, 32f), pathMat);

        // Límites
        Cube("Limite_Izquierdo", new Vector3(-12, 1.5f, 0), new Vector3(0.4f, 3f, 36f), wallMat);
        Cube("Limite_Derecho", new Vector3(12, 1.5f, 0), new Vector3(0.4f, 3f, 36f), wallMat);
        Cube("Limite_Inicio", new Vector3(0, 1.5f, -18), new Vector3(24f, 3f, 0.4f), wallMat);
        Cube("Limite_Final", new Vector3(0, 1.5f, 18), new Vector3(24f, 3f, 0.4f), wallMat);

        // Blocking del nivel
        Cube("Bloqueo_Seccion_01", new Vector3(-4, 1.5f, -10), new Vector3(12f, 3f, 0.4f), wallMat);
        Cube("Bloqueo_Seccion_02", new Vector3(4, 1.5f, -3), new Vector3(12f, 3f, 0.4f), wallMat);
        Cube("Bloqueo_Seccion_03", new Vector3(-4, 1.5f, 4), new Vector3(12f, 3f, 0.4f), wallMat);

        // Obstáculos
        Cube("Obstaculo_Cubo_01", new Vector3(4, 0.55f, -14), new Vector3(2f, 1.1f, 2f), obstacleMat);
        Cube("Obstaculo_Cubo_02", new Vector3(-6, 0.55f, -6), new Vector3(2f, 1.1f, 2f), obstacleMat);
        Cube("Obstaculo_Cubo_03", new Vector3(6, 0.55f, 1), new Vector3(2f, 1.1f, 2f), obstacleMat);
        Cube("Obstaculo_Cubo_04", new Vector3(-6, 0.55f, 9), new Vector3(2f, 1.1f, 2f), obstacleMat);

        // Llave
        GameObject key = Cube("Llave_Recogible", new Vector3(-7, 0.7f, -15), new Vector3(0.75f, 0.75f, 0.75f), keyMat);
        key.GetComponent<Collider>().isTrigger = true;
        key.AddComponent<KeyItem>();

        // Puerta
        GameObject door = Cube("Puerta_Bloqueada", new Vector3(0, 1.5f, 12), new Vector3(5.5f, 3f, 0.4f), doorMat);
        door.GetComponent<Collider>().isTrigger = true;
        door.AddComponent<DoorController>();

        // Meta
        GameObject goal = Cube("Zona_Final_Meta", new Vector3(0, 0.08f, 16), new Vector3(6f, 0.12f, 3f), goalMat);
        goal.GetComponent<Collider>().isTrigger = true;
        goal.AddComponent<GoalZone>();

        // Jugador
        GameObject player = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        player.name = "Jugador_Primera_Persona";
        player.transform.position = new Vector3(0, 1.1f, -16);

        Object.DestroyImmediate(player.GetComponent<CapsuleCollider>());

        CharacterController controller = player.AddComponent<CharacterController>();
        controller.height = 2f;
        controller.radius = 0.45f;
        controller.center = new Vector3(0, 1f, 0);

        PlayerMovement movement = player.AddComponent<PlayerMovement>();
        PlayerInteraction interaction = player.AddComponent<PlayerInteraction>();

        // Cámara
        GameObject camera = new GameObject("Main Camera");
        camera.tag = "MainCamera";
        camera.transform.SetParent(player.transform);
        camera.transform.localPosition = new Vector3(0, 1.65f, 0);
        camera.transform.localRotation = Quaternion.identity;
        camera.AddComponent<Camera>();
        camera.AddComponent<AudioListener>();

        // Luces
        GameObject lightObj = new GameObject("Directional Light");
        Light light = lightObj.AddComponent<Light>();
        light.type = LightType.Directional;
        light.intensity = 1.2f;
        lightObj.transform.rotation = Quaternion.Euler(55f, -35f, 0f);

        GameObject pointLight = new GameObject("Luz_Meta");
        Light pLight = pointLight.AddComponent<Light>();
        pLight.type = LightType.Point;
        pLight.range = 10f;
        pLight.intensity = 2f;
        pointLight.transform.position = new Vector3(0, 4f, 14f);

        RenderSettings.ambientLight = new Color(0.45f, 0.45f, 0.45f);

        // UI
        GameObject canvasObj = new GameObject("Canvas_UI_Prototipo");
        Canvas canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasObj.AddComponent<CanvasScaler>();
        canvasObj.AddComponent<GraphicRaycaster>();

        Text objectiveText = CreateText(
            "Texto_Objetivo",
            canvasObj.transform,
            new Vector2(0.02f, 0.88f),
            new Vector2(0.98f, 0.98f),
            Vector2.zero,
            new Vector2(0, 0),
            22,
            TextAnchor.UpperLeft
        );

        Text statusText = CreateText(
            "Texto_Estado",
            canvasObj.transform,
            new Vector2(0.02f, 0.66f),
            new Vector2(0.42f, 0.86f),
            Vector2.zero,
            new Vector2(0, 0),
            18,
            TextAnchor.UpperLeft
        );

        GameObject gameManagerObj = new GameObject("GameManager");
        GameManager manager = gameManagerObj.AddComponent<GameManager>();
        manager.objectiveText = objectiveText;
        manager.statusText = statusText;

        interaction.gameManager = manager;

        // Texto descriptivo 3D
        GameObject title = new GameObject("Titulo_Entrega");
        TextMesh titleMesh = title.AddComponent<TextMesh>();
        titleMesh.text = "Prototipo RV y Videojuegos\nBlocking + Mecánica Principal";
        titleMesh.fontSize = 40;
        titleMesh.anchor = TextAnchor.MiddleCenter;
        titleMesh.color = Color.white;
        title.transform.position = new Vector3(0, 3.5f, -17.4f);
        title.transform.rotation = Quaternion.Euler(0, 0, 0);
        title.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);

        EditorSceneManager.SaveScene(scene, scenePath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorSceneManager.OpenScene(scenePath);
    }
}
