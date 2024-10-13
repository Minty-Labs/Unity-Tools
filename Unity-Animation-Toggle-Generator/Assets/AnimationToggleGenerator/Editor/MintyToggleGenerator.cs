
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using VRC.SDK3.Avatars.Components;
using VRC.SDK3.Avatars.ScriptableObjects;

public class MintyToggleGenerator : EditorWindow {
    private const string Version = "1.0.1";
    private const string LogPrefix = "[<color=#9fffe3>MintyToggle Generator</color>] ";
    private static readonly string ProjectUserAgent = $"MintyToggleGenerator/{Version} Internal UnityWebRequest";
    private static bool _updateAvailable;
    
    private int _pageNumber;
    private static string _newVersionString;
    
    private GameObject _avatar;
    private GameObject _objectToAnimate;
    private VRCExpressionsMenu _expressionsMenu;
    private VRCExpressionParameters _expressionParameters;
    private VRCAvatarDescriptor _avatarDescriptor;
    private RuntimeAnimatorController _fxLayer;
    private string _parameterPath, _parameterName, _menuToggleText;
    public string customMenuText, customParameterName;
    private bool _isToggleEnabledDefault, _isToggleSaved = true, _isToggleSynced = true, _writeDefaultsOn, _isErrorPresent;
    private bool _editMenuText, _editParamName, _editExpressionMenu, _editExpressionParam;
    
    private readonly string[] _valueType = { "Int", "Float", "Bool" }; // This is purely for looks, to copy VRChat's SDK options
    private readonly string[] _writeDefaults = { "Auto", "ON", "OFF" };
    private int _writeDefaultsIndex;

    [MenuItem("Tools/Minty Labs/Animation Toggle Generator")]
    private static void ShowWindow() {
        CheckForUpdate();
        var eWindow = GetWindow<MintyToggleGenerator>();
        eWindow.titleContent = new GUIContent("Animation Toggle Generator");
        eWindow.minSize = new Vector2(500, 650);
        eWindow.maxSize = new Vector2(500, 850);
        eWindow.autoRepaintOnSceneChange = true;
        eWindow.Show();
    }
    
    private static void CheckForUpdate() {
        Debug.Log(LogPrefix + "Checking for updates...");
        var wc = new WebClient();
        wc.Headers.Add("User-Agent", ProjectUserAgent);
        _newVersionString = wc.DownloadString("https://raw.githubusercontent.com/Minty-Labs/Unity-Tools/refs/heads/main/Unity-Animation-Toggle-Generator/Remote/version.txt");
        _updateAvailable = _newVersionString != Version;
        Debug.Log(LogPrefix + (_updateAvailable ? "Update Available" : "No Update Available"));
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
        if (GUILayout.Button("Main", style: menuButtonStyle))
            _pageNumber = 0;
        GUI.backgroundColor = defaultBgColor;
        if (_pageNumber == 1)
            GUI.backgroundColor = new Color32(255, 255, 0, 170);
        if (GUILayout.Button("About" + $"{(_updateAvailable ? " (1)" : "")}", style: menuButtonStyle)) {
            _pageNumber = 1;
        }

        GUI.backgroundColor = defaultBgColor;
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.LabelField("<size=18><color=#9fffe3>Minty</color>Toggle Generator</size>", style, GUILayout.ExpandWidth(true));
        EditorGUILayout.Separator();
        EditorGUILayout.Space();
        
#region Page 1 (0) - Main Menu

        if (_pageNumber == 0) {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Reset")) {
                _avatar = null;
                _objectToAnimate = null;
                _expressionsMenu = null;
                _expressionParameters = null;
                _avatarDescriptor = null;
                _fxLayer = null;
                _parameterPath = "";
                _parameterName = "";
                _menuToggleText = "";
                customMenuText = "";
                customParameterName = "";
                _isToggleEnabledDefault = false;
                _isToggleSaved = true;
                _isToggleSynced = true;
                _writeDefaultsIndex = 0;
                _writeDefaultsOn = false;
                _isErrorPresent = false;
                _editMenuText = false;
                _editParamName = false;
                _editExpressionMenu = false;
                _editExpressionParam = false;
            }

            // if (!_isErrorPresent) {
            //     if (GUILayout.Button("Create Animation")) 
            //         CreateAnimations();
            // }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();
            
            _avatar = EditorGUILayout.ObjectField("Avatar Base Object", _avatar, typeof(GameObject), true) as GameObject;
            _objectToAnimate = EditorGUILayout.ObjectField("Target Object to Animate", _objectToAnimate, typeof(GameObject), true) as GameObject;
            
            // Auto-populate fields if they're null
            if (_avatar && !_avatarDescriptor)
                _avatarDescriptor = _avatar.GetComponent<VRCAvatarDescriptor>();

            if (_avatar && !_expressionsMenu && _avatarDescriptor)
                _expressionsMenu = _editExpressionMenu ? _expressionsMenu : _avatarDescriptor.expressionsMenu;

            if (_avatar && !_expressionParameters && _avatarDescriptor)
                _expressionParameters = _editExpressionParam ? _expressionParameters : _avatarDescriptor.expressionParameters;

            if (_avatar && !_fxLayer && _avatarDescriptor)
                _fxLayer = _avatarDescriptor.baseAnimationLayers[(int)VRCAvatarDescriptor.AnimLayerType.FX - 1].animatorController;
            
            EditorGUILayout.Space();
            EditorGUILayout.Space();

            GUILayout.Label("Expression Parameters Options", EditorStyles.boldLabel);
            _editParamName = EditorGUILayout.Toggle($"{(_editParamName ? "<color=#00FF00>" : "")}Edit Parameter Name{(_editParamName ? "</color>" : "")}", _editParamName);
            if (_editParamName) 
                customParameterName = EditorGUILayout.TextField("<color=#00FF00>Parameter Name</color>", customParameterName);

            GUI.enabled = false;
            EditorGUILayout.Popup("Value Type", 2, _valueType); // This is purely for looks, to copy VRChat's SDK options
            GUI.enabled = true;
            _isToggleSaved = EditorGUILayout.Toggle("Saved", _isToggleSaved);
            _isToggleEnabledDefault = EditorGUILayout.Toggle("Default Value", _isToggleEnabledDefault);
            _isToggleSynced = EditorGUILayout.Toggle("Network Synced", _isToggleSynced);
            
            EditorGUILayout.Space();
            GUILayout.Label("Animator Options", EditorStyles.boldLabel);
            // _writesDefaultOn = EditorGUILayout.Toggle($"Write Defaults [{(_writesDefaultOn ? "<color=#00ff00>ON</color>" : "<color=red>OFF</color>")}]", _writesDefaultOn);
            _writeDefaultsIndex = EditorGUILayout.Popup("Write Defaults", _writeDefaultsIndex, _writeDefaults);
            
            EditorGUILayout.Space();
            GUILayout.Label("VRC Avatar Options", EditorStyles.boldLabel);
            GUILayout.Label("Expressions Menu Options", EditorStyles.boldLabel);
            _editMenuText = EditorGUILayout.Toggle($"{(_editMenuText ? "<color=cyan>" : "")}Edit Menu Text{(_editMenuText ? "</color>" : "")}", _editMenuText);
            if (_editMenuText)
                customMenuText = EditorGUILayout.TextField("<color=cyan>Menu Text</color>", customMenuText);
            
            _editExpressionMenu = EditorGUILayout.Toggle($"{(_editExpressionMenu ? "<color=yellow>" : "")}Edit Expressions Menu{(_editExpressionMenu ? "</color>" : "")}", _editExpressionMenu);
            if (_editExpressionMenu)
                _expressionsMenu = EditorGUILayout.ObjectField("<color=yellow>VRC Expressions Menu</color>", _expressionsMenu, typeof(VRCExpressionsMenu), true) as VRCExpressionsMenu;
            // else
            //     _expressionsMenu = _avatarDescriptor.expressionsMenu;
            
            _editExpressionParam = EditorGUILayout.Toggle($"{(_editExpressionParam ? "<color=#ff8000>" : "")}Edit Expression Parameters{(_editExpressionParam ? "</color>" : "")}", _editExpressionParam);
            if (_editExpressionParam)
                _expressionParameters = EditorGUILayout.ObjectField("<color=#ff8000>VRC Expression Parameters</color>", _expressionParameters, typeof(VRCExpressionParameters), true) as VRCExpressionParameters;
            // else
            //     _expressionParameters = _avatarDescriptor.expressionParameters;
            
            EditorGUILayout.EndVertical();
            
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            
            EditorGUILayout.Space();
            GUI.enabled = !_isErrorPresent;
            var createButton = new GUIStyle(GUI.skin.button) { fixedWidth = 150f, fixedHeight = 30f };
            GUI.backgroundColor = new Color32((byte)(_isErrorPresent ? 255 : 0), 0, (byte)(_isErrorPresent ? 0 : 255), 255);
            if (GUILayout.Button("Create Animation", createButton)) 
                CreateAnimations();
            GUI.enabled = true;

            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.Space();
            GUI.backgroundColor = new Color32(0, 0, 0, 55);
            EditorGUILayout.BeginVertical(new GUIStyle(GUI.skin.window) { padding = new RectOffset(10, 10, 10, 10) });
            EditorGUILayout.LabelField("<size=15><b>Information</b> </size>");
            GUI.enabled = false;
            
            _avatarDescriptor = EditorGUILayout.ObjectField("VRC Avatar Descriptor", _avatarDescriptor, typeof(VRCAvatarDescriptor), true) as VRCAvatarDescriptor;
            _expressionsMenu = EditorGUILayout.ObjectField($"{(_editExpressionMenu ? "<color=#bdbd00>" : "")}VRC Expressions Menu{(_editExpressionMenu ? "</color>" : "")}", _expressionsMenu, typeof(VRCExpressionsMenu), true) as VRCExpressionsMenu;
            _expressionParameters = EditorGUILayout.ObjectField($"{(_editExpressionParam ? "<color=#bd5e00>" : "")}VRC Expression Parameters{(_editExpressionParam ? "</color>" : "")}", _expressionParameters, typeof(VRCExpressionParameters), true) as VRCExpressionParameters;
            _fxLayer = EditorGUILayout.ObjectField("FX Layer", _fxLayer, typeof(RuntimeAnimatorController), true) as RuntimeAnimatorController;
            
            EditorGUILayout.LabelField("Majority Write Defaults", !_fxLayer ? "<i>waiting...</i>" : (IsMajorityWriteDefaultsOn() ? "<color=#008000>ON</color>" : "<color=#ab0000>OFF</color>"));
            if (_fxLayer) {
                _writeDefaultsOn = _writeDefaultsIndex switch {
                    0 => IsMajorityWriteDefaultsOn(),
                    1 => true,
                    2 => false,
                    _ => _writeDefaultsOn
                };
            }
            EditorGUILayout.LabelField("Selected Write Defaults", _writeDefaults[_writeDefaultsIndex]);

            EditorGUILayout.LabelField("Avatar Path", !_avatar ? "<i>unset</i>" : AnimationUtility.CalculateTransformPath(_avatar.transform, null));
            EditorGUILayout.LabelField("Path of Animated Object", !_objectToAnimate ? "<i>unset</i>" : AnimationUtility.CalculateTransformPath(_objectToAnimate.transform, null));
            var avatarPath = !_avatar ? "" : AnimationUtility.CalculateTransformPath(_avatar.transform, null);
            var objectToAnimatePath = !_objectToAnimate ? "" : AnimationUtility.CalculateTransformPath(_objectToAnimate.transform, null);
            if (!string.IsNullOrEmpty(objectToAnimatePath) && !string.IsNullOrEmpty(avatarPath)) {
                var segments = objectToAnimatePath.Split('/');
                var lastSegment = segments[^1];

                lastSegment = lastSegment.Replace('_', ' ');
                var words = Regex.Split(lastSegment, @"(?<!^)(?=[A-Z])|(?<=[a-zA-Z])(?=[0-9])|_");
                lastSegment = string.Join(" ", words.Select(w => char.ToUpper(w[0]) + w[1..]));
                
                /*_parameterPath = objectToAnimatePath.Replace(avatarPath, "").TrimStart('/');
                EditorGUILayout.LabelField("Parameter Name:", _parameterPath);
                EditorGUILayout.LabelField("Toggle Name:", lastSegment);
                _parameterName = lastSegment;*/
                
                if (_editParamName) {
                    EditorGUILayout.LabelField("<color=#00bd00>Parameter Name:</color>", customParameterName);
                    _parameterPath = customParameterName;
                }
                else {
                    _parameterPath = objectToAnimatePath.Replace(avatarPath, "").TrimStart('/');
                    EditorGUILayout.LabelField("Parameter Name:", _parameterPath);
                    _parameterName = lastSegment;
                }

                if (_editMenuText) {
                    EditorGUILayout.LabelField("<color=#00bdbd>Menu Toggle Name:</color>", customMenuText);
                    _parameterName = !_editParamName ? lastSegment : customMenuText;
                }
                else {
                    EditorGUILayout.LabelField("Menu Toggle Name:", lastSegment);
                    _parameterName = lastSegment;
                }
            }
            else {
                EditorGUILayout.LabelField("Parameter Name:", "<i>unset</i>");
                EditorGUILayout.LabelField("Menu Toggle Name:", "<i>unset</i>");
            }

            EditorGUILayout.EndVertical();
            
            
            GUI.enabled = true;
            var avatarObjToAnimateMismatch = false;
            _isErrorPresent = !_objectToAnimate || _expressionParameters is null || _avatarDescriptor is null || _expressionsMenu is null || _fxLayer is null || 
                              (_editParamName && string.IsNullOrEmpty(customParameterName)) || (_editMenuText && string.IsNullOrEmpty(customMenuText));
            try {
                avatarObjToAnimateMismatch = !avatarPath.Equals(AnimationUtility.CalculateTransformPath(_objectToAnimate.transform, null).Split('/')[0]);
            }
            catch {
                avatarObjToAnimateMismatch = false;
            }
            finally {
                _isErrorPresent = _isErrorPresent || avatarObjToAnimateMismatch;
            }
                

            if (_isErrorPresent) {
                EditorGUILayout.Space();
                GUI.backgroundColor = new Color32(0, 0, 0, 55);
                EditorGUILayout.BeginVertical(new GUIStyle(GUI.skin.window) { padding = new RectOffset(10, 10, 10, 10) });
                EditorGUILayout.LabelField("<size=15><b><color=red>Errors</color></b></size>");
                
                if (_editParamName && string.IsNullOrEmpty(customParameterName))
                    EditorGUILayout.HelpBox("Parameter Name is empty.\nPlease enter a name for the parameter. Or disable the edit Parameter Name check box.", MessageType.Error);
                if (_editMenuText && string.IsNullOrEmpty(customMenuText))
                    EditorGUILayout.HelpBox("Menu Toggle Name is empty.\nPlease enter a name for the menu toggle. Or disable the edit Menu Text check box.", MessageType.Error);
                
                if (!_objectToAnimate)
                    EditorGUILayout.HelpBox("Object to Animate is not set.\nPlease set the object you want animated.", MessageType.Error);
                if (_avatarDescriptor is null)
                    EditorGUILayout.HelpBox("Avatar Descriptor is not set.\nPlease select a VRChat avatar.", MessageType.Error);
                if (_expressionParameters is null)
                    EditorGUILayout.HelpBox("Expression Parameters is not set.\nPlease add a VRC Expression Parameters", MessageType.Error);
                if (_expressionsMenu is null)
                    EditorGUILayout.HelpBox("Expressions Menu is not set.\nPlease add a VRC Expressions Menu", MessageType.Error);
                if (_fxLayer is null)
                    EditorGUILayout.HelpBox("FX Layer is not set.\nPlease add a VRC FX Layer (Animation Controller)", MessageType.Error);
                if (_avatar && _objectToAnimate) {
                    if (avatarObjToAnimateMismatch)
                        EditorGUILayout.HelpBox("Incorrect paths.\nYour avatar does not match the object path's avatar. Please make sure the object you are trying to animate is on your avatar.", MessageType.Error);
                }
                
                EditorGUILayout.EndVertical();
            }
            
            Repaint();
            return;
        }
#endregion
        
#region Page 2 (1) - About

        EditorGUILayout.LabelField("<size=15><b>About</b></size>");

        EditorGUILayout.LabelField($"<size=12><b>Version:</b> <color=#EECCE0>{Version}</color>{(_updateAvailable ? $" - <color=red>Update Available</color> - <b>v{_newVersionString}</b>" : "")}</size>");

        if (_updateAvailable) {
            EditorGUILayout.BeginHorizontal();
            GUI.enabled = false;
            if (GUILayout.Button("Check for Update")) { }
            GUI.enabled = true;
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Open Booth Page"))
                Application.OpenURL("https://mintylabs.booth.pm/items/4949097");
            if (GUILayout.Button("Open Gumroad Page"))
                Application.OpenURL("https://mintylabs.gumroad.com/l/ScreenshotUtility");
            if (GUILayout.Button("Open GitHub Page"))
                Application.OpenURL("https://github.com/Minty-Labs/Unity-Tools/releases");
        }
        else {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Check for Update"))
                CheckForUpdate();
        }
        
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(5);
        EditorGUILayout.LabelField("<size=12>Developer: <color=#9fffe3>Mint</color>Lily</size>");
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

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();

        GUI.backgroundColor = new Color32(252, 77, 80, 255);
        if (GUILayout.Button("Booth"))
            Application.OpenURL("https://mintylabs.booth.pm/");

        GUI.backgroundColor = new Color32(255, 144, 232, 255);
        if (GUILayout.Button("Gumroad"))
            Application.OpenURL("https://mintylabs.gumroad.com/");

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

        
        EditorGUILayout.Space(10);
        GUI.backgroundColor = new Color32(0, 0, 0, 55);
        EditorGUILayout.LabelField("<size=15><b>Note</b></size>");
        EditorGUILayout.BeginVertical(new GUIStyle(GUI.skin.window) { padding = new RectOffset(10, 10, 10, 10) });
        EditorGUILayout.LabelField("This project was inspired by Ahriana's Project (AhrianaDev)");
        EditorGUILayout.LabelField("This is aimed to be a more user-friendly, yet informative version of the original project.");
        EditorGUILayout.LabelField("<size=15> </size>");
        EditorGUILayout.LabelField("<i>This project is not affiliated with AhrianaDev.</i>");
        EditorGUILayout.EndVertical();

#endregion
    }
    
    public void OnInspectorUpdate() => Repaint();

    private void CreateAnimations() {
        // Create Animation Clips
        var animationOn = CreateAnimation("on", 0, 1, 0, 0);
        var animationOff = CreateAnimation("off", 0, 0, 0, 1);

        var currentFolder = GetSelectedPathOrFallback();
        var on = $"{currentFolder}/{animationOn.name}";
        var off = $"{currentFolder}/{animationOff.name}";
        AssetDatabase.CreateAsset(animationOn, on);
        AssetDatabase.CreateAsset(animationOff, off);
        
        // Create Parameters
        var newParam = new VRCExpressionParameters.Parameter {
            name = _parameterPath,
            valueType = VRCExpressionParameters.ValueType.Bool,
            saved = _isToggleSaved,
            defaultValue = _isToggleEnabledDefault ? 1f : 0f,
            networkSynced = _isToggleSynced
        };
        
        // Array.Resize never seems to work for me as of Unity 2022.3+, so I'm using a List instead.
        var paramAsList = _expressionParameters.parameters.ToList();
        paramAsList.Add(newParam);
        _expressionParameters.parameters = paramAsList.ToArray();
        
        // Create Menu Controls
        var control = new VRCExpressionsMenu.Control {
            name = _editMenuText ? customMenuText : _parameterName,
            icon = null,
            type = VRCExpressionsMenu.Control.ControlType.Toggle,
            parameter = new VRCExpressionsMenu.Control.Parameter {
                name = _parameterPath
            }
        };
        _expressionsMenu.controls.Add(control);
        
        // Create FX Layer
        CreateAnimatorLayer(animationOn, animationOff);
        
        // Save Changes
        EditorUtility.SetDirty(_expressionsMenu);
        EditorUtility.SetDirty(_expressionParameters);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        EditorUtility.DisplayDialog("Animation Toggle Generator", $"Created animation files.\nSaved to\n{on}\n{off}", "Close");
    }

    private AnimationClip CreateAnimation(string animName, float startTime, float startValue, float endTime, float endValue) {
        var clipOn = new AnimationClip {
            name = $"{_parameterName}_{animName}.anim"
        };

        var curve = AnimationCurve.Linear(startTime, startValue, endTime, endValue);
        var binding = new EditorCurveBinding {
            path = AnimationUtility.CalculateTransformPath(_objectToAnimate.transform, _avatar.transform),
            type = typeof(GameObject),
            propertyName = "m_IsActive"
        };
        AnimationUtility.SetEditorCurve(clipOn, binding, curve);

        return clipOn;
    }

    private void CreateAnimatorLayer(AnimationClip enabledClip, AnimationClip disabledClip) {
        // Load controller
        var animatorController = _fxLayer as AnimatorController;
        if (!animatorController) {
            Debug.LogError(LogPrefix + "FX Layer is not an Animator Controller.");
            EditorUtility.DisplayDialog("Animation Toggle Generator", "FX Layer is not an Animator Controller or FX Layer became null/missing.", "Close");
            return;
        }

        // Create new layer
        var layer = new AnimatorControllerLayer {
            name = _parameterPath,
            defaultWeight = 1
        };

        var animatorStateMachine = new AnimatorStateMachine();
        layer.stateMachine = animatorStateMachine;

        AssetDatabase.AddObjectToAsset(layer.stateMachine, AssetDatabase.GetAssetPath(animatorController));
        layer.stateMachine.hideFlags = HideFlags.HideInHierarchy;

        // Create Parameters
        var parameters = animatorController.parameters;
        var newParameter = new AnimatorControllerParameter {
            name = _parameterPath,
            type = AnimatorControllerParameterType.Bool
        };
        ArrayUtility.Add(ref parameters, newParameter);
        animatorController.parameters = parameters;

        // Create enabled state
        var enabledState = new AnimatorState {
            name = "Enabled",
            motion = enabledClip,
            writeDefaultValues = _writeDefaultsOn
        };

        // Create disabled state
        var disabledState = new AnimatorState {
            name = "Disabled",
            motion = disabledClip,
            writeDefaultValues = _writeDefaultsOn
        };

        // Add a transition from enabled to the disabled state
        var transition = new AnimatorStateTransition {
            destinationState = disabledState,
            duration = 0f,
            exitTime = 0,
            hasExitTime = false
        };

        enabledState.AddTransition(transition);
        AssetDatabase.AddObjectToAsset(transition, AssetDatabase.GetAssetPath(animatorController));
        transition.hideFlags = HideFlags.HideInHierarchy;

        transition.AddCondition(AnimatorConditionMode.IfNot, 1f, _parameterPath);

        // Add a transition from disabled to the enabled state
        var transition2 = new AnimatorStateTransition {
            destinationState = enabledState,
            duration = 0f,
            exitTime = 0,
            hasExitTime = false
        };

        disabledState.AddTransition(transition2);
        AssetDatabase.AddObjectToAsset(transition2, AssetDatabase.GetAssetPath(animatorController));
        transition2.hideFlags = HideFlags.HideInHierarchy;

        transition2.AddCondition(AnimatorConditionMode.If, 1f, _parameterPath);

        // Add the states to the state machine
        layer.stateMachine.AddState(enabledState, new Vector3(300, 10, 0));
        AssetDatabase.AddObjectToAsset(enabledState, AssetDatabase.GetAssetPath(animatorController));
        enabledState.hideFlags = HideFlags.HideInHierarchy;

        layer.stateMachine.AddState(disabledState, new Vector3(300, 150, 0));
        AssetDatabase.AddObjectToAsset(disabledState, AssetDatabase.GetAssetPath(animatorController));
        enabledState.hideFlags = HideFlags.HideInHierarchy;

        layer.stateMachine.defaultState = disabledState;
        
        // Add the layer to the controller
        animatorController.AddLayer(layer);
        EditorUtility.SetDirty(animatorController);
    }
    
    // Allows you get the current folder that is opened in the project window
    private static string GetSelectedPathOrFallback() {
        var path = "Assets";
        
        foreach (var obj in Selection.GetFiltered(typeof(Object), SelectionMode.Assets)) {
            path = AssetDatabase.GetAssetPath(obj);
            if (string.IsNullOrEmpty(path) || !File.Exists(path)) continue;
            path = Path.GetDirectoryName(path);
            break;
        }
        return path;
    }

    private bool IsMajorityWriteDefaultsOn() {
        int on = 0, off = 0;
        foreach (var layer in (_fxLayer as AnimatorController)!.layers) {
            foreach (var state in layer.stateMachine.states) {
                if (state.state.writeDefaultValues)
                    on++;
                else
                    off++;
            }
        }
        
        return on > off;
    }
}
