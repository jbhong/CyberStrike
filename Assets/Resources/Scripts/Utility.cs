using UnityEngine;
using SimpleFirebaseUnity;
using SimpleFirebaseUnity.MiniJSON;
using System.Collections.Generic;
using System.Collections;
using System;
using System.Linq;

public static class Utility
{
    private static string apiPassword = "password";

    public static List<KeyValuePair<string, int>> scores = new List<KeyValuePair<string, int>>();

    private static Firebase firebase;
    private static FirebaseQueue firebaseQueue;

    private static int authenticated = -1;
    private static bool waitingForLogin = false;
    private static bool waitingForCreate = false;
    private static int updateSuccess = -1;
    private static int foundUser = -1;

    private static string failureReason = "";

    private static string userLooking;
    private static string password;

    private static Dictionary<string, object> userDetails = new Dictionary<string, object>();

    private static void GetOKHandler(Firebase sender, DataSnapshot snapshot)
    {
        Dictionary<string, object> _dict = snapshot.Value<Dictionary<string, object>>();
        List<string> _keys = snapshot.Keys;

        if (sender.Key == "users")
        {
            if (waitingForLogin)
            {
                if (_keys != null)
                {
                    foreach (string _key in _keys)
                    {
                        if (_key == userLooking)
                        {
                            Dictionary<string, object> _dictVals = (Dictionary<string, object>)_dict[_key];
                            if (_dictVals["password"].Equals(password))
                            {
                                authenticated = 1;
                                userDetails.Add("username", userLooking);
                                userDetails.Add("password", password);
                                userDetails.Add("highscore", Convert.ToInt32(_dictVals["highscore"]));
                                userLooking = null;
                                password = null;
                                return;
                            }
                        }
                    }
                }
                failureReason = "NoUser";
                authenticated = 0;
                userLooking = null;
                password = null;
            }
            else if (waitingForCreate)
            {
                if (_keys != null)
                {
                    foreach (string _key in _keys)
                    {
                        Dictionary<string, object> _dictVals = (Dictionary<string, object>)_dict[_key];
                        if (_key == userDetails["username"].ToString())
                        {
                            failureReason = "Username";
                            foundUser = 1;
                            return;
                        }
                    }
                }
                foundUser = 0;
            }
            else
            {
                Dictionary<string, int> _scores = new Dictionary<string, int>();
                if (_keys != null)
                {
                    foreach (string _key in _keys)
                    {
                        Dictionary<string, object> _dictVals = (Dictionary<string, object>)_dict[_key];
                        _scores.Add(_key, Convert.ToInt32(_dictVals["highscore"]));
                    }
                    scores = _scores.ToList();

                    scores.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));
                }
            }
            
        }
        else
        {
            if (_keys != null)
                foreach (string _key in _keys)
                {
                    Debug.Log(_key + " = " + _dict[_key]);
                }
        }
    }

    private static void GetFailHandler(Firebase sender, FirebaseError err)
    {
        //Debug.LogError("[ERR] Get from key: <" + sender.FullKey + ">,  " + err.Message + " (" + (int)err.Status + ")");
    }

    private static void SetOKHandler(Firebase sender, DataSnapshot snapshot)
    {
        //Debug.Log("[OK] Set from key: <" + sender.FullKey + ">");
    }

    private static void SetFailHandler(Firebase sender, FirebaseError err)
    {
        //Debug.LogError("[ERR] Set from key: <" + sender.FullKey + ">, " + err.Message + " (" + (int)err.Status + ")");
    }

    private static void UpdateOKHandler(Firebase sender, DataSnapshot snapshot)
    {
        updateSuccess = 1;
        //Debug.Log("[OK] Update from key: <" + sender.FullKey + ">");
    }

    private static void UpdateFailHandler(Firebase sender, FirebaseError err)
    {
        failureReason = "Failure";
        updateSuccess = 0;
        //Debug.LogError("[ERR] Update from key: <" + sender.FullKey + ">, " + err.Message + " (" + (int)err.Status + ")");
    }

    private static void DelOKHandler(Firebase sender, DataSnapshot snapshot)
    {
        //Debug.Log("[OK] Del from key: <" + sender.FullKey + ">");
    }

    private static void DelFailHandler(Firebase sender, FirebaseError err)
    {
        //Debug.LogError("[ERR] Del from key: <" + sender.FullKey + ">, " + err.Message + " (" + (int)err.Status + ")");
    }

    private static void PushOKHandler(Firebase sender, DataSnapshot snapshot)
    {
        //Debug.Log("[OK] Push from key: <" + sender.FullKey + ">");
    }

    private static void PushFailHandler(Firebase sender, FirebaseError err)
    {
        //Debug.LogError("[ERR] Push from key: <" + sender.FullKey + ">, " + err.Message + " (" + (int)err.Status + ")");
    }

    private static void GetRulesOKHandler(Firebase sender, DataSnapshot snapshot)
    {
        //Debug.Log("[OK] GetRules");
        //Debug.Log("[OK] Raw Json: " + snapshot.RawJson);
    }

    private static void GetRulesFailHandler(Firebase sender, FirebaseError err)
    {
        //Debug.LogError("[ERR] GetRules,  " + err.Message + " (" + (int)err.Status + ")");
    }

    private static void GetTimeStamp(Firebase sender, DataSnapshot snapshot)
    {
        long timeStamp = snapshot.Value<long>();
        DateTime dateTime = Firebase.TimeStampToDateTime(timeStamp);

        //Debug.Log("[OK] Get on timestamp key: <" + sender.FullKey + ">");
        //Debug.Log("Date: " + timeStamp + " --> " + dateTime.ToString());
    }

    public static void ConnectToFireBase()
    {
        firebase = Firebase.CreateNew("https://cyberstrike.firebaseio.com/");

        // Init callbacks
        firebase.OnGetSuccess += GetOKHandler;
        firebase.OnGetFailed += GetFailHandler;
        firebase.OnSetSuccess += SetOKHandler;
        firebase.OnSetFailed += SetFailHandler;
        firebase.OnUpdateSuccess += UpdateOKHandler;
        firebase.OnUpdateFailed += UpdateFailHandler;
        firebase.OnPushSuccess += PushOKHandler;
        firebase.OnPushFailed += PushFailHandler;
        firebase.OnDeleteSuccess += DelOKHandler;
        firebase.OnDeleteFailed += DelFailHandler;

        // Get child node from firebase, if false then all the callbacks are not inherited.
        Firebase temporary = firebase.Child("temporary", true);
        Firebase lastUpdate = firebase.Child("lastUpdate");

        lastUpdate.OnGetSuccess += GetTimeStamp;

        // Make observer on "last update" time stamp
        FirebaseObserver observer = new FirebaseObserver(lastUpdate, 1f);
        observer.OnChange += (Firebase sender, DataSnapshot snapshot) =>
        {
            //Debug. Log("[OBSERVER] Last updated changed to: " + snapshot.Value<long>());
        };
        observer.Start();

        // Create a FirebaseQueue
        firebaseQueue = new FirebaseQueue(true, 3, 1f); // if _skipOnRequestError is set to false, queue will stuck on request Get.LimitToLast(-1).
    }

    public static Dictionary<string, object> GetUserLoggedIn()
    {
        return userDetails;
    }

    public static void CreateUser(string _username, string _password)
    {
        waitingForCreate = true;
        failureReason = "";
        foundUser = -1;
        updateSuccess = -1;
        userDetails.Clear();
        userDetails.Add("username", _username);
        userDetails.Add("password", _password);
        userDetails.Add("highscore", 0);
        //userDetails.Add("email", _email);
        firebaseQueue.AddQueueGet(firebase.Child("users", true));
    }

    public static void Login(string _username, string _password)
    {
        authenticated = -1;
        failureReason = "";
        waitingForLogin = true;
        userLooking = _username;
        password = _password;
        userDetails.Clear();
        firebaseQueue.AddQueueGet(firebase.Child("users", true));
    }

    public static void UpdateUsersScore(string _username, string _password, int _newScore)
    {
        firebaseQueue.AddQueueUpdate(firebase.Child("users", true), "{\"" + _username + "\": { \"password\": \"" + _password + "\", \"highscore\": "+_newScore+ "} }");
        updateSuccess = -1;
        failureReason = "";
    }

    public static void GetScores()
    {
        scores.Clear();
        firebaseQueue.AddQueueGet(firebase.Child("users", true));
    }

    public static string Authenticated()
    {
        //if waiting to be logged in
        if (waitingForLogin)
        {
            //if we have been authenticated, stop waiting and return success
            if (authenticated == 1)
            {
                waitingForLogin = false;
                return "Success";
            }
            //else if something failed, stop waiting and return failure
            else if (authenticated == 0)
            {
                waitingForLogin = false;
                return failureReason;
            }
        }
        //if waiting for our account to be made
        else if (waitingForCreate)
        {
            //if the account has been made, stop waiting and return success
            if (updateSuccess == 1)
            {
                waitingForCreate = false;
                return "Success";
            }
            //else if the account making failed, stop waiting and return failure
            else if (updateSuccess == 0)
            {
                userDetails.Clear();
                waitingForCreate = false;
                return failureReason;
            }
            //if we didn't find a user that conflicted then 
            else if(foundUser == 0)
            {
                firebaseQueue.AddQueueUpdate(firebase.Child("users", true), "{\"" + userDetails["username"] + "\": { \"password\": \"" + userDetails["password"] + "\", \"highscore\": 0} }");
                foundUser = -1;
            }
            //if we found a user that conflicts with our current user, stop waiting and return failure
            else if (foundUser == 1)
            {
                userDetails.Clear();
                waitingForCreate = false;
                return failureReason;
            }
        }
        else if(!waitingForCreate && userDetails.Count > 0)
        {
            return "Success";

        }
        return "";
    }

    public static List<KeyValuePair<string, int>> GetLoadedScores()
    {
        return scores;
    }

    public static bool IsConnected()
    {
        return (firebase != null);
    }

    public static void SetLayerRecursively(GameObject _obj, int _newLayer)
    {
        if(_obj == null)
            return;

        _obj.layer = _newLayer;

        foreach(Transform _child in _obj.transform)
        {
            if(_child == null)
                continue;

            SetLayerRecursively(_child.gameObject, _newLayer);
        }
    }
}
