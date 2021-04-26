using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Touch : MonoBehaviour
{
    //タップした指の識別としてtouch.fingerIdが使われる。
    //Idには１～の値が入るらしい。プログラマにはわからない。
    //自分で作成した変数「_fingerId」に、「touch.fingerId」の中に代入する値を入れる。
    //指に振り分けられるidは0以上の整数なので、
    //使われる可能性が無い-1で_fingerIdを初期化。それを後にtouch.fingerIdに代入。
    private int _fingerId1 = -1;//先にはいったほうを1とする。2本まで。
    private int _fingerId2 = -1;

    //HingeJointコンポネートを入れる
    //Hinge Joint は、2 つの リジッドボディ をグループ化し、互いにヒンジで連結されているかのように制約します。
    //ドアに最適ですが、鎖や振り子などをモデル化するのにも使用できます。とのこと。
    //Rigidbody (リジッドボディ) はオブジェクトに物理挙動を可能にするためのメインコンポーネントです。
    //リジッドボディを加えた瞬間から、オブジェクトは重力の影響を受けるようになります。との説明も。
    private HingeJoint myHingeJoint;

    //初期の傾き
    private float defaultAngle = 20;
    //弾いた時の傾き
    private float flickAngle = -20;

    //フリッパーの取得はスタートで。
    // Start is called before the first frame update
    void Start()
    {
        //HingeJointコンポーネント取得
        this.myHingeJoint = GetComponent<HingeJoint>();

        //フリッパーの傾きを設定
        SetAngle(this.defaultAngle);
    }

    // Update is called once per frame
    void Update()
    {
        //touchCountの値は画面に触れている指の数になる。
        //画面に指が触れていない(＝０)時に処理をし続けて重くならないように、
        //if文で実行条件を指で触れている時のみにする。(1以上)
        if (Input.touchCount > 0)
        {

            //触れている指の数を変数に代入
            var touchCount = Input.touchCount;

            //forを使い、触れている指の数だけ回す。
            //指が触れた時に、その指で何をするか処理させるため。
            for (int i = 0; i < touchCount; i++)
            {
                //触れている指の情報を使うため、その情報が格納されたTouchオブジェクトを取得。
                //しかしこれだけでは触れた指の回数分処理をするだけで、どの処理がどの指に対応するか不明となる（多分）
                //なので、それを判別するためにtouchIdをどこかで使う。
                var touch = Input.GetTouch(i);



                //毎回参照するのが面倒なので、変数にしておくだけ
                //この段階で、fingerIdの中身はもう割り振られてる。
                //中身は、押された瞬間に割り振られている。
                //指が触れている間は、Idは変わらない。
                //この↑のforは毎フレーム行われる
                var FI = touch.fingerId;


                

                //さらに、触れている指が今どんな状態なのか、Phaseで取得。
                //その状態に応じて、switchで分岐させ、その時に何をさせたいかを記述する。
                switch (touch.phase)
                {
                    // 画面に指が触れた時(触れている時)に行いたい処理をここに書く
                    case TouchPhase.Began:
                        
                        // _fingerId1に-1(初期値)が割り振られていなければ、
                        // 割り振りたいのでこのifになる
                        if (_fingerId1 == -1)
                        {
                            //初期値の_fingerId1に、FIを割り振る。
                            //これで一本目の指が
                            //_fingerId1に割り振られた。
                            _fingerId1 = FI;

                        } else if (_fingerId2 == -1)//_fingerId1が割り振られているなら、2本目に割り振る。
                        {
                            //同じく、これで2本目の指に割り振られた。
                            _fingerId2 = FI;

                        }//三本目以降は、何もしない。

                        //このFIを代入された「_fingerId1」をどうやって条件に使うのか？
                        //・・・・・・・




                        //このifは、「座標xがスクリーンの2分の1以上の場合」という意味。
                        //このifのtrueが右側の処理、false(else)が左側の処理を書き込む場所となる。
                        if (Input.mousePosition.x >= Screen.width / 2)
                        {



                            if ((_fingerId1 != -1) && (_fingerId2 == -1 ) && (tag == "RightFripperTag"))
                            { 
                                SetAngle(this.flickAngle);
                               
                            }
                            if ((_fingerId1 != -1) && (_fingerId2 != -1) && (tag == "RightFripperTag"))
                                {
                                SetAngle(this.flickAngle);

                            }


                        }
                        else
                        {
                                if ((_fingerId1 != -1) && (_fingerId2 == -1) && (tag == "LeftFripperTag"))
                                {
                                    SetAngle(this.flickAngle);

                                }
                                if ((_fingerId1 != -1) && (_fingerId2 != -1) && (tag == "LeftFripperTag"))
                                {
                                    SetAngle(this.flickAngle);

                                }
                            }



                        break;

                    // 画面上で指が動いたときに行いたい処理をここに書く
                    case TouchPhase.Moved:
                        
                        break;

                    // 指が画面に触れているが動いてはいない時に行いたい処理をここに書く
                    case TouchPhase.Stationary:
                       
                        break;

                    // 画面から指が離れた時に行いたい処理をここに書く
                    case TouchPhase.Ended:


                        //離れたほうの指だけを、初期値にもどしたい。
                        //離れたほうの指を判断したい。
                        if (_fingerId1 == FI)//_fingerId1の中身がFIと同じであるなら、初期化していい
                        {
                            _fingerId1 = -1;//初期化する

                            

                        } else if (_fingerId2 == FI)
                        {
                            _fingerId2 = -1;
                        }


                        //フリッパーを動かす
                        if ((_fingerId1 == -1) && (tag == "RightFripperTag"))
                        {
                            SetAngle(this.defaultAngle);

                        }
                        if ((_fingerId2 == -1) && (tag == "RighttFripperTag"))
                        {
                            SetAngle(this.defaultAngle);

                        }

                        if ((_fingerId1 == -1) && (tag == "LeftFripperTag"))
                        {
                            SetAngle(this.defaultAngle);

                        }
                        if ((_fingerId2 == -1) && (tag == "LeftFripperTag"))
                        {
                            SetAngle(this.defaultAngle);

                        }



                        //今回の書き方は、2本だからOKで、3本以上だと詰む






                        break;

                    // システムがタッチの追跡をキャンセルした時に行いたい処理をここに書く
                    case TouchPhase.Canceled:
                        
                        break;
                    
                        
                }

            }
        }
    }
    
    //フリッパーの傾きを命令するメソッド
    public void SetAngle(float angle)
    {
        JointSpring jointSpr = this.myHingeJoint.spring;
        jointSpr.targetPosition = angle;
        this.myHingeJoint.spring = jointSpr;
    }

}

