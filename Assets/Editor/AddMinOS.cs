#if UNITY_IOS
using UnityEngine;
using UnityEditor.Callbacks;
using UnityEditor;
using UnityEditor.iOS.Xcode;
using System.IO;
 
// Post process for iOS build
public class AddMinOS
{
    [PostProcessBuild]
    public static void ChangeXcodePlist(BuildTarget buildTarget, string pathToBuiltProject)
    {
        UnityEngine.Debug.Log( "ChangeXcodePlist" );
     
        // Info.plist modifications
        {
            // Get plist
            string plistPath = pathToBuiltProject + "/Info.plist";
            var plist = new PlistDocument();
            plist.ReadFromString(File.ReadAllText(plistPath));
 
            // Get root
            var rootDict = plist.root;
 
            // Set minimum OS to 13
            var buildKeyMinOS = "MinimumOSVersion";
            rootDict.SetString(buildKeyMinOS, "13.0");
 
            // Write to file
            File.WriteAllText(plistPath, plist.WriteToString());
        }
    }
}
#endif