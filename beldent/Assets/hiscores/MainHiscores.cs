using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class MainHiscores : MonoBehaviour {

    public ScoreLine scoreLineNewHiscore;
    public ScoreLine scoreLine;
    public Transform container;

    public Text titleField;
    public Text puestoField;
    public Text field;

    public LeterChanger[] letters;
    public int lettersID;
    public LeterChanger letterActive;
    public int puesto;

	string fileName = "C:\\hiscores\\hiscores.txt";


    public List<Hiscore> arrengedHiscores;
    public List<Hiscore> hiscores;
    [Serializable]
    public class Hiscore
    {
        public string username;
        public int hiscore;       
    }
	void Start () {
        puesto = 1;
        Screen.fullScreen = true;
		LoadHiscores(fileName);
        puestoField.text = "PUESTO " + puesto;
		field.text = Data.Instance.score.ToString ();
		lettersID = -1;
		SetLetterActive(true);
	}
    void Update()
    {
		if (Input.GetKeyUp(KeyCode.UpArrow))
            letterActive.ChangeLetter(false);
		else if (Input.GetKeyUp(KeyCode.DownArrow))
            letterActive.ChangeLetter(true);
		else if (Input.GetKeyUp(KeyCode.W))
			letterActive.ChangeLetter(false);
		else if (Input.GetKeyUp(KeyCode.S))
			letterActive.ChangeLetter(true);
		
		if (Input.GetKeyUp(KeyCode.LeftArrow))
			SetLetterActive(false);
		else if (Input.GetKeyUp(KeyCode.RightArrow))
			SetLetterActive(true, true);
		else if (Input.GetKeyUp(KeyCode.A))
			SetLetterActive(false);
		else if (Input.GetKeyUp(KeyCode.D))
			SetLetterActive(true, true);
		
        else if (Input.GetKeyUp(KeyCode.Q) || Input.GetKeyUp(KeyCode.Space))
            SetLetterActive(true);
        else if (Input.GetKeyUp(KeyCode.Alpha1))
            SetLetterActive(false);
    }
	void SetLetterActive(bool next, bool rightPressed = false)
    {
       
        if (next) lettersID++;
        else lettersID--;
        if (lettersID < 0)
        {
			lettersID = 0;
            return;
        }
        if (lettersID > letters.Length - 1)
        {
			lettersID = letters.Length - 1;
			if(!rightPressed)
          	  Save();
            return;
        }
		foreach (LeterChanger letterChanger in letters)
		{
			letterChanger.SetActivate(false);
			letterChanger.GetComponent<Animation>().Play("letterOff");
		}

        letterActive = letters[lettersID];

        letterActive.SetActivate(true);
        letterActive.GetComponent<Animation>().Play("letterOn");
    }
    private bool yaAgrego;
    void LoadHiscores(string fileName)
    {
            String[] arrLines = File.ReadAllLines(fileName);
            int num = 1;
            foreach (string line in arrLines)
            {
                string[] lines = line.Split("_"[0]);
                Hiscore hiscore = new Hiscore();
                hiscore.username = lines[0];
                hiscore.hiscore = int.Parse(lines[1]);
                hiscores.Add(hiscore);

				if (hiscore.hiscore < Data.Instance.score && !yaAgrego)
                {
                    yaAgrego = true;
                    puesto = num;
                    if (num < 11)
                    {
                        ScoreLine newScoreLine = Instantiate(scoreLineNewHiscore);
						newScoreLine.Init(num, "XXX", Data.Instance.score);
                        newScoreLine.transform.SetParent(container);
                        newScoreLine.transform.localScale = Vector3.one;
                        num++;
                    }                    
                }

                if(num<11)
                {
                    ScoreLine newScoreLine = Instantiate(scoreLine);                
                    newScoreLine.Init(num, hiscore.username, hiscore.hiscore);               
                    newScoreLine.transform.SetParent(container);
                    newScoreLine.transform.localScale = Vector3.one;
                }               

                num++;
            } 
     }
    void Save()
    {
        string username = "";
        foreach (LeterChanger letterChanger in letters)
        {
            string letra = letterChanger.field.text;
            if (letra == "_") letra = " ";
            username += letra;
        }
		SaveNew(fileName, username, Data.Instance.score);
    }
    public void SaveNew(string fileName, string username, int newHiscoreToSave)
    {
        Hiscore newHiscore = new Hiscore();
        newHiscore.username = username;
        newHiscore.hiscore = newHiscoreToSave;
        hiscores.Add(newHiscore);

        arrengedHiscores = OrderByHiscore(hiscores);

        String[] arrLines = new String[hiscores.Count];
        int a = 0;
        foreach (Hiscore hs in arrengedHiscores)
        {
            print(hs);
            arrLines[a] = hs.username + "_" + hs.hiscore;
            a++;
        }
        File.WriteAllLines(fileName, arrLines);
        Invoke("grabaEnd", 0.1f);
		Data.Instance.score = 0;
    }
    void grabaEnd()
    {
		SceneManager.LoadScene("Intro");
    }
    List<Hiscore> OrderByHiscore(List<Hiscore> hs)
    {
        return hs.OrderBy(go => go.hiscore).Reverse().ToList();
    }

}
