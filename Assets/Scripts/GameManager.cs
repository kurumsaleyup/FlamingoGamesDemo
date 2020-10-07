using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public static GameManager gm;
	public static Player player;

	public bool advancing;
	public bool gameOver;

	public Text levelText;
	public Text stageText;
	public GameObject gameOverUI;


	
	public int curStageNr;
	private GameObject curStage;
	public GameObject[] stages;

	private void Awake()
	{
		if (!gm)
		{
			gm = this;
		}
	}

	private void Start()
	{
		curStageNr = 0;
		NextStage();
		RefreshUI();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.R))
		{
			RestartScene();
		}
	}

	public void RestartScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void Advance()
	{
		if (advancing)
		{
			return;
		}
		advancing = true;

		if (curStageNr < stages.Length)
		{
			NextStage();

			Debug.Log("next levela girdim stage:" + curStageNr);
		}
        else
        {
			GameOver();
        }
	}

	public void NextStage()
	{
		curStageNr++;
		if (curStage)
		{
			Destroy(curStage);
		}
		StartCoroutine(SpawnNextStage());
		RefreshUI();
	}

	public void RefreshUI()
	{
		stageText.text = curStageNr.ToString();
	}

	private IEnumerator SpawnNextStage()
	{
		yield return new WaitForSeconds(1f);
		curStage = Instantiate(stages[curStageNr-1]);
		advancing = false;
	}

	public void GameOver()
	{
		gameOver = true;
		gameOverUI.SetActive(true);
	}
}
