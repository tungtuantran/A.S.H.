#if UNITY_IOS
using System;
using System.IO;

using UnityEngine;
using UnityEngine.Assertions;

using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;

using SimpleJSON;

public static class UnityCoreHapticsPostProcessor
{
  // Set to path that this script is in
  const string MODULE_MAP_FILENAME = "module.modulemap";

  [PostProcessBuild]
  public static void OnPostProcessBuild(BuildTarget buildTarget, string buildPath)
  {
    if (buildTarget == BuildTarget.iOS)
    {
      var pbxProjectPath = PBXProject.GetPBXProjectPath(buildPath);
      var proj = new PBXProject();
      proj.ReadFromFile(pbxProjectPath);

      string targetGUID = proj.GetUnityFrameworkTargetGuid();

      // Get relative path of the plugin the from Assets folder
      // Should be something like "UnityCoreHaptics/Plugins/iOS/UnityCoreHaptics/Source"
      var pluginRelativePathInUnity = GetPluginPathRelativeToAssets();

      // Get relative path of the plugin in XCode
      string pluginRelativePathInXCode = Path.Combine("Libraries", pluginRelativePathInUnity);

      proj.AddFrameworkToProject(targetGUID, "CoreHaptics.framework", false);
      proj.AddBuildProperty(targetGUID, "SWIFT_VERSION", "5.1");
      proj.SetBuildProperty(targetGUID, "ENABLE_BITCODE", "NO");

      proj.AddBuildProperty(targetGUID, "CLANG_ENABLE_MODULES", "YES");
      proj.AddBuildProperty(targetGUID, "SWIFT_INCLUDE_PATHS", pluginRelativePathInXCode);
      proj.AddBuildProperty(targetGUID, "LD_RUNPATH_SEARCH_PATHS", "@executable_path/Frameworks");

      WriteModuleToFramework(proj, targetGUID, pbxProjectPath, pluginRelativePathInUnity, pluginRelativePathInXCode);
      FixAHAPAudioFilePaths(proj, pbxProjectPath);
    }
  }
  
  // Made to check if two paths are the same
  // Based on this: https://stackoverflow.com/questions/2281531/how-can-i-compare-directory-paths-in-c
  private static string _normalizePath(string path)
  {
      return Path.GetFullPath(new Uri(path).LocalPath)
                .TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
                .ToUpperInvariant();
  }

  private static string _getRelativePath(string basePath, string fullPath) {
    // Base case 1: not found
    if (fullPath == null || fullPath == "") {
      return null;
    }
    // Base case 2: found
    if (_normalizePath(fullPath) == _normalizePath(basePath)) {
      return "";
    }
    // Recursive case
    var dirPath = Path.GetDirectoryName(fullPath);
    return Path.Combine(_getRelativePath(basePath, dirPath), Path.GetFileName(fullPath));
  }

  private static string GetPluginPathRelativeToAssets() {
      string[] files = System.IO.Directory.GetFiles(Application.dataPath, "UnityCoreHapticsProxy.cs", SearchOption.AllDirectories);
      if (files.Length != 1) {
        throw new Exception("[UnityCoreHapticsPostProcessor] Error: there should exactly be one file named UnityCoreHapticsProxy.cs");
      }
      return Path.GetDirectoryName(_getRelativePath(Application.dataPath, files[0]));
  }

  private static void WriteModuleToFramework(
    PBXProject proj,
    string targetGUID,
    string pbxProjectPath,
    string pluginRelativePathInUnity,
    string pluginRelativePathInXCode
  )
  {
    // Add a module map reference to the XCode project
    string moduleMapDestRelativePath = Path.Combine(pluginRelativePathInXCode, MODULE_MAP_FILENAME);
    Debug.Log("[UnityCoreHapticsPostProcessor] Adding properties to XCode framework. Module path : " + moduleMapDestRelativePath);
    string file_guid = proj.AddFile(moduleMapDestRelativePath, moduleMapDestRelativePath, PBXSourceTree.Source);
    proj.AddFileToBuild(targetGUID, file_guid);
    proj.WriteToFile(pbxProjectPath);

    // Copy the module file from Unity to XCode
    string sourcePath = Path.Combine(Application.dataPath, pluginRelativePathInUnity, MODULE_MAP_FILENAME);
    string destPath = Path.Combine(Path.GetDirectoryName(pbxProjectPath), "..", moduleMapDestRelativePath);
    if (!Directory.Exists(Path.GetDirectoryName(destPath)))
    {
      Debug.Log("[UnityCoreHapticsPostProcessor] Creating directory " + destPath);
      Directory.CreateDirectory(Path.GetDirectoryName(destPath));
    }
    Debug.Log("[UnityCoreHapticsPostProcessor] Copy module file to project : " + sourcePath + " -> " + destPath);
    File.Copy(sourcePath, destPath);
  }

  /// <summary>
  /// Corrects the audio paths in AHAP files to inclulde Data/Raw at the beginning.
  /// Also checks if the audio paths are correct.
  /// </summary>
  /// <param name="pbxProject"></param>
  /// <param name="pbxProjectPath"></param>
  private static void FixAHAPAudioFilePaths(PBXProject pbxProject, string pbxProjectPath) {

    const string audioPathKey = "EventWaveformPath";

    // StreamingAssets are stored in Data/Raw in XCode builds
    // See: https://docs.unity3d.com/Manual/StreamingAssets.html
    string projFileDir = Path.GetDirectoryName(pbxProjectPath);
    string streamingAssetsPath = Path.Combine(projFileDir, "..", "Data/Raw");

    // Get all AHAP files recursively
    var ahapPathArray = Directory.GetFiles(streamingAssetsPath, "*.ahap", SearchOption.AllDirectories);
    
    // Loop through all AHAP files
    foreach (var ahapPath in ahapPathArray) {
      var text = File.ReadAllText(ahapPath);

      // Load them as JSON
      // Example usage: https://wiki.unity3d.com/index.php/SimpleJSON
      JSONNode json = SimpleJSON.JSON.Parse(text);
      JSONArray eventsArray = json["Pattern"].AsArray;

      // Loop through all events in the pattern
      foreach (var keyValuePair in eventsArray) {
        var _event = keyValuePair.Value; // event is a special keyword in C#, hence _event
        var obj = _event["Event"];
        if (obj.HasKey(audioPathKey)) {
          // Get path to audio file (relative to StreamingAssets folder)
          var relativePath = obj[audioPathKey].Value;

          // Assert that audio location is correct
          var audioFileExists = File.Exists(Path.Combine(Application.streamingAssetsPath, relativePath));
          try {
            Assert.IsTrue(audioFileExists);
          }
          catch {
            throw new System.Exception(
              "[UnityCoreHapticsPostProcessor] " + Path.GetFileName(ahapPath) 
              + " refers to an audio path that does not exist: " + relativePath
              + ". Please check that your audio file is within the StreamingAssets folder" 
              + " and that the path is relative to StreamingAssets."
            );
          }

          // Modify path by prepending Data/Raw where StreamingAssets are dumped
          // See: https://docs.unity3d.com/Manual/StreamingAssets.html
          obj[audioPathKey] = Path.Combine("Data/Raw", relativePath);
        }
      }

      // Update AHAP files in a compact format to reduce build size
      File.WriteAllText(ahapPath, json.ToString());
    }
  }
}
#endif
