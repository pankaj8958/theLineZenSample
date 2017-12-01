using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelGeneratorManager : MonoBehaviour {

    public Button playButton;
    Text playButtonText;
    public Text theGameIntroAndEndText;
    public static LevelGeneratorManager levelGeneratorInstance;
    public List< Transform > gameObjectOnLevelScroll = null;
    public List<Transform> gameObjectOnHurdleScroll = null;
    public Transform playereference;
    enum thePrefabsWeWant
    {
        THESTRAIGHTLINE,
        THESQUARELINE,
        THETRIANGLEONE,
    }
    public bool isGameInitialize = false;
    float straightLineSize = 4.5f;
    float squareLineSize = 5.5f;
    GameObject strailLinePrefab;
    GameObject theSquarePrefab;
    GameObject theTraingleHurdle;
    void Awake()
    {
        if (levelGeneratorInstance == null)
            levelGeneratorInstance = this;
        if (gameObjectOnLevelScroll == null)
            gameObjectOnLevelScroll = new List<Transform>();
        if (gameObjectOnHurdleScroll == null)
            gameObjectOnHurdleScroll = new List<Transform>();
        if (playButton) {
            playButton.onClick.RemoveAllListeners();
            playButton.onClick.AddListener(PlayAndreplayButtonAction);
            playButtonText = playButton.transform.GetChild(0).GetComponent<Text>();
            playButtonText.text = "Start";
        }
        if(theGameIntroAndEndText)
        {
            theGameIntroAndEndText.text = "Zip Line Game";
        }
    }

    // Use this for initialization
    void Start () {
        strailLinePrefab = GetPrefabsFromresources(thePrefabsWeWant.THESTRAIGHTLINE);
        theTraingleHurdle = GetPrefabsFromresources(thePrefabsWeWant.THETRIANGLEONE);
        theSquarePrefab = GetPrefabsFromresources(thePrefabsWeWant.THESQUARELINE);
       // iniTialiezeGamedata();
    }
	
    

	/// <summary>
	/// Initialize gamedata where it all begins.
	/// </summary>
    void iniTialiezeGamedata ()
    {
        for(int i = 0;i < 4;i++)
        {
            createRandomLevelParam(Random.Range(1,3));
        }
    }

	/// <summary>
	/// Gets the prefabs from resources by respect of type.
	/// </summary>
	/// <returns>The prefabs fromresources.</returns>
	/// <param name="type">Type.</param>
    GameObject GetPrefabsFromresources (thePrefabsWeWant type)
    {
        GameObject thePrefabWeWant = null;
        switch (type)
        {
            case thePrefabsWeWant.THESTRAIGHTLINE:
                thePrefabWeWant = Resources.Load("Prefabs/path1") as GameObject;
                break;
            case thePrefabsWeWant.THETRIANGLEONE:
                thePrefabWeWant = Resources.Load("Prefabs/path2") as GameObject;
                break;
            case thePrefabsWeWant.THESQUARELINE:
                thePrefabWeWant = Resources.Load("Prefabs/path3") as GameObject;
                break;
        }
        return thePrefabWeWant;

    }

	/// <summary>
	/// Creates the random level parameter as the path for player.
	/// </summary>
	/// <param name="valueCheck">Value check.</param>
	public void createRandomLevelParam (int valueCheck)
    {
        Vector3 postSet = new Vector3();
        GameObject instantiateNewGameObject = null;
        if(valueCheck == 0)
            valueCheck = Random.Range(1,3);
       // Debug.Log("value check..."+valueCheck);

        switch (valueCheck)
        {
            case 1:
                instantiateNewGameObject = Instantiate(strailLinePrefab) as GameObject;
                instantiateNewGameObject.name = "Line";
                if (gameObjectOnLevelScroll != null && gameObjectOnLevelScroll.Count > 0)
                {
                    postSet = gameObjectOnLevelScroll[gameObjectOnLevelScroll.Count - 1].localPosition;
                    if (gameObjectOnLevelScroll[gameObjectOnLevelScroll.Count - 1].name == "Line")
                        postSet.y += straightLineSize;
                    else
                        postSet.y += squareLineSize;
                }
                else
                    postSet = Vector3.zero;
                instantiateNewGameObject.transform.localPosition = postSet;
                break;
            case 3:
                instantiateNewGameObject = Instantiate(theTraingleHurdle) as GameObject;
                instantiateNewGameObject.name = "Triangle";
                if (gameObjectOnLevelScroll != null && gameObjectOnLevelScroll.Count > 0)
                    postSet = gameObjectOnLevelScroll[gameObjectOnLevelScroll.Count - 1].localPosition;
                else
                    postSet = Vector3.zero;
                instantiateNewGameObject.transform.localPosition = postSet;
                createRandomLevelParam(Random.Range(1, 2));
                break;
            case 2:
                instantiateNewGameObject = Instantiate(theSquarePrefab) as GameObject;
                instantiateNewGameObject.name = "Square";
                if (gameObjectOnLevelScroll != null && gameObjectOnLevelScroll.Count > 0)
                {
                    postSet = gameObjectOnLevelScroll[gameObjectOnLevelScroll.Count - 1].localPosition;
                    if (gameObjectOnLevelScroll[gameObjectOnLevelScroll.Count - 1].name == "Line")
                        postSet.y += straightLineSize;
                    else
                        postSet.y += squareLineSize;
                }
                else
                    postSet = Vector3.zero;
                instantiateNewGameObject.transform.localPosition = postSet;
                break;
        }

        if (valueCheck < 3 && gameObjectOnLevelScroll != null && instantiateNewGameObject)
            gameObjectOnLevelScroll.Add(instantiateNewGameObject.transform);
        else if(gameObjectOnLevelScroll != null && instantiateNewGameObject)
            gameObjectOnHurdleScroll.Add(instantiateNewGameObject.transform);
        valueCheck = Random.Range (0,9);
        if(valueCheck < 5)
        {
            instantiateNewGameObject = Resources.Load("Prefabs/TestObject") as GameObject;
            if(instantiateNewGameObject)
            {
                instantiateNewGameObject = Instantiate(instantiateNewGameObject) as GameObject;
                instantiateNewGameObject.name = "THeFireBall";
                if (gameObjectOnLevelScroll != null && gameObjectOnLevelScroll.Count > 0)
                    postSet = gameObjectOnLevelScroll[gameObjectOnLevelScroll.Count - 1].localPosition;
                else
                    postSet = Vector3.zero;
                instantiateNewGameObject.transform.localPosition = postSet;
                if(gameObjectOnHurdleScroll != null && instantiateNewGameObject)
                    gameObjectOnHurdleScroll.Add(instantiateNewGameObject.transform);
            }
        }
    }


	/// <summary>
	/// Play and replay button action for UI screen.
	/// </summary>
    public void PlayAndreplayButtonAction ()
    {
        isGameInitialize = true;
        iniTialiezeGamedata();
        playButton.gameObject.SetActive(false);
        theGameIntroAndEndText.gameObject.SetActive(false);
    }

    public void assignGameOverdata ()
    {
        for(int i = 0;i < gameObjectOnLevelScroll.Count; i++)
        {
            Destroy(gameObjectOnLevelScroll[i].gameObject);
        }
        gameObjectOnLevelScroll = null;
        if(gameObjectOnHurdleScroll != null && gameObjectOnHurdleScroll.Count > 0)
        {
            for(int i = 0;i < gameObjectOnHurdleScroll.Count; i++)
            {
                if(gameObjectOnHurdleScroll[i])
                    Destroy(gameObjectOnHurdleScroll[i].gameObject);
            }
        }
        gameObjectOnHurdleScroll = null;
        if(theGameIntroAndEndText)
        {
            theGameIntroAndEndText.gameObject.SetActive(true);
            theGameIntroAndEndText.text = "Replay";
        }

        if(playButton)
        {
           playButtonText.text = "Replay";
           playButton.gameObject.SetActive(true);
           playButton.onClick.RemoveAllListeners();
           playButton.onClick.AddListener(resetGame);
        }
        isGameInitialize = false;
        playereference.localPosition = Vector3.zero; 
    }

    public void resetGame()
    {
        Invoke("restartGame",0.2f);
    }

    void restartGame ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  
    }
}
