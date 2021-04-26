using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrightnessRegulator : MonoBehaviour
{
    // Materialクラスの変数を作成。
    // マテリアル＝素材。ゲームオブジェクトの見た目などのパラメータの集まり。
    // ベースカラーなど。テクスチャなども。
    Material myMaterial;

    // Emissionの最小値
    private float minEmission = 0.3f;
    // Emissionの強度
    private float magEmission = 2.0f;
    // 角度
    private int degree = 0;
    //発光速度
    private int speed = 10;
    // ターゲットのデフォルトの色
    //カラーオブジェクト。ユニティの持っているオブジェクト。
    //カラーオブジェクトから白色を呼び出している。スタティックらしい。
    Color defaultColor = Color.white;

    // Use this for initialization
    void Start()
    {

        // タグによって光らせる色を変える
        //光らせているのではなくて、その準備に、変数の更新をしているだけ。
        if (tag == "SmallStarTag")
        {
            this.defaultColor = Color.white;
        }
        else if (tag == "LargeStarTag")
        {
            this.defaultColor = Color.yellow;
        }
        else if (tag == "SmallCloudTag" || tag == "LargeCloudTag")
        {
            this.defaultColor = Color.cyan;
        }

        //オブジェクトにアタッチしているMaterialを取得
        //マテリアルを取得する前に、Rendererを取得している。
        //レンダラーが、マテリアル変数を持っているため。
        //アセットからマテリアルを直接呼ぶのは難しい。
        //今やりたいことは、このコンポーネントを割り当てたオブジェクトのマテリアルを取得したい。
        //Unityの画面の、この.csをアタッチしている「Mesh Renderer」の部分がそれにあたる。
        //また、Mesh RendererはRendererを継承しているので、操作ができる。
        this.myMaterial = GetComponent<Renderer>().material;

        //オブジェクトの最初の色を設定
        //難しい文だが、myMaterial.Set○○で色々とマテリアルに設定ができる。
        //今回は色。第一引数は"_EmissionColor"。シェーダーという側で定義されているらしい。シェーダーがカラーなら"_EmissionColor"と固定。
        //Unityの、これがアタッチされているオブジェクトを見て、Shaderという部分で、Emmisionのcolorが変更される事になる。
        //パソコンがどうやって描画すればいいか、そのコードを持っているのはシェーダー。
        //パラメーターを持っているのがマテリアル。
        //そして、第二引数で、カラー×決めた数値としている。
        //色はRGBで数値化されている。0～255で見ることが多いがここではRGBそれぞれ0～1で扱われている。0が黒、1が白。0.5が灰など。
        //なので、それに掛け算をして増やしているので、明るくなることになる。
        //ここでは、minなので、一番光が小さい時の明るさを設定している。
        myMaterial.SetColor("_EmissionColor", this.defaultColor * minEmission);
    }

    // Update is called once per frame
    void Update()
    {

        if (this.degree >= 0)//あたった瞬間に条件を満たすので、ひかりはじめる。なぜ180なのかはこの際あまり関係ない。180以上になると発動するだけ。
        {
            // 光らせる強度を計算する
            // ここで若干、180度を設定している意味が出ている。
            //また、一番小さい状態の光に光を追加で足していたりして、光を強くしている。
            //ここでは計算して準備しているだけ。
            Color emissionColor = this.defaultColor * (this.minEmission + Mathf.Sin(this.degree * Mathf.Deg2Rad) * this.magEmission);

            // エミッションに色を設定する
            //上で設定した光の強さというか色を、ここで使用している。
            //光らせているのはここ。
            myMaterial.SetColor("_EmissionColor", emissionColor);

            //現在の角度を小さくする
            //180が小さくなっていく式となっているので、ゆっくりと0より小さくなり、光がなくなる。
            //speedの値を調整すれば、光の消えるスピードを調節できる。
            this.degree -= this.speed;
        }
    }

    //衝突時に呼ばれる関数
    void OnCollisionEnter(Collision other)
    {
        //角度を180に設定
        //180度の回転から少しづつ小さくしていくため。
  　　　//回転する動きを再現したいため。
        //光らせるか光らせないかを、角度で判断するため、らしい
        //あまりいいやり方ではない

        //ここに直接光る処理を書くと、あたった瞬間だけ一瞬ひかって終わってしまうので、（2フレームで終わる。）
        //ここでは条件になる文のみを書いている。（なので光らせる処理はフレームをまたげるUpdate（）に書く。）
        this.degree = 180;
    }
}
