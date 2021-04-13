using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreAddition : MonoBehaviour
{

    //得点
    private int score = 0;

    //スコアを表示するテキスト
    private GameObject scoreText;


    // Start is called before the first frame update
    void Start()
    {
        //シーン中のScoreオブジェクトを取得
        this.scoreText = GameObject.Find("Score");
    }

    // Update is called once per frame
    void Update()
    {

    }

    //衝突時に呼ばれる関数
    void OnCollisionEnter(Collision other)
    {
        //接触したオブジェクトにより得点を加算

        if (tag == "SmallStarTag")
        {
            this.scoreText.GetComponent<Text>().text = (this.score += 1).ToString();
        }
        else if (tag == "LargeStarTag")
        {
            this.scoreText.GetComponent<Text>().text = (this.score += 100).ToString();
        }
        else if (tag == "SmallCloudTag")
        {
            this.scoreText.GetComponent<Text>().text = (this.score += 10).ToString();
        }
        else if (tag == "LargeCloudTag")
        {
            this.scoreText.GetComponent<Text>().text = (this.score += 20).ToString();
        }
    }
}
