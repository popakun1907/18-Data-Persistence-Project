using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreTableManager : MonoBehaviour
{
    [SerializeField] private GameObject entryPrefab;

    private GameManager gameManager;

    private float offset = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;

        GameObject entryObj;
        Entry entryComp;
        for (int i = 0; i < gameManager.scoreTable.Length; i++)
        {
            entryObj = Instantiate(entryPrefab);

            entryComp = entryObj.GetComponent<Entry>();
            entryComp.entryName.text = gameManager.scoreTable[i].name;
            entryComp.score.text = gameManager.scoreTable[i].score.ToString();

            entryObj.transform.parent = gameObject.transform;
            entryObj.transform.localPosition = new Vector3(0.0f, offset, 0.0f);

            offset -= 30.0f;
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
