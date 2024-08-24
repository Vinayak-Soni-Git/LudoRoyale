using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Firebase;
using Firebase.Extensions;
using Firebase.Auth;
using UnityEngine.UI;
using Google;
using System.Net.Http;
using TMPro;
using UnityEngine.SceneManagement;

public class GoogleLoginScript : MonoBehaviour
{
    public string googleWebClientAPI = "234625823700-pa1vvfk6gamkjefpbln8rf6547e9rmar.apps.googleusercontent.com";

    private GoogleSignInConfiguration configuration;

    Firebase.DependencyStatus dependencyStatus = Firebase.DependencyStatus.UnavailableOther;
    Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;

    public TextMeshProUGUI  userName, userEmail;
    public Image userProfilePic;
    public string imageUrl;
    
    public GameObject LoginScreen, ProfileScreen;
    // Start is called before the first frame update

    void Awake(){
        configuration = new GoogleSignInConfiguration{
            WebClientId = googleWebClientAPI,
            RequestIdToken = true
        };
    }
    void Start()
    {
        initFirebase();
    }

    void initFirebase(){
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
    }
    public void GoogleSignInClick(){
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;
        GoogleSignIn.Configuration.RequestEmail = true;

        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnGoogleAuthenticatedFinished);
    }

    void OnGoogleAuthenticatedFinished(Task<GoogleSignInUser> task){
        if(task.IsFaulted){
            Debug.LogError("Fault");
        }
        else if(task.IsCanceled){
            Debug.LogError("Login Cancel");
        }
        else{
            Firebase.Auth.Credential credential = Firebase.Auth.GoogleAuthProvider.GetCredential(task.Result.IdToken, null);
            auth.SignInWithCredentialAsync(credential).ContinueWithOnMainThread(task =>{
                if(task.IsCanceled){
                    Debug.LogError("SignInWithCredentialAsync was canceled");
                    return;
                }
                if(task.IsFaulted){
                    Debug.LogError("SignInWithCredentialAsync encountered an error: "+task.Exception);
                    return;
                }
                user = auth.CurrentUser;

                userName.text = user.DisplayName;
                userEmail.text = user.Email;

                LoginScreen.SetActive(false);
                ProfileScreen.SetActive(true);
                SceneManager.LoadScene("MainMenuScene");
            });
        }
    }

    private string CheckImageUrl(string url){
        if(!string.IsNullOrEmpty(url)){
            return url;
        }
        return imageUrl;
    }

    IEnumerator LoadImage(string imageUrl){
        WWW www = new WWW(imageUrl);
        yield return www;
        userProfilePic.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
    }
}
