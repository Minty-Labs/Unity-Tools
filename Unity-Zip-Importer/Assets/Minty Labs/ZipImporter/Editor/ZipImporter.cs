using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using UnityEditor;
using UnityEngine;

public class ZipImporter : EditorWindow {
    private const string Version = "1.0.0";
    private const string LogPrefix = "[<color=#9fffe3>ZipArchive Importer</color>] ";
    private static readonly string ProjectUserAgent = $"ZipArchive Importer/{Version} Internal UnityWebRequest";
    private int _pageNumber;
    private static bool _updateAvailable;

    private ZipArchive _zipArchive;
    private List<string> _zipContents;
    private FileStream _zipFileStream;
    private List<bool> _importingCheckBoxes, _interactiveCheckBoxes;
    private static Queue<Action> _importQueue;
    private static string _lastDir = "", _newVersionString = "", _unityPackageFileName = "";
    private string _currentFilePath;
    private static bool _doImport = true, _activelyImporting, _importingUnityPackage, _doingFakeImportForTestingOrDevelopmentPurposes;
    private Vector2 _scrollView = Vector2.zero;

    private static void CheckForUpdate() {
        Debug.Log(LogPrefix + "Checking for update...");
        try {
            var wc = new WebClient();
            wc.Headers.Add("User-Agent", ProjectUserAgent);
            _newVersionString = wc.DownloadString("https://raw.githubusercontent.com/Minty-Labs/Unity-Tools/main/Unity-Zip-Importer/Remote/version.txt");
            _updateAvailable = _newVersionString != Version;
            Debug.Log(LogPrefix + (_updateAvailable ? "Update Available" : "No Update Available"));
            wc.Dispose();
        }
        catch (Exception ex) {
            Debug.Log(LogPrefix + $"Failed to check for update\n<color=red>{ex.Message}</color>\n<color=red>{ex.StackTrace}</color>");
        }
    }

    [MenuItem("Tools/Minty Labs/Zip Importer")]
    private static void ShowWindow() {
        CheckForUpdate();
        var eWindow = GetWindow<ZipImporter>("Zip Importer");
        eWindow.minSize = new Vector2(500, 450);
        eWindow.autoRepaintOnSceneChange = true;
        eWindow.Show();
    }

    private void OnGUI() {
        var defaultBgColor = GUI.backgroundColor;
        EditorStyles.label.richText = true;
        GUILayout.Space(12f);
        var style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, richText = true, fixedHeight = 40f };
        var importBtn = new GUIStyle(GUI.skin.button) { fixedHeight = 30f };

        EditorGUILayout.BeginHorizontal();
        var menuButtonStyle = new GUIStyle(GUI.skin.button) { fixedWidth = 100f };
        if (_pageNumber == 0)
            GUI.backgroundColor = new Color32(255, 255, 0, 170);
        if (GUILayout.Button("Main", style: menuButtonStyle)) {
            _pageNumber = 0;
            OnDestroy();
        }
        GUI.backgroundColor = defaultBgColor;
        if (_pageNumber == 1)
            GUI.backgroundColor = new Color32(255, 255, 0, 170);
        if (GUILayout.Button($"About{(_updateAvailable ? " (1)" : "")}", style: menuButtonStyle))
            _pageNumber = 1;
        GUI.backgroundColor = defaultBgColor;

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.LabelField("<size=18><color=#9fffe3>Minty</color>ZipArchive Importer</size>", style, GUILayout.ExpandWidth(true));
        EditorGUILayout.Separator();
        EditorGUILayout.Space();

        #region Page 1 (0) - Main Menu

        if (_pageNumber == 0) {
            if (!_activelyImporting) {
                EditorGUILayout.Space(50);
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.Separator();
                var savedValuesBtn = new GUIStyle(GUI.skin.button) { fixedWidth = 150f, fixedHeight = 30f };
                GUI.backgroundColor = new Color32(0, 255, 0, 170);
                if (GUILayout.Button("Import Package", style: savedValuesBtn)) {
                    OnDestroy();
                    Debug.Log("Importing Package Button Press");
                    ImportWithGui();
                }
                EditorGUILayout.Separator();
                EditorGUILayout.EndHorizontal();

                GUI.backgroundColor = defaultBgColor;
                /*
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("Fake Import Zip Package", style: importBtn)) {
                    OnDestroy();
                    Debug.Log("[DEBUG] Emulating Import Package Button Press");
                    _activelyImporting = true;
                    _importingUnityPackage = false;
                    _doingFakeImportForTestingOrDevelopmentPurposes = true;
                    var window = GetWindow<ZipImporter>("Zip Importer");
                    var file = new FileInfo("Assets/../_test/Larger File Test.zip");if (!file.Exists)
                        Debug.LogError("File does not exist");
                    window.UpdateGui(file.FullName);
                    window.Show();
                }
                if (GUILayout.Button("Fake Import Unity Package", style: importBtn)) {
                    OnDestroy();
                    Debug.Log("[DEBUG] Emulating Import Unity Package Button Press");
                    _activelyImporting = true;
                    _importingUnityPackage = true;
                    _doingFakeImportForTestingOrDevelopmentPurposes = true;
                    _unityPackageFileName = "poi_Pro_9.0.45.unitypackage";
                    var window = GetWindow<ZipImporter>("Zip Importer");
                    var file = new FileInfo("Assets/../_test/poi_Pro_9.0.45.unitypackage");
                    if (!file.Exists)
                        Debug.LogError("File does not exist");
                    window.UpdateGui(file.FullName, true);
                    window.Show();
                }
                
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space(20);
                */
            }
            else {
                if (_importingUnityPackage) {
                    GUILayout.Space(12f);
                    EditorGUILayout.LabelField($"<size=15><b>Importing Unity Package</b>{(_doingFakeImportForTestingOrDevelopmentPurposes ? " (TEST)" : "")}</size>");
                    GUI.backgroundColor = new Color32(0, 0, 0, 55);
                    EditorGUILayout.BeginVertical(new GUIStyle(GUI.skin.window) { fixedHeight = 40f, padding = new RectOffset(10, 10, 10, 10) });
                    EditorGUILayout.LabelField($"<color=yellow>{_unityPackageFileName}</color>");
                    /*
                    EditorGUILayout.Separator();
                    if (_activelyImporting && _importingUnityPackage) {
                        GUILayout.Space(12f);
                        GUI.backgroundColor = new Color32(255, 0, 0, 170);
                        if (GUILayout.Button("Cancel", style: importBtn))
                            OnDestroy();
                    }
                    */
                    EditorGUILayout.EndVertical();
                    GUI.backgroundColor = defaultBgColor;
                    return;
                }
                
                EditorGUILayout.LabelField("<size=15><b>Information</b></size>");
                EditorGUILayout.HelpBox("  If \"Import\" is checked, the package will be imported\n" +
                                        "  If \"Interactive\" is checked, the unity window with the package contents will show for each package", MessageType.Info);

                EditorGUILayout.Space(10);

                EditorGUILayout.LabelField($"<size=15><b>File</b>{(_doingFakeImportForTestingOrDevelopmentPurposes ? " (TEST)" : "")}</size>");
                GUI.backgroundColor = new Color32(0, 0, 0, 55);
                EditorGUILayout.BeginVertical(new GUIStyle(GUI.skin.window) { fixedHeight = 40f, padding = new RectOffset(10, 10, 10, 10) });
                EditorGUILayout.LabelField($"<color=yellow>{_currentFilePath}</color>");
                EditorGUILayout.EndVertical();
                GUI.backgroundColor = defaultBgColor;

                GUILayout.Space(1f);
                using (new EditorGUILayout.HorizontalScope()) {
                    EditorGUILayout.LabelField("Import", GUILayout.Width(60));
                    EditorGUILayout.LabelField("Interactive", GUILayout.Width(80));
                    EditorGUILayout.LabelField("File Name");
                }

                _scrollView = EditorGUILayout.BeginScrollView(_scrollView);
                for (var i = 0; i < _zipContents.Count; i++) {
                    using (new EditorGUILayout.HorizontalScope()) {
                        _importingCheckBoxes[i] = EditorGUILayout.Toggle(_importingCheckBoxes[i], GUILayout.Width(60));
                        _interactiveCheckBoxes[i] = EditorGUILayout.Toggle(_interactiveCheckBoxes[i], GUILayout.Width(80));
                        EditorGUILayout.LabelField(_zipContents[i]);
                    }
                }

                EditorGUILayout.BeginHorizontal();
                var toggleBtn = new GUIStyle(GUI.skin.button) { fixedWidth = 60f };
                using (new EditorGUILayout.VerticalScope(options: GUILayout.Width(50))) {
                    if (GUILayout.Button("All", style: toggleBtn))
                        _importingCheckBoxes = _importingCheckBoxes.Select(_ => true).ToList();
                    if (GUILayout.Button("None", style: toggleBtn))
                        _importingCheckBoxes = _importingCheckBoxes.Select(_ => false).ToList();
                }

                using (new EditorGUILayout.VerticalScope(options: GUILayout.Width(50))) {
                    if (GUILayout.Button("All", style: toggleBtn))
                        _interactiveCheckBoxes = _interactiveCheckBoxes.Select(_ => true).ToList();
                    if (GUILayout.Button("None", style: toggleBtn))
                        _interactiveCheckBoxes = _interactiveCheckBoxes.Select(_ => false).ToList();
                }

                EditorGUILayout.EndHorizontal();
                
                EditorGUILayout.EndScrollView();
                
                EditorGUILayout.BeginHorizontal();
                GUI.backgroundColor = new Color32(0, 0, 255, 170);
                if (GUILayout.Button("Import", style: importBtn)) {
                    if (_importQueue == null) _doImport = true;
                    _importQueue = new Queue<Action>();
                    for (var i = 0; i < _zipContents.Count; i++) {
                        var flag = _interactiveCheckBoxes[i];
                        if (!_importingCheckBoxes[i] || !flag) continue;
                        var zipEntry = _zipArchive.Entries.Single(entry => entry.FullName == _zipContents[i]);
                        _importQueue.Enqueue(() => OpenZip(zipEntry.FullName, flag, zipEntry));
                    }
                    for (var i = 0; i < _zipContents.Count; i++) {
                        var flag = _interactiveCheckBoxes[i];
                        if (!_importingCheckBoxes[i] || flag) continue;
                        var zipEntry = _zipArchive.Entries.Single(entry => entry.FullName == _zipContents[i]);
                        _importQueue.Enqueue(() => OpenZip(zipEntry.FullName, flag, zipEntry));
                    }
                }

                GUI.backgroundColor = new Color32(255, 0, 0, 170);
                if (GUILayout.Button("Cancel", style: importBtn))
                    OnDestroy();

                EditorGUILayout.EndHorizontal();

                DoNextImport();
            }
            return;
        }


        #endregion

        #region Page 2 (1) - About

        //if (_pageNumber == 1) return;
        EditorGUILayout.LabelField("<size=15><b>About</b></size>");

        EditorGUILayout.LabelField($"<size=12><b>Version:</b> <color=#EECCE0>{Version}</color>{(_updateAvailable ? $" - <color=red>Update Available</color> - v{_newVersionString}" : "")}</size>");
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Check For Update"))
            CheckForUpdate();

        if (_updateAvailable) {
            if (GUILayout.Button("Open Booth Page"))
                Application.OpenURL("https://mintylabs.booth.pm/items/5527952");
            if (GUILayout.Button("Open GitHub Releases Page"))
                Application.OpenURL("https://github.com/Minty-Labs/Unity-Tools/releases");
        }

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(5);
        EditorGUILayout.LabelField("<size=12>Developer: <color=#9fffe3>Mint</color>Lily</size>");
        EditorGUILayout.BeginHorizontal();
        GUI.backgroundColor = new Color32(0, 255, 170, 255);
        if (GUILayout.Button("Minty Labs"))
            Application.OpenURL("https://mintylabs.dev/");
        GUI.backgroundColor = new Color32(252, 77, 80, 255);
        if (GUILayout.Button("Booth"))
            Application.OpenURL("https://mintylabs.booth.pm/");
        GUI.backgroundColor = new Color32(29, 155, 240, 255);
        if (GUILayout.Button("X (Twitter)"))
            Application.OpenURL("https://x.com/MintLiIy");
        GUI.backgroundColor = new Color32(0, 0, 0, 255);
        if (GUILayout.Button("GitHub"))
            Application.OpenURL("https://github.com/MintLily");
        GUI.backgroundColor = new Color32(0, 185, 254, 255);
        if (GUILayout.Button("Ko-fi (Donate)"))
            Application.OpenURL("https://ko-fi.com/MintLily");
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(10);
        GUI.backgroundColor = new Color32(0, 0, 0, 55);
        EditorGUILayout.LabelField("<size=15><b>Note</b></size>");
        EditorGUILayout.BeginVertical(new GUIStyle(GUI.skin.window) { padding = new RectOffset(10, 10, 10, 10) });
        EditorGUILayout.LabelField("This project was inspired by Goze's Project (https://goze.booth.pm/items/4571505)");
        EditorGUILayout.LabelField("Download their project, if you don\'t want all the bells and whistles this project offers.");
        EditorGUILayout.LabelField("<size=15> </size>");
        EditorGUILayout.LabelField("<size=15>Local Cuties</size>");
        EditorGUILayout.LabelField("Elly, Penny, Emily, Myrkur");
        EditorGUILayout.EndVertical();

        #endregion
    }

    private static void OpenZip(string name, bool interactive, ZipArchiveEntry archiveEntry) {
        // Open Zip
        var archiveStream = archiveEntry.Open();
        // Get Path
        var tmpPath = Application.temporaryCachePath;
        // Trys to get the full completed path
        var tryGetPath = Path.Combine(tmpPath, Path.GetFileName(name));
        while (File.Exists(tryGetPath))
            tryGetPath = Path.Combine(tmpPath, DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + Path.GetFileName(name));

        // Creates and writes the file
        using (var fileStream = new FileStream(tryGetPath, FileMode.Create))
            archiveStream.CopyTo(fileStream);
        archiveStream.Dispose();

        // Import Package
        AssetDatabase.ImportPackage(tryGetPath, interactive);
        // Delete File
        File.Delete(tryGetPath);
    }

    private void UpdateGui(string filePath, bool onlyUnityPackageImport = false) {
        // Get Path
        _currentFilePath = filePath;
        if (onlyUnityPackageImport) return; // pretty much only used for testing
        // Open Zip
        _zipFileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        _zipArchive = new ZipArchive(_zipFileStream);
        // Get Zip File Contents
        _zipContents = _zipArchive.Entries.Where(contents => Path.GetExtension(contents.FullName) == ".unitypackage").Select(file => file.FullName).ToList();
        // Set Check Boxes
        _interactiveCheckBoxes = _zipContents.Select(_ => true).ToList();
        _importingCheckBoxes = _zipContents.Select(_ => true).ToList();
    }

    private void OnDestroy() {
        _zipArchive?.Dispose();
        _zipFileStream?.Dispose();
        _importQueue?.Clear();
        _activelyImporting = false;
        _importingUnityPackage = false;
        _unityPackageFileName = "";
        _doImport = true;
    }

    [MenuItem("Assets/Import Package/Custom Package as Zip - with GUI")]
    public static void ImportWithGui() {
        var filePath = GetFilePath();
        if (filePath.Length == 0) return;

        switch (Path.GetExtension(filePath)) {
            case ".zip":
                _activelyImporting = true;
                _importingUnityPackage = false;
                var window = GetWindow<ZipImporter>("Zip Importer");
                window.UpdateGui(filePath);
                window.Show();
                break;
            case ".unitypackage":
                _activelyImporting = true;
                _importingUnityPackage = true;
                _unityPackageFileName = Path.GetFileName(filePath);
                AssetDatabase.ImportPackage(filePath, true);
                break;
        }
    }

    [MenuItem("Assets/Import Package/Custom Package as Zip - Auto Import Everything")]
    public static void ImportWithNoGui() => CreateInstance<ZipImporter>().Import(interactive: false);

    private static string GetFilePath() {
        var filePath = EditorUtility.OpenFilePanel("Select File", _lastDir, "unitypackage,zip");
        _lastDir = Path.GetDirectoryName(filePath);
        return string.IsNullOrEmpty(filePath) ? "" : filePath;
    }

    private void Import(bool interactive) {
        var filePath = GetFilePath();
        if (filePath.Length == 0) return;

        switch (Path.GetExtension(filePath)) {
            case ".zip":
                _activelyImporting = true;
                _importingUnityPackage = false;
                using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read)) {
                    using (var archive = new ZipArchive(fileStream)) {
                        archive.Entries.Where(entry => Path.GetExtension(entry.FullName) == ".unitypackage").ToList().ForEach(entry =>
                            OpenZip(entry.FullName, interactive, entry));
                    }
                }
                break;
            case ".unitypackage":
                _activelyImporting = true;
                _importingUnityPackage = true;
                _unityPackageFileName = Path.GetFileName(filePath);
                AssetDatabase.ImportPackage(filePath, interactive);
                break;
        }
    }

    private void EventFailureMethod(string name, string msg) => ImportEndEvent(name);

    private void ImportEndEvent(string nameOfPackage) {
        AssetDatabase.importPackageCompleted -= ImportEndEvent;
        AssetDatabase.importPackageCancelled -= ImportEndEvent;
        AssetDatabase.importPackageFailed -= EventFailureMethod;
        _doImport = true;
        EditorApplication.delayCall += DoNextImport;
    }

    private void DoNextImport() {
        if (!_doImport) return;
        if (_importQueue?.Count == 0) {
            OnDestroy();
            return;
        }

        AssetDatabase.importPackageCompleted += ImportEndEvent;
        AssetDatabase.importPackageCancelled += ImportEndEvent;
        AssetDatabase.importPackageFailed += EventFailureMethod;
        _doImport = false;
        _importQueue?.Dequeue()();
    }
}