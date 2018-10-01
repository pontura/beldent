using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;

public class Data : MonoBehaviour
{
	public builds build;
	public enum builds
	{
		RELEASE,
		DEBUG
	}

    const string PREFAB_PATH = "Data";    
    static Data mInstance = null;
	public Customizer customizer;
	public int score;

    public static Data Instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = FindObjectOfType<Data>();
            }
            return mInstance;
        }
    }
    public string currentLevel;
    public void LoadLevel(string aLevelName)
    {
        this.currentLevel = aLevelName;
        SceneManager.LoadScene(aLevelName);
    }
    void Awake()
    {
        if (!mInstance)
            mInstance = this;
        else
        {
            Destroy(this.gameObject);
            return;
        }
		customizer = GetComponent<Customizer> ();
        DontDestroyOnLoad(this.gameObject);
    }

}
