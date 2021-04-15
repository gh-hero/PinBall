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
    void OnCollisionEnter(Collision other)//実は衝突した相手の情報がotherに入っているので、これを利用できる。
    {
        //なので、other.gameObject.Tagと書けば、衝突した相手の持っているタグを取得でき、
        //それをstring型の変数に入れ、後のifで使える。string型なのはタグ名がstring型だから。
        string collidededObjectTag = other.gameObject.tag;

        //接触したオブジェクトにより得点を加算
        if (collidededObjectTag == "SmallStarTag")
        {
            this.scoreText.GetComponent<Text>().text = (this.score += 1).ToString();
        }
        else if (collidededObjectTag == "LargeStarTag")
        {
            this.scoreText.GetComponent<Text>().text = (this.score += 100).ToString();
        }
        else if (collidededObjectTag == "SmallCloudTag")
        {
            this.scoreText.GetComponent<Text>().text = (this.score += 10).ToString();
        }
        else if (collidededObjectTag == "LargeCloudTag")
        {
            this.scoreText.GetComponent<Text>().text = (this.score += 20).ToString();
        }


        //接触したオブジェクトにより得点を加算
        //(tag == "SmallStarTag")このようにあったとして、この「tag」というのはこの.csをアタッチしているゲームオブジェクトのタグ名と同じ名前になる。
        //なので「このオブジェクトのタグ名がSmallStarTagという名前だったら、1点加算しますよ。」という意味になる。
        //で、実際、この書き方だと何かと衝突したときに自分のtagを調べることしか出来ないので、ボール側にこのifを書いても、ballにタグは設定されていないので、
        //tagはかならずnullとなり、どのifも全てfalseになる。なのでこの.csをボールにアタッチして使いたいなら、tagの部分を衝突した相手のtag名にする必要がある。
        //それを可能にするのが、「other.gameObject.tag」である。衝突時に呼ばれる関数であるOncollisionEnterには引数「other」があり、このotherには衝突した相手の情報が入っている。なのでother.gameObject.tagと書けば、衝突した相手のタグを取得できる。
        //if (tag == "SmallStarTag")//tagと書くのは実際不完全で、本当は、gameObject.tagと書くのが適切らしい。//また、this.game.CompareTag("比べたいタグ名")とも書けて、これはjavaのイコールスメソッドのようなもの。軽くて便利らしい。
        //{
        //    this.scoreText.GetComponent<Text>().text = (this.score += 1).ToString();
        //}
        //
        //else if (tag == "LargeStarTag")
        //{
        //    this.scoreText.GetComponent<Text>().text = (this.score += 100).ToString();
        //}
        //else if (tag == "SmallCloudTag")
        //{
        //    this.scoreText.GetComponent<Text>().text = (this.score += 10).ToString();
        //}
        //else if (tag == "LargeCloudTag")
        //{
        //    this.scoreText.GetComponent<Text>().text = (this.score += 20).ToString();
        //}


    }
}
