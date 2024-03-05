using System;
using System.Collections;
using System.Collections.Generic;
using Firebase;
using UnityEngine;
using Firebase.Auth;
using TMPro;

public class FirebaseAuthManager : MonoBehaviour
{
    public FirebaseAuth auth;
    private FirebaseUser user;

    public string userID => user?.UserId;

    public event Action<bool> LogChangeEvent;
    public event Action CreatIDEvent;
    public event Action<Exception> LogErrorEvent;
    
    public void InitSet()
    {
        
        auth = FirebaseAuth.DefaultInstance;
        auth.StateChanged += OnChanged;
        
        
        if(auth.CurrentUser != null)
            LogOut();
    }
    

    private void OnChanged(object sender, EventArgs e)
    {
        if (auth.CurrentUser != user)
        {
            bool signed = (auth.CurrentUser != user && auth.CurrentUser != null);
            Debug.Log(signed);
            if(!signed)
            {
                Debug.Log("로그아웃");
                LogChangeEvent?.Invoke(false);
            }

            user = auth.CurrentUser;
            
            if (signed)
            {
                Debug.Log("로그인");
                LogChangeEvent?.Invoke(true);
            }
        }
    }

    public void Create(string email, string password)
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                OnLogError(task.Exception);
                return;
            }

            Debug.Log("create");
            FirebaseUser newUser = task.Result.User;
            UnityMainThreadDispatcher.Instance().Enqueue(() =>
            {
                CreatIDEvent?.Invoke();
            });
        });
    }

    public void LogIn(string email, string password)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                OnLogError(task.Exception);
                return;
            }
            
            FirebaseUser newUser = task.Result.User;
        });
    }
    
    public void LogOut()
    {
        auth.SignOut();
    }

    private void OnLogError(AggregateException e)
    {
        if (e != null)
        {
            foreach (Exception exception in e.InnerExceptions)
            {
                LogErrorEvent?.Invoke(exception);
            }
        }
    }
}