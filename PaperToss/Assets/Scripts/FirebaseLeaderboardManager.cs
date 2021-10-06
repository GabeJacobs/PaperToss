using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Firebase;
using Firebase.Analytics;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using Oculus.Platform;
using Oculus.Platform.Models;

public class FirebaseLeaderboardManager : MonoBehaviour
{
    
    //Firebase variables
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;    
    public FirebaseUser User;
    public DatabaseReference DBreference;

    
    public FirebaseApp app;
    void Awake () {
        //
        // Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
        //     var dependencyStatus = task.Result;
        //     if (dependencyStatus == Firebase.DependencyStatus.Available) {
        //         FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
        //         InitializeFirebase();
        //         // Set a flag here to indicate whether Firebase is ready to use by your app.
        //     } else {
        //         UnityEngine.Debug.LogError(System.String.Format(
        //             "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
        //         // Firebase Unity SDK is not safe to use here.
        //     }
        // });
        
    }
    
    private void InitializeFirebase()
    {
        // Debug.Log("Setting up Firebase Auth");
        // app = Firebase.FirebaseApp.DefaultInstance;
        // auth = FirebaseAuth.DefaultInstance;
        // DBreference = FirebaseDatabase.DefaultInstance.RootReference;
    }
    void Start()
    {
        
        //
        // WriteNewScore("2", "amy", 24);
        // WriteNewScore("3", "isa", 2);

        // DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        // FirebaseDatabase.DefaultInstance
        //     .GetReference("user-scores").OrderByChild("score")
        //     .GetValueAsync().ContinueWithOnMainThread(task => {
        //         if (task.IsFaulted) {
        //             // Handle the error...
        //         }
        //         else if (task.IsCompleted) {
        //             DataSnapshot snapshot = task.Result;
        //             foreach (DataSnapshot childSnapshot in snapshot.Children.Reverse<DataSnapshot>())
        //             {
        //                 Debug.Log(childSnapshot.Child("uid"));
        //                 Debug.Log(childSnapshot.Child("username"));
        //                 Debug.Log(childSnapshot.Child("score"));
        //             }
        //             // Do something with snapshot...
        //         }
        //     });
        //


        // Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        // auth.SignInAnonymouslyAsync().ContinueWith(task => {
        //     if (task.IsCanceled) {
        //         Debug.LogError("SignInAnonymouslyAsync was canceled.");
        //         return;
        //     }
        //     if (task.IsFaulted) {
        //         Debug.LogError("SignInAnonymouslyAsync encountered an error: " + task.Exception);
        //         return;
        //     }
        //
        //     Firebase.Auth.FirebaseUser newUser = task.Result;
        //     Debug.LogFormat("User signed in successfully: {0} ({1})",
        //         newUser.DisplayName, newUser.UserId);
        // });

    }

    void LoggedInWithOculusID(string oculusID, string oculusName)
    {
        writeNewUser(oculusID, oculusName);

    }
    
    private void writeNewUser(string userId, string name) {
        // DatabaseReference mDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;
        // PTUser user = new PTUser(name);
        // string json = JsonUtility.ToJson(user);
        // mDatabaseRef.Child("users").Child(userId).SetRawJsonValueAsync(json);
    }
    
    private void WriteNewScore(string userId, string username, int score) {
        // DatabaseReference mDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;
        //
        // // Create new entry at /user-scores/$userid/$scoreid and at
        // // /leaderboard/$scoreid simultaneously
        //
        // string key = mDatabaseRef.Child("leaderboards").Push().Key;
        // Debug.Log (key);
        // LeaderboardEntry entry = new LeaderboardEntry(userId, username, score.ToString());
        // Dictionary<string, string> entryValues = entry.ToDictionary();
        // Dictionary<string, System.Object> childUpdates = new Dictionary<string, System.Object>();
        // childUpdates["/scores/" + key] = entryValues;
        // childUpdates["/user-scores/" + userId + "/" + key] = entryValues;
        // mDatabaseRef.UpdateChildrenAsync(childUpdates);
    }
}
