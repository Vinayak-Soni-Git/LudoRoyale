using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Google;
using Firebase.Extensions;

public class SignIN : MonoBehaviour
{

    public string webClientId = "234625823700-pa1vvfk6gamkjefpbln8rf6547e9rmar.apps.googleusercontent.com";

    private GoogleSignInConfiguration configuration;
    Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;
    void initFirebase(){
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
    }
    private void Awake()
    {


        configuration = new GoogleSignInConfiguration
        {
            WebClientId = webClientId,
            RequestIdToken = true, 
        };
    }
    void Start()
    {
        initFirebase();
    }
    public void OnSignIn()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;
        GoogleSignIn.Configuration.RequestEmail = true;

        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(
          OnGoogleAuthenticatedFinished);
    }

    public void OnSignOut()
    {
    
        GoogleSignIn.DefaultInstance.SignOut();
    }

    public void OnDisconnect()
    {

        GoogleSignIn.DefaultInstance.Disconnect();
    }

    void OnGoogleAuthenticatedFinished(Task<GoogleSignInUser> task)
    {
        if (task.IsFaulted)
        {
            using (IEnumerator<System.Exception> enumerator =
                    task.Exception.InnerExceptions.GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    GoogleSignIn.SignInException error =
                            (GoogleSignIn.SignInException)enumerator.Current;
                    FindObjectOfType<Authenticator>().GoogleErrorCallback("Failed  " + error +  " ");

                }
                else
                {
                    FindObjectOfType<Authenticator>().GoogleErrorCallback("Fail");

                }
            }
        }
        else if (task.IsCanceled)
        {
            FindObjectOfType<Authenticator>().GoogleErrorCallback("Cancelled  ");
        }
        else
        {
            Firebase.Auth.Credential credential = Firebase.Auth.GoogleAuthProvider.GetCredential(task.Result.IdToken, null);
            auth.SignInWithCredentialAsync(credential).ContinueWithOnMainThread(task1 =>{
                if(task1.IsCanceled){
                    Debug.LogError("SignInWithCredentialAsync was canceled");
                    return;
                }
                if(task1.IsFaulted){
                    Debug.LogError("SignInWithCredentialAsync encountered an error: "+task1.Exception);
                    return;
                }
                user = auth.CurrentUser;
                FindObjectOfType<Authenticator>().GoogleSuccessCallback(user.UserId, user.Email, user.PhotoUrl.ToString(), user.DisplayName,task.Result.IdToken);
            });
            
        }
    }

    public void OnSignInSilently()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;
  
        GoogleSignIn.DefaultInstance.SignInSilently()
              .ContinueWith(OnGoogleAuthenticatedFinished);
    }


    public void OnGamesSignIn()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = true;
        GoogleSignIn.Configuration.RequestIdToken = false;

  
        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(
          OnGoogleAuthenticatedFinished);
    }
    public void SignInGoogle()
    {
        OnSignIn();
    }


}
