#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class PoiyomiUpgrader : EditorWindow {

    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Auto-detect", false)]
    private static void PlaceholderMethod() => Debug.LogWarning($"{LoggerPrefix} This does not yet work.");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Auto-detect", true)]
    private static bool ValidatePlaceholderMethod() => false;
    
#region Pro

#region 7.3

    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/7.3/\u2605 Poiyomi Pro \u2605      \u279C      .poiyomi \u2215 Poiyomi Pro", false)]
    private static void UpgradePro73() => UpgradeShader("7.3", "\u2605 Poiyomi Pro \u2605", "Pro");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/7.3/\u2605 Poiyomi Pro \u2605      \u279C      .poiyomi \u2215 Poiyomi Pro", true)]
    private static bool ValidateUpgradePro73() => ValidateMethod(); // ValidateMethod("7.3", "\u2605 Poiyomi Pro \u2605");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/7.3/\u2605 Pro Grabpass \u2605      \u279C      .poiyomi \u2215 Poiyomi Pro Grab Pass", false)]
    private static void UpgradeGrabPassPro73() => UpgradeShader("7.3", "\u2605 Pro Grabpass \u2605", "Pro Grab Pass");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/7.3/\u2605 Pro Grabpass \u2605      \u279C      .poiyomi \u2215 Poiyomi Pro Grab Pass", true)]
    private static bool ValidateUpgradeGrabPassPro73() => ValidateMethod(); // ValidateMethod("7.3", "\u2605 Pro Grabpass \u2605");

#endregion

#region 8.1

    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/8.1/Poiyomi Pro", false)]
    private static void UpgradePro81() => UpgradeShader("8.1", "Poiyomi Pro");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/8.1/Poiyomi Pro", true)]
    private static bool ValidateUpgradePro81() => ValidateMethod(); // ValidateMethod("8.1", "Poiyomi Pro");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/8.1/Poiyomi Pro Early Z      \u279C      .poiyomi \u2215 Poiyomi Pro", false)]
    private static void UpgradeEarlyZPro81() => UpgradeShader("8.1", "Pro Early Z", "Pro");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/8.1/Poiyomi Pro Early Z      \u279C      .poiyomi \u2215 Poiyomi Pro", true)]
    private static bool ValidateUpgradeEarlyZPro81() => ValidateMethod(); // ValidateMethod("8.1", "Pro Early Z");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/8.1/Poiyomi Pro Early Z Outline      \u279C      .poiyomi \u2215 Poiyomi Pro Outline Early", false)]
    private static void UpgradeEarlyOutlineZPro81() => UpgradeShader("8.1", "Pro Early Z Outline", "Pro Outline Early");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/8.1/Poiyomi Pro Early Z Outline      \u279C      .poiyomi \u2215 Poiyomi Pro Outline Early", true)]
    private static bool ValidateUpgradeEarlyOutlineZPro81() => ValidateMethod(); // ValidateMethod("8.1", "Pro Early Z Outline");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/8.1/Poiyomi Pro Fur", false)]
    private static void UpgradeFurPro81() => UpgradeShader("8.1", "Pro Fur");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/8.1/Poiyomi Pro Fur", true)]
    private static bool ValidateUpgradeFurPro81() => ValidateMethod(); // ValidateMethod("8.1", "Pro Fur");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/8.1/Poiyomi Pro Geometric Dissolve", false)]
    private static void UpgradeGeometricDissolvePro81() => UpgradeShader("8.1", "Pro Geometric Dissolve");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/8.1/Poiyomi Pro Geometric Dissolve", true)]
    private static bool ValidateUpgradeGeometricDissolvePro81() => ValidateMethod(); // ValidateMethod("8.1", "Pro Geometric Dissolve");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/8.1/Poiyomi Pro Grab Pass", false)]
    private static void UpgradeGrabPassPro81() => UpgradeShader("8.1", "Pro Grab Pass");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/8.1/Poiyomi Pro Grab Pass", true)]
    private static bool ValidateUpgradeGrabPassPro81() => ValidateMethod(); // ValidateMethod("8.1", "Pro Grab Pass");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/8.1/Poiyomi Pro Outline      \u279C      .poiyomi \u2215 Poiyomi Pro", false)]
    private static void UpgradeOutlinePro81() => UpgradeShader("8.1", "Pro Outline", "Pro");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/8.1/Poiyomi Pro Outline      \u279C      .poiyomi \u2215 Poiyomi Pro", true)]
    private static bool ValidateUpgradeOutlinePro81() => ValidateMethod(); // ValidateMethod("8.1", "Pro Outline");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/8.1/Poiyomi Pro Outline Early      \u279C      .poiyomi \u2215 Poiyomi Pro outline Early", false)]
    private static void UpgradeOutlineEarlyPro81() => UpgradeShader("8.1", "Pro Outline Early", "Pro Outline Early");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/8.1/Poiyomi Pro Outline Early      \u279C      .poiyomi \u2215 Poiyomi Pro Outline Early", true)]
    private static bool ValidateUpgradeOutlineEarlyPro81() => ValidateMethod(); // ValidateMethod("8.1", "Pro Outline Early");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/8.1/Poiyomi Pro ShatterWave", false)]
    private static void UpgradeShatterwavePro81() => UpgradeShader("8.1", "Pro ShatterWave");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/8.1/Poiyomi Pro ShatterWave", true)]
    private static bool ValidateUpgradeShatterwavePro81() => ValidateMethod(); // ValidateMethod("8.1", "Pro ShatterWave");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/8.1/Poiyomi Pro Wireframe", false)]
    private static void UpgradeWireframePro81() => UpgradeShader("8.1", "Pro Wireframe");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/8.1/Poiyomi Pro Wireframe", true)]
    private static bool ValidateUpgradeWireframePro81() => ValidateMethod(); // ValidateMethod("8.1", "Pro Wireframe");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/8.1/Poiyomi Pro World", false)]
    private static void UpgradeWorldPro81() => UpgradeShader("8.1", "Pro World");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/8.1/Poiyomi Pro World", true)]
    private static bool ValidateUpgradeWorldPro81() => ValidateMethod(); // ValidateMethod("8.1", "Pro World");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/8.1/Poiyomi Pro World Fur", false)]
    private static void UpgradeWorldFurPro81() => UpgradeShader("8.1", "Pro World Fur");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/8.1/Poiyomi Pro World Fur", true)]
    private static bool ValidateUpgradeWorldFurPro81() => ValidateMethod(); // ValidateMethod("8.1", "Pro World Fur");
    
#endregion

#region 8.2

    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/8.2/Poiyomi Pro", false)]
    private static void UpgradePro82() => UpgradeShader("8.2", "Pro");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/8.2/Poiyomi Pro", true)]
    private static bool ValidateUpgradePro82() => ValidateMethod(); // ValidateMethod("8.2", "Pro");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/8.2/Poiyomi Pro Early Z      \u279C      .poiyomi \u2215 Poiyomi Pro", false)]
    private static void UpgradeEarlyZPro82() => UpgradeShader("8.2", "Pro Early Z", "Pro");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/8.2/Poiyomi Pro Early Z      \u279C      .poiyomi \u2215 Poiyomi Pro", true)]
    private static bool ValidateUpgradeEarlyZPro82() => ValidateMethod(); // ValidateMethod("8.2", "Pro Early Z");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/8.2/Poiyomi Pro Early Z Outline      \u279C      .poiyomi \u2215 Poiyomi Pro Outline Early", false)]
    private static void UpgradeEarlyOutlineZPro82() => UpgradeShader("8.2", "Pro Early Z Outline", "Pro Outline Early");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/8.2/Poiyomi Pro Early Z Outline      \u279C      .poiyomi \u2215 Poiyomi Pro Outline Early", true)]
    private static bool ValidateUpgradeEarlyOutlineZPro82() => ValidateMethod(); // ValidateMethod("8.2", "Pro Early Z Outline");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/8.2/Poiyomi Pro Fur", false)]
    private static void UpgradeFurPro82() => UpgradeShader("8.2", "Pro Fur");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/8.2/Poiyomi Pro Fur", true)]
    private static bool ValidateUpgradeFurPro82() => ValidateMethod(); // ValidateMethod("8.2", "Pro Fur");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/8.2/Poiyomi Pro Geometric Dissolve", false)]
    private static void UpgradeGeometricDissolvePro82() => UpgradeShader("8.2", "Pro Geometric Dissolve");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/8.2/Poiyomi Pro Geometric Dissolve", true)]
    private static bool ValidateUpgradeGeometricDissolvePro82() => ValidateMethod(); // ValidateMethod("8.2", "Pro Geometric Dissolve");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/8.2/Poiyomi Pro Geometric Dissolve Grab Pass      \u279C      .poiyomi \u2215 Poiyomi Pro Geometric Dissolve", false)]
    private static void UpgradeGeometricDissolveGrabPassPro82() => UpgradeShader("8.2", "Pro Geometric Dissolve", "Pro Geometric Dissolve");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/8.2/Poiyomi Pro Geometric Dissolve Grab Pass      \u279C      .poiyomi \u2215 Poiyomi Pro Geometric Dissolve", true)]
    private static bool ValidateUpgradeGeometricDissolveGrabPassPro82() => ValidateMethod(); // ValidateMethod("8.2", "Pro Geometric Dissolve");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/8.2/Poiyomi Pro Grab Pass", false)]
    private static void UpgradeGrabPassPro82() => UpgradeShader("8.2", "Pro Grab Pass");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/8.2/Poiyomi Pro Grab Pass", true)]
    private static bool ValidateUpgradeGrabPassPro82() => ValidateMethod(); // ValidateMethod("8.2", "Pro Grab Pass");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/8.2/Poiyomi Pro Outline      \u279C      .poiyomi \u2215 Poiyomi Pro", false)]
    private static void UpgradeOutlinePro82() => UpgradeShader("8.2", "Pro Outline", "Pro");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/8.2/Poiyomi Pro Outline      \u279C      .poiyomi \u2215 Poiyomi Pro", true)]
    private static bool ValidateUpgradeOutlinePro82() => ValidateMethod(); // ValidateMethod("8.2", "Pro Outline");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/8.2/Poiyomi Pro Outline Early      \u279C      .poiyomi \u2215 Poiyomi Pro Outline Early", false)]
    private static void UpgradeOutlineEarlyPro82() => UpgradeShader("8.2", "Pro Outline Early", "Pro Outline Early");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/8.2/Poiyomi Pro Outline Early      \u279C      .poiyomi \u2215 Poiyomi Pro Outline Early", true)]
    private static bool ValidateUpgradeOutlineEarlyPro82() => ValidateMethod(); // ValidateMethod("8.2", "Pro Outline Early");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/8.2/Poiyomi Pro Outline Tessellated      \u279C      .poiyomi \u2215 Poiyomi Pro Tessellated", false)]
    private static void UpgradeOutlineTessellatedPro82() => UpgradeShader("8.2", "Pro Outline Tessellated", "Pro Tessellated");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/8.2/Poiyomi Pro Outline Tessellated      \u279C      .poiyomi \u2215 Poiyomi Pro Tessellated", true)]
    private static bool ValidateUpgradeOutlineTessellatedPro82() => ValidateMethod(); // ValidateMethod("8.2", "Pro Outline Tessellated");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/8.2/Poiyomi Pro ShatterWave", false)]
    private static void UpgradeShatterwavePro82() => UpgradeShader("8.2", "Pro ShatterWave");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/8.2/Poiyomi Pro ShatterWave", true)]
    private static bool ValidateUpgradeShatterwavePro82() => ValidateMethod(); // ValidateMethod("8.2", "Pro ShatterWave");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/8.2/Poiyomi Pro Tessellated", false)]
    private static void UpgradeTessellatedPro82() => UpgradeShader("8.2", "Pro Tessellated");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/8.2/Poiyomi Pro Tessellated", true)]
    private static bool ValidateUpgradeTessellatedPro82() => ValidateMethod(); // ValidateMethod("8.2", "Pro Tessellated");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/8.2/Poiyomi Pro Tessellated Geom", false)]
    private static void UpgradeTessellatedGeomPro82() => UpgradeShader("8.2", "Pro Tessellated Geom");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/8.2/Poiyomi Pro Tessellated Geom", true)]
    private static bool ValidateUpgradeTessellatedGeomPro82() => ValidateMethod(); // ValidateMethod("8.2", "Pro Tessellated Geom");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/8.2/Poiyomi Pro Two Pass", false)]
    private static void UpgradeTwoPassPro82() => UpgradeShader("8.2", "Pro Two Pass");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/8.2/Poiyomi Pro Two Pass", true)]
    private static bool ValidateUpgradeTwoPassPro82() => ValidateMethod(); // ValidateMethod("8.2", "Pro Two Pass");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/8.2/Poiyomi Pro Wireframe", false)]
    private static void UpgradeWireframePro82() => UpgradeShader("8.2", "Pro Wireframe");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/8.2/Poiyomi Pro Wireframe", true)]
    private static bool ValidateUpgradeWireframePro82() => ValidateMethod(); // ValidateMethod("8.2", "Pro Wireframe");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/8.2/Poiyomi Pro World", false)]
    private static void UpgradeWorldPro82() => UpgradeShader("8.2", "Pro World");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/8.2/Poiyomi Pro World", true)]
    private static bool ValidateUpgradeWorldPro82() => ValidateMethod(); // ValidateMethod("8.2", "Pro World");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/8.2/Poiyomi Pro World Fur", false)]
    private static void UpgradeWorldFurPro82() => UpgradeShader("8.2", "Pro World Fur");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/8.2/Poiyomi Pro World Fur", true)]
    private static bool ValidateUpgradeWorldFurPro82() => ValidateMethod(); // ValidateMethod("8.2", "Pro World Fur");
    
#endregion

#region 9.0

    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/9.0/Poiyomi Pro", false)]
    private static void UpgradePro90() => UpgradeShader("9.0", "Pro");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/9.0/Poiyomi Pro", true)]
    private static bool ValidateUpgradePro90() => ValidateMethod(); // ValidateMethod("9.0", "Pro");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/9.0/Poiyomi Pro Fur", false)]
    private static void UpgradeFurPro90() => UpgradeShader("9.0", "Pro Fur");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/9.0/Poiyomi Pro Fur", true)]
    private static bool ValidateUpgradeFurPro90() => ValidateMethod(); // ValidateMethod("9.0", "Pro Fur");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/9.0/Poiyomi Pro Geom", false)]
    private static void UpgradeGeomPro90() => UpgradeShader("9.0", "Pro Geom");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/9.0/Poiyomi Pro Geom", true)]
    private static bool ValidateUpgradeGeomPro90() => ValidateMethod(); // ValidateMethod("9.0", "Pro Geom");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/9.0/Poiyomi Pro Geometric Dissolve", false)]
    private static void UpgradeGeometricDissolvePro90() => UpgradeShader("9.0", "Pro Geometric Dissolve");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/9.0/Poiyomi Pro Geometric Dissolve", true)]
    private static bool ValidateUpgradeGeometricDissolvePro90() => ValidateMethod(); // ValidateMethod("9.0", "Pro Geometric Dissolve");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/9.0/Poiyomi Pro Grab Pass", false)]
    private static void UpgradeGrabPassPro90() => UpgradeShader("9.0", "Pro Grab Pass");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/9.0/Poiyomi Pro Grab Pass", true)]
    private static bool ValidateUpgradeGrabPassPro90() => ValidateMethod(); // ValidateMethod("9.0", "Pro Grab Pass");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/9.0/Poiyomi Pro Outline Early", false)]
    private static void UpgradeOutlineEarlyPro90() => UpgradeShader("9.0", "Pro Outline Early");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/9.0/Poiyomi Pro Outline Early", true)]
    private static bool ValidateUpgradeOutlineEarlyPro90() => ValidateMethod(); // ValidateMethod("9.0", "Pro Outline Early");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/9.0/Poiyomi Pro Tessellated", false)]
    private static void UpgradeTessellatedPro90() => UpgradeShader("9.0", "Pro Tessellated");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/9.0/Poiyomi Pro Tessellated", true)]
    private static bool ValidateUpgradeTessellatedPro90() => ValidateMethod(); // ValidateMethod("9.0", "Pro Tessellated");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/9.0/Poiyomi Pro Tessellated Geom", false)]
    private static void UpgradeTessellatedGeomPro90() => UpgradeShader("9.0", "Pro Tessellated Geom");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/9.0/Poiyomi Pro Tessellated Geom", true)]
    private static bool ValidateUpgradeTessellatedGeomPro90() => ValidateMethod(); // ValidateMethod("9.0", "Pro Tessellated Geom");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/9.0/Poiyomi Pro Tessellated Geometric Dissolve", false)]
    private static void UpgradeTessellatedGeometricDissolvePro90() => UpgradeShader("9.0", "Pro Tessellated Geometric Dissolve");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/9.0/Poiyomi Pro Two Pass", false)]
    private static void UpgradeTwoPassPro90() => UpgradeShader("9.0", "Pro Two Pass");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/9.0/Poiyomi Pro Two Pass", true)]
    private static bool ValidateUpgradeTwoPassPro90() => ValidateMethod(); // ValidateMethod("9.0", "Pro Two Pass");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/9.0/Poiyomi Pro Wireframe", false)]
    private static void UpgradeWireframePro90() => UpgradeShader("9.0", "Pro Wireframe");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/9.0/Poiyomi Pro Wireframe", true)]
    private static bool ValidateUpgradeWireframePro90() => ValidateMethod(); // ValidateMethod("9.0", "Pro Wireframe");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/9.0/Poiyomi Pro World", false)]
    private static void UpgradeWorldPro90() => UpgradeShader("9.0", "Pro World");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/9.0/Poiyomi Pro World", true)]
    private static bool ValidateUpgradeWorldPro90() => ValidateMethod(); // ValidateMethod("9.0", "Pro World");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/9.0/Poiyomi Pro World Fur", false)]
    private static void UpgradeWorldFurPro90() => UpgradeShader("9.0", "Pro World Fur");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/9.0/Poiyomi Pro World Fur", true)]
    private static bool ValidateUpgradeWorldFurPro90() => ValidateMethod(); // ValidateMethod("9.0", "Pro World Fur");

#endregion

#region 9.1

    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/9.1/Poiyomi Pro", false)]
    private static void UpgradePro91() => UpgradeShader("9.1", "Pro");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/9.1/Poiyomi Pro", true)]
    private static bool ValidateUpgradePro91() => ValidateMethod(); // ValidateMethod("9.1", "Pro");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/9.1/Poiyomi Pro Fur", false)]
    private static void UpgradeFurPro91() => UpgradeShader("9.1", "Pro Fur");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/9.1/Poiyomi Pro Fur", true)]
    private static bool ValidateUpgradeFurPro91() => ValidateMethod(); // ValidateMethod("9.1", "Pro Fur");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/9.1/Poiyomi Pro Geom", false)]
    private static void UpgradeGeomPro91() => UpgradeShader("9.1", "Pro Geom");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/9.1/Poiyomi Pro Geom", true)]
    private static bool ValidateUpgradeGeomPro91() => ValidateMethod(); // ValidateMethod("9.1", "Pro Geom");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/9.1/Poiyomi Pro Geometric Dissolve", false)]
    private static void UpgradeGeometricDissolvePro91() => UpgradeShader("9.1", "Pro Geometric Dissolve");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/9.1/Poiyomi Pro Geometric Dissolve", true)]
    private static bool ValidateUpgradeGeometricDissolvePro91() => ValidateMethod(); // ValidateMethod("9.1", "Pro Geometric Dissolve");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/9.1/Poiyomi Pro Grab Pass", false)]
    private static void UpgradeGrabPassPro91() => UpgradeShader("9.1", "Pro Grab Pass");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/9.1/Poiyomi Pro Grab Pass", true)]
    private static bool ValidateUpgradeGrabPassPro91() => ValidateMethod(); // ValidateMethod("9.1", "Pro Grab Pass");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/9.1/Poiyomi Pro Outline Early", false)]
    private static void UpgradeOutlineEarlyPro91() => UpgradeShader("9.1", "Pro Outline Early");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/9.1/Poiyomi Pro Outline Early", true)]
    private static bool ValidateUpgradeOutlineEarlyPro91() => ValidateMethod(); // ValidateMethod("9.1", "Pro Outline Early");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/9.1/Poiyomi Pro Tessellated", false)]
    private static void UpgradeTessellatedPro91() => UpgradeShader("9.1", "Pro Tessellated");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/9.1/Poiyomi Pro Tessellated", true)]
    private static bool ValidateUpgradeTessellatedPro91() => ValidateMethod(); // ValidateMethod("9.1", "Pro Tessellated");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/9.1/Poiyomi Pro Tessellated Geom", false)]
    private static void UpgradeTessellatedGeomPro91() => UpgradeShader("9.1", "Pro Tessellated Geom");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/9.1/Poiyomi Pro Tessellated Geom", true)]
    private static bool ValidateUpgradeTessellatedGeomPro91() => ValidateMethod(); // ValidateMethod("9.1", "Pro Tessellated Geom");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/9.1/Poiyomi Pro Tessellated Geometric Dissolve", false)]
    private static void UpgradeTessellatedGeometricDissolvePro91() => UpgradeShader("9.1", "Pro Tessellated Geometric Dissolve");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/9.1/Poiyomi Pro Two Pass", false)]
    private static void UpgradeTwoPassPro91() => UpgradeShader("9.1", "Pro Two Pass");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/9.1/Poiyomi Pro Two Pass", true)]
    private static bool ValidateUpgradeTwoPassPro91() => ValidateMethod(); // ValidateMethod("9.1", "Pro Two Pass");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/9.1/Poiyomi Pro Wireframe", false)]
    private static void UpgradeWireframePro91() => UpgradeShader("9.1", "Pro Wireframe");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/9.1/Poiyomi Pro Wireframe", true)]
    private static bool ValidateUpgradeWireframePro91() => ValidateMethod(); // ValidateMethod("9.1", "Pro Wireframe");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/9.1/Poiyomi Pro World", false)]
    private static void UpgradeWorldPro91() => UpgradeShader("9.1", "Pro World");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/9.1/Poiyomi Pro World", true)]
    private static bool ValidateUpgradeWorldPro91() => ValidateMethod(); // ValidateMethod("9.1", "Pro World");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/9.1/Poiyomi Pro World Fur", false)]
    private static void UpgradeWorldFurPro91() => UpgradeShader("9.1", "Pro World Fur");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Pro/9.1/Poiyomi Pro World Fur", true)]
    private static bool ValidateUpgradeWorldFurPro91() => ValidateMethod(); // ValidateMethod("9.1", "Pro World Fur");
    
#endregion

#endregion

#region Toon

#region 7.3

    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Toon/7.3/• Poiyomi Toon •      \u279C      .poiyomi \u2215 Poiyomi Toon", false)]
    private static void UpgradeToon73() => UpgradeShader("7.3", "• Poiyomi Toon •", "Toon");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Toon/7.3/• Poiyomi Toon •      \u279C      .poiyomi \u2215 Poiyomi Toon", true)]
    private static bool ValidateUpgradeToon73() => ValidateMethod(); // ValidateMethod("7.3", "• Poiyomi Toon •");

#endregion

#region 8.0

    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Toon/8.0/Poiyomi Outline      \u279C      .poiyomi \u2215 Poiyomi Toon", false)]
    private static void UpgradeOutlineToon80() => UpgradeShader("8.0", "Poiyomi Outline", "Poiyomi Toon");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Toon/8.0/Poiyomi Outline      \u279C      .poiyomi \u2215 Poiyomi Toon", true)]
    private static bool ValidateUpgradeOutlineToon80() => ValidateMethod(); // ValidateMethod("8.0", "Poiyomi Outline");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Toon/8.0/Poiyomi Outline Early      \u279C      .poiyomi \u2215 Poiyomi Toon", false)]
    private static void UpgradeOutlineEarlyToon80() => UpgradeShader("8.0", "Poiyomi Outline Early", "Poiyomi Toon");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Toon/8.0/Poiyomi Outline Early      \u279C      .poiyomi \u2215 Poiyomi Toon", true)]
    private static bool ValidateUpgradeOutlineEarlyToon80() => ValidateMethod(); // ValidateMethod("8.0", "Poiyomi Outline Early");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Toon/8.0/Poiyomi Toon", false)]
    private static void UpgradeToon80() => UpgradeShader("8.0", "Poiyomi Toon");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Toon/8.0/Poiyomi Toon", true)]
    private static bool ValidateUpgradeToon80() => ValidateMethod(); // ValidateMethod("8.0", "Poiyomi Toon");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Toon/8.0/Poiyomi World      \u279C      .poiyomi \u2215 Poiyomi Toon World", false)]
    private static void UpgradeWorldToon80() => UpgradeShader("8.0", "Poiyomi World", "Poiyomi Toon World");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Toon/8.0/Poiyomi World      \u279C      .poiyomi \u2215 Poiyomi Toon World", true)]
    private static bool ValidateUpgradeWorldToon80() => ValidateMethod(); // ValidateMethod("8.0", "Poiyomi World");

#endregion

#region 8.1

    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Toon/8.1/Poiyomi Toon", false)]
    private static void UpgradeToon81() => UpgradeShader("8.1", "Poiyomi Toon");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Toon/8.1/Poiyomi Toon", true)]
    private static bool ValidateUpgradeToon81() => ValidateMethod(); // ValidateMethod("8.1", "Poiyomi Toon");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Toon/8.1/Poiyomi Toon Early Z      \u279C      .poiyomi \u2215 Poiyomi Toon", false)]
    private static void UpgradeEarlyZToon81() => UpgradeShader("8.1", "Poiyomi Toon Early Z", "Poiyomi Toon");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Toon/8.1/Poiyomi Toon Early Z      \u279C      .poiyomi \u2215 Poiyomi Toon", true)]
    private static bool ValidateUpgradeEarlyZToon81() => ValidateMethod(); // ValidateMethod("8.1", "Poiyomi Toon Early Z");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Toon/8.1/Poiyomi Toon Outline      \u279C      .poiyomi \u2215 Poiyomi Toon", false)]
    private static void UpgradeOutlineToon81() => UpgradeShader("8.1", "Poiyomi Toon Outline", "Poiyomi Toon");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Toon/8.1/Poiyomi Toon Outline      \u279C      .poiyomi \u2215 Poiyomi Toon", true)]
    private static bool ValidateUpgradeOutlineToon81() => ValidateMethod(); // ValidateMethod("8.1", "Poiyomi Toon Outline");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Toon/8.1/Poiyomi Toon Outline Early", false)]
    private static void UpgradeOutlineEarlyToon81() => UpgradeShader("8.1", "Poiyomi Toon Outline Early");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Toon/8.1/Poiyomi Toon Outline Early", true)]
    private static bool ValidateUpgradeOutlineEarlyToon81() => ValidateMethod(); // ValidateMethod("8.1", "Poiyomi Toon Outline Early");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Toon/8.1/Poiyomi World      \u279C      .poiyomi \u2215 Poiyomi Toon World", false)]
    private static void UpgradeWorldToon81() => UpgradeShader("8.1", "Poiyomi World", "Poiyomi Toon World");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Toon/8.1/Poiyomi World      \u279C      .poiyomi \u2215 Poiyomi Toon World", true)]
    private static bool ValidateUpgradeWorldToon81() => ValidateMethod(); // ValidateMethod("8.1", "Poiyomi World");

#endregion

#region 9.0

    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Toon/9.0/Poiyomi Toon", false)]
    private static void UpgradeToon90() => UpgradeShader("9.0", "Poiyomi Toon");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Toon/9.0/Poiyomi Toon", true)]
    private static bool ValidateUpgradeToon90() => ValidateMethod(); // ValidateMethod("9.0", "Poiyomi Toon");

    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Toon/9.0/Poiyomi Toon Grab Pass", false)]
    private static void UpgradeGrabPassToon90() => UpgradeShader("9.0", "Poiyomi Toon Grab Pass");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Toon/9.0/Poiyomi Toon Grab Pass", true)]
    private static bool ValidateUpgradeGrabPassToon90() => ValidateMethod(); // ValidateMethod("9.0", "Poiyomi Toon Grab Pass");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Toon/9.0/Poiyomi Toon Outline Early", false)]
    private static void UpgradeOutlineEarlyToon90() => UpgradeShader("9.0", "Poiyomi Toon Outline Early");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Toon/9.0/Poiyomi Toon Outline Early", true)]
    private static bool ValidateUpgradeOutlineEarlyToon90() => ValidateMethod(); // ValidateMethod("9.0", "Poiyomi Toon Outline Early");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Toon/9.0/Poiyomi Toon Two Pass", false)]
    private static void UpgradeTwoPassToon90() => UpgradeShader("9.0", "Poiyomi Toon Two Pass");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Toon/9.0/Poiyomi Toon Two Pass", true)]
    private static bool ValidateUpgradeTwoPassToon90() => ValidateMethod(); // ValidateMethod("9.0", "Poiyomi Toon Two Pass");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Toon/9.0/Poiyomi Toon World", false)]
    private static void UpgradeWorldToon90() => UpgradeShader("9.0", "Poiyomi Toon World");
    
    [MenuItem("GameObject/Poiyomi/Upgrade Material(s) to Latest Poiyomi/Toon/9.0/Poiyomi Toon World", true)]
    private static bool ValidateUpgradeWorldToon90() => ValidateMethod(); // ValidateMethod("9.0", "Poiyomi Toon World");

#endregion

#endregion
    
    private static void UpgradeShader(string version, string partOfShaderName, string forceChangeTo = "") {
        if (Selection.objects.Length == 0) return; // bail if nothing selected
        
        // for each selected object
        foreach (var obj in Selection.objects) {
            if (obj is Material material) {
                var isOld = IsOlderPoi(material);
                var is73Old = material.shader.name.Contains("7.3/•") || material.shader.name.Contains("7.3/\u2605");
                var isToon = IsPoiToon(material, version, partOfShaderName);
                var isPro = IsProPoi(material, version, partOfShaderName);
                var isValidPoiShader = IsValidPoiShader(material);
                var matUpgraded = false;

                Debug.Log($"{LoggerPrefix} <Material> Upgrading material \"<color=yellow>{material.name}</color>\" on \"<color=yellow>{obj.name}</color>\"...");
                
                // Debug.Log("isOld: " + isOld);
                // Debug.Log("isToon: " + isToon);
                // Debug.Log("isPro: " + isPro);
                // Debug.Log("isValidPoiShader: " + isValidPoiShader);
                // Debug.Log("is73Old: " + is73Old);
                // Debug.Log("partOfShaderName: " + partOfShaderName);

                if ((isPro || isToon) && (isOld || is73Old) && isValidPoiShader) { // if pro or toon and old or really old and is valid
                    // upgrade to latest Poi
                    var shaderName = $".poiyomi/Poiyomi {(string.IsNullOrWhiteSpace(forceChangeTo) ? partOfShaderName : forceChangeTo)}";
                    Debug.Log($"{LoggerPrefix} <Material> Shader Name: \"<color=yellow>{shaderName}</color>\"");
                    var shader = Shader.Find(shaderName);
                    if (shader == null) {
                        Debug.LogWarning($"{LoggerPrefix} <Material> Shader \"<color=yellow>{shaderName}</color>\" not found.");
                        continue;
                    }

                    material.shader = shader;
                    Debug.Log($"{LoggerPrefix} <Material> Upgraded \"<color=yellow>{material.name}</color>\" to \"<color=yellow>{shaderName}</color>\"");
                    matUpgraded = true;
                }

                if (!matUpgraded)
                    Debug.LogWarning($"{LoggerPrefix} Material on \"<color=yellow>{material.name}</color>\" may not a Poiyomi shader or is already the latest version.");
            }
            else if (obj is GameObject go) {
                // go through each GameObject Transform
                foreach (var o in go.GetComponentsInChildren<Transform>()) {
                    var renderers = o.GetComponentsInChildren<Renderer>();
                    // go through each Renderer (All Types)
                    foreach (var renderer in renderers) {
                        // go through each Material
                        foreach (var goMaterial in renderer.sharedMaterials) {
                            var isOld = IsOlderPoi(goMaterial);
                            var is73Old = goMaterial.shader.name.Contains("7.3/•") || goMaterial.shader.name.Contains("7.3/\u2605");
                            var isToon = IsPoiToon(goMaterial, version, partOfShaderName);
                            var isPro = IsProPoi(goMaterial, version, partOfShaderName);
                            var isValidPoiShader = IsValidPoiShader(goMaterial);
                            var goUpgraded = false;

                            Debug.Log($"{LoggerPrefix} <GameObject> Upgrading material \"<color=yellow>{goMaterial.name}</color>\" on \"<color=yellow>{o.name}</color>\"...");
                            
                            // Debug.Log("isOld: " + isOld);
                            // Debug.Log("isToon: " + isToon);
                            // Debug.Log("isPro: " + isPro);
                            // Debug.Log("isValidPoiShader: " + isValidPoiShader);
                            // Debug.Log("is73Old: " + is73Old);
                            // Debug.Log("partOfShaderName: " + partOfShaderName);
                            
                            if ((isPro || isToon) && (isOld || is73Old) && isValidPoiShader) { // if pro or toon and old or really old and is valid
                                // upgrade to latest Poi
                                var shaderName = $".poiyomi/Poiyomi {(string.IsNullOrWhiteSpace(forceChangeTo) ? partOfShaderName : forceChangeTo)}";
                                Debug.Log($"{LoggerPrefix} <GameObject> Shader Name: \"<color=yellow>{shaderName}</color>\"");
                                var shader = Shader.Find(shaderName);
                                if (shader == null) {
                                    Debug.LogWarning($"{LoggerPrefix} <GameObject> Shader \"<color=yellow>{shaderName}</color>\" not found.");
                                    continue;
                                }

                                goMaterial.shader = shader;
                                Debug.Log($"{LoggerPrefix} <GameObject> Upgraded \"<color=yellow>{goMaterial.name}</color>\" to \"<color=yellow>{shaderName}</color>\"");
                                goUpgraded = true;
                            }

                            if (!goUpgraded)
                                Debug.LogWarning($"{LoggerPrefix} Material on \"<color=yellow>{goMaterial.name}</color>\" may not a Poiyomi shader or is already the latest version.");
                        }
                    }
                }
            }
        }
        
        AssetDatabase.SaveAssets();
    }

    private static bool ValidateMethod(/*string version, string partOfShaderName*/) {
        return Selection.activeObject is Material or GameObject;
        // if (Selection.objects.Length == 0) return false; // bail if nothing selected
        // var finalResult = false;
        //
        // // for each selected object
        // foreach (var obj in Selection.objects) {
        //     if (obj is Material material) {
        //         var isOld = IsOlderPoi(material);
        //         var isToon = IsPoiToon(material, version, partOfShaderName);
        //         var isPro = IsProPoi(material, version, partOfShaderName);
        //         var isValidPoiShader = IsValidPoiShader(material, partOfShaderName);
        //
        //         finalResult = (isPro || isToon) && isOld && isValidPoiShader;
        //         break;
        //     }
        //
        //     if (obj is GameObject go) {
        //         foreach (var o in go.GetComponentsInChildren<Transform>()) {
        //             var renderers = o.GetComponentsInChildren<Renderer>();
        //             // go through each Renderer
        //             foreach (var renderer in renderers) {
        //                 // go through each Material
        //                 foreach (var goMaterial in renderer.sharedMaterials) {
        //                     var isOld = IsOlderPoi(goMaterial);
        //                     var isToon = IsPoiToon(goMaterial, version, partOfShaderName);
        //                     var isPro = IsProPoi(goMaterial, version, partOfShaderName);
        //                     var isValidPoiShader = IsValidPoiShader(goMaterial, partOfShaderName);
        //
        //                     finalResult = (isPro || isToon) && isOld && isValidPoiShader;
        //                 }
        //             }
        //         }
        //
        //         break;
        //     }
        // }
        //
        // return finalResult;
    }
    
    private static bool IsOlderPoi(Material material) => material.shader.name.Contains("/Old Versions/");
    private static bool IsProPoi(Material material, string version, string partOfShaderName) => material.shader.name.Contains($".poiyomi/Old Versions/{version}/Poiyomi {partOfShaderName}");
    private static bool IsPoiToon(Material material, string version, string partOfShaderName) => material.shader.name.Contains($".poiyomi/Old Versions/{version}/Poiyomi {partOfShaderName}");
    private static bool IsValidPoiShader(Material material) => material.shader.name.Contains(".poiyomi");
    
    private const string LoggerPrefix = "[<color=#9fffe3>Poiyomi Upgrader</color>]";
}
#endif
