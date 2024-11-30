using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class SnapshotUtility : EditorWindow {
    private const string Version = "1.4.0";
    private const string SaveFileVersion = "2";
    private const string LogPrefix = "[<color=#9fffe3>MintySnapshot Utility</color>] ";
    private static readonly string ProjectUserAgent = $"MintySnapshot Utility/{Version} Internal UnityWebRequest";
    private static bool _updateAvailable;

    private int _pageNumber;

    private static int _languageSelected;
    private readonly string[] _languageOptions = { "English", "日本語", "한국인", "Русский" };

    private int _resolutionSelected;
    private readonly string[] _resolutionOptions = { "Custom", "Standards", "VRChat", "ChilloutVR" };

    private int _standardResSelected;
    private readonly string[] _standardResOptions = { "240p", "360p", "480p", "720p", "1080p", "1440p (2K)", "2160p (4K)", "4320p (8K)" };

    private int _resolutionMultiplier = 1;

    private int _height = 1080, _width = 1920;
    private bool _isTransparent, _openFileDirectory, _openInDefaultImageViewer;

    private static string _japaneseContributors, _koreanContributors, _russianContributors, _newVersionString;

    private static Camera _camera;
    private static bool _useSceneCamera;
    private string _cameraNameFromScene;

    [MenuItem("Tools/Minty Labs/Snapshot Utility")]
    private static void ShowWindow() {
        UpdateFolderStructure();
        CheckForUpdate();
        var eWindow = GetWindow<SnapshotUtility>();
        eWindow.titleContent = new GUIContent("Snapshot Utility");
        eWindow.minSize = new Vector2(500, 650);
        eWindow.autoRepaintOnSceneChange = true;
        eWindow.Show();

        try {
            if (_camera == null)
                _camera = GameObject.Find("Main Camera")?.GetComponent<Camera>();
        }
        catch {
            Debug.LogWarning(LogPrefix + "Could not find Main Camera in scene. Skipping...");
        }
    }

    private static void GetContributors() {
        var wc = new WebClient();
        wc.Headers.Add("User-Agent", ProjectUserAgent);
        _japaneseContributors = wc.DownloadString("https://raw.githubusercontent.com/Minty-Labs/Unity-Tools/main/Unity-Snapshot-Utility/Remote/JapaneseContributors.txt");
        _koreanContributors = wc.DownloadString("https://raw.githubusercontent.com/Minty-Labs/Unity-Tools/main/Unity-Snapshot-Utility/Remote/KoreanContributors.txt");
        _russianContributors = wc.DownloadString("https://raw.githubusercontent.com/Minty-Labs/Unity-Tools/main/Unity-Snapshot-Utility/Remote/RussianContributors.txt");
        wc.Dispose();
    }

    private static void CheckForUpdate() {
        Debug.Log(LogPrefix + LanguageModel.CheckingForUpdate(_languageSelected));
        var wc = new WebClient();
        wc.Headers.Add("User-Agent", ProjectUserAgent);
        _newVersionString = wc.DownloadString("https://raw.githubusercontent.com/Minty-Labs/Unity-Tools/main/Unity-Snapshot-Utility/Remote/version.txt");
        _updateAvailable = _newVersionString != Version;
        Debug.Log(LogPrefix + (_updateAvailable ? LanguageModel.UpdateAvailable(_languageSelected) : LanguageModel.NoUpdateAvailable(_languageSelected)));
        wc.Dispose();
    }

    private void OnGUI() {
        var defaultBgColor = GUI.backgroundColor;
        EditorStyles.label.richText = true;
        GUILayout.Space(12f);
        var style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, richText = true, fixedHeight = 40f };

        EditorGUILayout.BeginHorizontal();
        var menuButtonStyle = new GUIStyle(GUI.skin.button) { fixedWidth = 100f };
        if (_pageNumber == 0)
            GUI.backgroundColor = new Color32(255, 255, 0, 170);
        if (GUILayout.Button(LanguageModel.Main(_languageSelected), style: menuButtonStyle))
            _pageNumber = 0;
        GUI.backgroundColor = defaultBgColor;
        if (_pageNumber == 1)
            GUI.backgroundColor = new Color32(255, 255, 0, 170);
        if (GUILayout.Button(LanguageModel.About(_languageSelected) + $"{(_updateAvailable ? " (1)" : "")}", style: menuButtonStyle)) {
            _pageNumber = 1;
            GetContributors();
        }

        GUI.backgroundColor = defaultBgColor;
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.LabelField("<size=18><color=#9fffe3>Minty</color>Snapshot Utility</size>", style, GUILayout.ExpandWidth(true));
        EditorGUILayout.Separator();
        EditorGUILayout.Space();

        var savedValueDir = new DirectoryInfo("Assets/Minty Labs/Snapshot Utility/Saved Values/");
        if (!savedValueDir.Exists) savedValueDir.Create();
        var savedValueFile = new FileInfo(savedValueDir.FullName + "__savedValues.txt");
        if (!savedValueFile.Exists) savedValueFile.Create();

#region Page 1 (0) - Main Menu

        if (_pageNumber == 0) {
            EditorGUILayout.LabelField($"<size=15><b>{LanguageModel.GeneralOptions(_languageSelected)}</b></size>");
            _languageSelected = EditorGUILayout.Popup(LanguageModel.Language(_languageSelected), _languageSelected, _languageOptions);

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            var savedValuesBtn = new GUIStyle(GUI.skin.button) { fixedWidth = _languageSelected == 3 ? 225f : 150f };
            if (GUILayout.Button(LanguageModel.LoadSavedValues(_languageSelected), style: savedValuesBtn)) {
                var savedValues = File.ReadAllLines(savedValueFile.FullName);
                if (savedValues.Length == 0) {
                    Debug.LogError(LogPrefix + LanguageModel.SaveFileIssue(_languageSelected));
                    EditorGUILayout.HelpBox(LanguageModel.SaveFileIssue(_languageSelected), MessageType.Error);
                    return;
                }

                var versionLine = savedValues[11];
                if (!string.IsNullOrWhiteSpace(versionLine)) {
                    Debug.LogError(LogPrefix + LanguageModel.SaveFileIssue(_languageSelected));
                    EditorGUILayout.HelpBox(LanguageModel.SaveFileIssue(_languageSelected), MessageType.Error);

                    if (versionLine.Contains(SaveFileVersion)) return;
                    Debug.LogError(LogPrefix + LanguageModel.OldSaveFile(_languageSelected));
                    EditorGUILayout.HelpBox(LanguageModel.OldSaveFile(_languageSelected), MessageType.Error);
                    return;
                }

                _isTransparent = bool.Parse(savedValues[0]);
                _openFileDirectory = bool.Parse(savedValues[1]);
                _openInDefaultImageViewer = bool.Parse(savedValues[2]);
                _resolutionSelected = int.Parse(savedValues[3]);
                _standardResSelected = int.Parse(savedValues[4]);
                _resolutionMultiplier = int.Parse(savedValues[5]);
                _cameraNameFromScene = savedValues[6];
                _height = int.Parse(savedValues[7]);
                _width = int.Parse(savedValues[8]);
                var date = savedValues[10].Split(':');
                Debug.Log(LogPrefix + "Loaded Saved Values" + date[1].TrimStart(' ') + ":" + date[2] + ":" + date[3]);
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField($"<size=15><b>{LanguageModel.CameraOptions(_languageSelected)}</b></size>");
            _useSceneCamera = EditorGUILayout.ToggleLeft(LanguageModel.UseSceneCamera(_languageSelected), _useSceneCamera);
            EditorGUILayout.BeginHorizontal();
            GUI.enabled = !_useSceneCamera;
            EditorGUILayout.LabelField(LanguageModel.SelectCamera(_languageSelected));
            _camera = EditorGUILayout.ObjectField(_useSceneCamera ? SceneView.lastActiveSceneView.camera : _camera, typeof(Camera), true, null) as Camera;
            GUI.enabled = true;
            EditorGUILayout.EndHorizontal();
            _cameraNameFromScene = !_camera ? "null" : _camera.gameObject.name;

            _isTransparent = EditorGUILayout.ToggleLeft(LanguageModel.HideSkybox(_languageSelected), _isTransparent);
            var typeRect = GUILayoutUtility.GetLastRect();
            GUI.Label(new Rect(typeRect.x - 80, typeRect.y, typeRect.width, typeRect.height), new GUIContent("", LanguageModel.SkyboxTooltip(_languageSelected)));

            _openFileDirectory = EditorGUILayout.ToggleLeft(LanguageModel.OpenInExplorer(_languageSelected), _openFileDirectory);
            _openInDefaultImageViewer = EditorGUILayout.ToggleLeft(LanguageModel.OpenInViewer(_languageSelected), _openInDefaultImageViewer);

            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField($"<size=15><b>{LanguageModel.ResolutionTypeSize(_languageSelected)}</b></size>");

            EditorGUI.BeginChangeCheck();
            _resolutionSelected = EditorGUILayout.Popup(LanguageModel.ResolutionType(_languageSelected), _resolutionSelected, _resolutionOptions);
            switch (_resolutionSelected) {
                case 0:
                    _resolutionMultiplier = 1;
                    EditorGUILayout.LabelField(LanguageModel.SetOwn(_languageSelected));
                    _width = EditorGUILayout.IntField(LanguageModel.Width(_languageSelected), _width);
                    _height = EditorGUILayout.IntField(LanguageModel.Height(_languageSelected), _height);
                    if (_width > 11999 || _height > 11999) {
                        EditorGUILayout.HelpBox(LanguageModel.HighResWarning(_languageSelected), MessageType.Warning);
                        return;
                    }

                    break;
                case 1:
                    _resolutionMultiplier = 1;
                    _standardResSelected = EditorGUILayout.Popup(LanguageModel.ResPresets(_languageSelected), _standardResSelected, _standardResOptions);
                    switch (_standardResSelected) {
                        case 0: // 240p
                            _width = 320;
                            _height = 240;
                            break;
                        case 1: // 360p
                            _width = 640;
                            _height = 360;
                            break;
                        case 2: // 480p
                            _width = 720;
                            _height = 480;
                            break;
                        case 3: // 720p
                            _width = 1280;
                            _height = 720;
                            break;
                        case 4: // 1080p
                            _width = 1920;
                            _height = 1080;
                            break;
                        case 5: // 1440p
                            _width = 2560;
                            _height = 1440;
                            break;
                        case 6: // 4K
                            _width = 3840;
                            _height = 2160;
                            break;
                        case 7: // 8K
                            _width = 7680;
                            _height = 4320;
                            break;
                    }

                    break;
                case 2: // VRChat
                    _width = 1200;
                    _height = 900;
                    break;
                case 3: // ChilloutVR
                    _width = 512;
                    _height = 512;
                    break;
            }

            EditorGUI.EndChangeCheck();

            if (_resolutionSelected != 0) {
                _width = EditorGUILayout.IntField(LanguageModel.Width(_languageSelected), _width) * _resolutionMultiplier;
                _height = EditorGUILayout.IntField(LanguageModel.Height(_languageSelected), _height) * _resolutionMultiplier;
            }

            if (_resolutionSelected is 4 or 5) {
                EditorGUILayout.BeginHorizontal();
                _resolutionMultiplier = EditorGUILayout.IntSlider(label: LanguageModel.Multiplier(_languageSelected), _resolutionMultiplier, 1, 16);
                EditorGUILayout.EndHorizontal();
            }

            if (GUILayout.Button(LanguageModel.ResetValues(_languageSelected))) {
                _isTransparent = false;
                _openFileDirectory = false;
                _openInDefaultImageViewer = false;
                _resolutionSelected = 1;
                _standardResSelected = 2;
                _resolutionMultiplier = 1;
                _cameraNameFromScene = "null";
                _height = 1920;
                _width = 1080;
            }

            if (GUILayout.Button(LanguageModel.SaveValues(_languageSelected))) {
                if (!savedValueFile.Exists) savedValueFile.Create();
                var sb = new StringBuilder();
                sb.AppendLine(_isTransparent.ToString());
                sb.AppendLine(_openFileDirectory.ToString());
                sb.AppendLine(_openInDefaultImageViewer.ToString());
                sb.AppendLine(_resolutionSelected.ToString());
                sb.AppendLine(_standardResSelected.ToString());
                sb.AppendLine(_resolutionMultiplier.ToString());
                sb.AppendLine(_cameraNameFromScene);
                sb.AppendLine(_height.ToString());
                sb.AppendLine(_width.ToString());
                sb.AppendLine("\nUpdated: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                sb.Append("Version: " + SaveFileVersion);
                File.WriteAllText(savedValueFile.FullName, sb.ToString());
                Debug.Log(LogPrefix + "Saved values to file");
            }

            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField($"<size=15><b>{LanguageModel.HowToUse(_languageSelected)}</b></size>");
            EditorGUILayout.HelpBox($"  1. {LanguageModel.AddCamera(_languageSelected)}\n" +
                                    $"  2. {LanguageModel.BoxSetup(_languageSelected)}\n" +
                                    $"  3. {LanguageModel.BoxSetRes(_languageSelected)}\n" +
                                    $"  4. {LanguageModel.BoxPressButton(_languageSelected)}" +
                                    $"{(_openFileDirectory ? $"\n  5a. {LanguageModel.BoxFileExplorer(_languageSelected)}" : "")}" +
                                    $"{(_openInDefaultImageViewer ? $"\n  5b. {LanguageModel.BoxImageViewer(_languageSelected)}" : "")}", MessageType.Info);

            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField($"<size=15>{LanguageModel.ImageResolutionOutcome(_languageSelected)} {_width}x{_height}</size>");
            if (_width > 11999 || _height > 11999) {
                EditorGUILayout.HelpBox(LanguageModel.HighResWarning(_languageSelected), MessageType.Warning);
            }

            var playModeButtons = new GUIStyle(GUI.skin.button) { fixedWidth = 150f, fixedHeight = 30f };
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            EditorGUILayout.Space();
            GUI.backgroundColor = new Color32(0, 0, 255, 255);
            if (GUILayout.Button(LanguageModel.TakeSnapshotButton(_languageSelected), playModeButtons)) {
                var bytes = CaptureSnapshot(_isTransparent, _width, _height, _camera);
                if (bytes == null) return;
                var filename = ScreenshotName(_width.ToString(), _height.ToString());
                var dInfo = new DirectoryInfo("Assets/Minty Labs/Snapshot Utility/Snapshots");
                if (!dInfo.Exists) dInfo.Create();

                File.WriteAllBytes(dInfo.FullName + "\\" + filename, bytes);
                Debug.Log(LogPrefix + $"Saved to \"{filename}\"");

                if (_openFileDirectory) {
                    Process.Start(dInfo.FullName);
                    Debug.Log(LogPrefix + $"Opening \"{dInfo.FullName}\"");
                }

                if (_openInDefaultImageViewer) {
                    Application.OpenURL("file://" + dInfo.FullName + "\\" + filename);
                    Debug.Log(LogPrefix + $"Opening \"{filename}\"");
                }
            }

            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            return;
        }

#endregion

#region Page 2 (1) - About

        EditorGUILayout.LabelField($"<size=15><b>{LanguageModel.About(_languageSelected)}</b></size>");

        EditorGUILayout.LabelField($"<size=12><b>{LanguageModel.Version(_languageSelected)}:</b> <color=#EECCE0>{Version}</color>{(_updateAvailable ? $" - <color=red>{LanguageModel.UpdateAvailable(_languageSelected)}</color> - <b>v{_newVersionString}</b>" : "")}</size>");

        if (_updateAvailable) {
            EditorGUILayout.BeginHorizontal();
            GUI.enabled = false;
            if (GUILayout.Button(LanguageModel.CheckForUpdateButton(_languageSelected))) { }
            GUI.enabled = true;
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button(LanguageModel.OpenBoothPage(_languageSelected)))
                Application.OpenURL("https://mintylabs.booth.pm/items/4949097");
            // if (GUILayout.Button(LanguageModel.OpenGumroadPage(_languageSelected)))
            //     Application.OpenURL("https://mintylabs.gumroad.com/l/ScreenshotUtility");
            if (GUILayout.Button(LanguageModel.OpenGitHubPage(_languageSelected)))
                Application.OpenURL("https://github.com/Minty-Labs/Unity-Tools/releases");
        }
        else {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button(LanguageModel.CheckForUpdateButton(_languageSelected)))
                CheckForUpdate();
        }
        
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(5);
        EditorGUILayout.LabelField($"<size=12>{LanguageModel.Developer(_languageSelected)}: <color=#9fffe3>Mint</color>Lily</size>");
        EditorGUILayout.BeginHorizontal();
        GUI.backgroundColor = new Color32(0, 255, 170, 255);
        if (GUILayout.Button("Minty Labs"))
            Application.OpenURL("https://mintylabs.dev/");

        GUI.backgroundColor = new Color32(0, 0, 0, 255);
        if (GUILayout.Button("GitHub"))
            Application.OpenURL("https://github.com/MintLily");

        GUI.backgroundColor = new Color32(29, 155, 240, 255);
        if (GUILayout.Button("X (Twitter)"))
            Application.OpenURL("https://x.com/MintLiIy");
        
        GUI.backgroundColor = new Color32(0, 133, 255, 255);
        if (GUILayout.Button("Bluesky"))
            Application.OpenURL("https://bsky.app/profile/lily.mintylabs.dev");

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();

        GUI.backgroundColor = new Color32(252, 77, 80, 255);
        if (GUILayout.Button("Booth"))
            Application.OpenURL("https://mintylabs.booth.pm/");
        
        GUI.enabled = false;
        GUI.backgroundColor = new Color32(255, 144, 232, 255);
        if (GUILayout.Button("Gumroad"))
            Application.OpenURL("https://mintylabs.gumroad.com/");

        GUI.enabled = true;
        GUI.backgroundColor = new Color32(12, 14, 29, 255);
        if (GUILayout.Button("Jinxxy"))
            Application.OpenURL("https://jinxxy.com/MintLily");

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();

        GUI.backgroundColor = new Color32(0, 185, 254, 255);
        if (GUILayout.Button("Ko-fi (Donate)"))
            Application.OpenURL("https://ko-fi.com/MintLily");

        GUI.backgroundColor = new Color32(241, 101, 82, 255);
        if (GUILayout.Button("Patreon (Donate)"))
            Application.OpenURL("https://www.patreon.com/MintLily");

        EditorGUILayout.EndHorizontal();

        var content = new GUIContent((Texture)AssetDatabase.LoadAssetAtPath("Assets/Minty Labs/Snapshot Utility/tex/refresh.png", typeof(Texture)));
        EditorGUILayout.Space(10);
        GUI.backgroundColor = new Color32(0, 0, 0, 0);
        using (new EditorGUILayout.HorizontalScope()) {
            EditorGUILayout.LabelField($"<size=15><b>{LanguageModel.LanguageContributors(_languageSelected)}</b></size>");
            if (GUILayout.Button(content, new GUIStyle(GUI.skin.button) { fixedWidth = 25, fixedHeight = 25 }))
                GetContributors();
            GUILayout.FlexibleSpace();
        }

        GUI.backgroundColor = new Color32(0, 0, 0, 55);
        EditorGUILayout.BeginVertical(new GUIStyle(GUI.skin.window) { padding = new RectOffset(10, 10, 10, 10) });
        EditorGUILayout.LabelField($"<size=12><b>{LanguageModel.English(_languageSelected)} (English)</b></size>");
        EditorGUILayout.LabelField("Lily");
        EditorGUILayout.LabelField($"<size=12><b>{LanguageModel.Japanese(_languageSelected)} (日本語)</b></size>");
        EditorGUILayout.LabelField(_japaneseContributors ?? "null");
        EditorGUILayout.LabelField($"<size=12><b>{LanguageModel.Korean(_languageSelected)} (한국인)</b></size>");
        EditorGUILayout.LabelField(_koreanContributors ?? "null");
        EditorGUILayout.LabelField($"<size=12><b>{LanguageModel.Russian(_languageSelected)} (Русский)</b></size>");
        EditorGUILayout.LabelField(_russianContributors ?? "null");
        EditorGUILayout.EndVertical();

#endregion
    }

    private static byte[] CaptureSnapshot(bool isTransparent, int width, int height, Camera camera) {
        if (!camera) {
            Debug.LogError(LogPrefix + LanguageModel.NoCameraError(_languageSelected));
            EditorGUILayout.HelpBox(LanguageModel.NoCameraError(_languageSelected), MessageType.Error);
            return null;
        }
        
        var newRenderTex = new RenderTexture(width, height, (int)(isTransparent ? TextureFormat.ARGB32 : TextureFormat.RGB24));
        var currentHdrSetting = camera.allowHDR;
        var currentCamClearFlags = camera.clearFlags;
        var currentCamBgColor = camera.backgroundColor;
        
        camera.targetTexture = newRenderTex;
        camera.backgroundColor = new Color(0, 0, 0, isTransparent ? 0 : 1);
        camera.nearClipPlane = 0.01f;
        if (!currentHdrSetting)
            camera.allowHDR = true;
        
        var snapshotTexture = new Texture2D(width, height, isTransparent ? TextureFormat.ARGB32 : TextureFormat.RGB24, false);
        camera.Render();
        RenderTexture.active = newRenderTex;
        snapshotTexture.ReadPixels(new Rect(0, 0, width, height), 0, 0);

        // Reset camera settings
        camera.backgroundColor = currentCamBgColor;
        camera.clearFlags = currentCamClearFlags;
        camera.targetTexture = null;
        RenderTexture.active = null;
        camera.allowHDR = currentHdrSetting;
        DestroyImmediate(newRenderTex);

        return snapshotTexture.EncodeToPNG();
    }

    private static string ScreenshotName(string width, string height) => $"{Application.productName}_snapshot_{width}x{height}_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png";

    private static void UpdateFolderStructure() {
        var target = new DirectoryInfo("Assets/Minty Labs/Snapshot Utility/Scripts/");
        var newFolder = new DirectoryInfo("Assets/Minty Labs/Snapshot Utility/Editor/");
        if (!target.Exists) return;
        Debug.Log($"{LogPrefix}Updating folder structure...");
        target.MoveTo(newFolder.FullName);
        AssetDatabase.Refresh();
    }
}