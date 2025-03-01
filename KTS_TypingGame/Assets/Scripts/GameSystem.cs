using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//ゲームの進行とかに関するクラス。変数の受け渡しもここで行うのでめちゃくちゃ重要。
public class GameSystem : MonoBehaviour
{
    TypingDictionary DicScript;//Dictionary_Finalクラス
    SoundManager soundManager;

    Animation anim;//終了時のアニメーション
    int animedTime;//終了してからの経過時間
    bool animFlag;//アニメが再生されたかどうかの判定

    AudioSource audioSource;//音源の入れ物(スピーカー的な感じ)
    public AudioClip StartSound;//音源(開始音)
    public AudioClip EndSound;//音源(終了音)
    bool AudioFlag;//音を鳴らす際の目印

    int TimeSum;//ゲームをプレイした総時間(使うのは最後だけ)

    public Text countDownText;  //ゲーム開始前のカウントダウンを表示するテキスト
    public Text helpComment;//カウントダウンの前に表示するテキスト
    public GameObject FinishObject;//終わるときに表示するテキストが入ってるオブジェクト
    public Text FinishMoji;//終わるときに表示するテキスト


    public　float countDownTime = 0;//カウントダウンする用の少数型の変数
    public int bonusLevel;  //連続で正タイプを続けるともらえる延長時間の程度 0:なし  1:1秒  2:3秒  3:5秒(他のスクリプトで使うから必要)
    public int Mode = 0;    //モード。数字によって難易度とかが違う
    public bool finish;            //終わる条件。trueになったら強制終了
    public int start;       //開始中かの判定 -1:開始前   0:開始中   1:開始後    2:終了
    public bool NoTime;            //ノータイムかどうかの判定


    public int TrueTypeSum;//正しくタイプした数
    public int FalseTypeSum;//間違ってタイプした数
    public int TypeSum;     //タイプした総数
    public int TypedMojiSum;//タイプした文字数
    public string ModeName;

    public bool UpdatedLimitFlag;//制限時間を更新したかどうかの判定。(スピード、一発勝負用)


    // Start is called before the first frame update
    void Start()
    {   
        //アニメーションに関する初期設定
        anim = GameObject.Find("Door").GetComponent<Animation>();
        animedTime = 0;
        animFlag = false;

        //オーディオに関する設定
        audioSource = GetComponent<AudioSource>();
        AudioFlag = true;
 
        //ゲームオブジェクトから他のスクリプトのコンポーネントを取得
        DicScript = GameObject.Find("Dictionary").GetComponent<TypingDictionary>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();           

        //開始前のカウントダウン表示用のオブジェクト取得
        countDownText = GameObject.Find("CountDown").GetComponent<Text>();
        helpComment = GameObject.Find("HelpComment").GetComponent<Text>();

        //終了時に表示するテキストのコンポーネントの取得と設定
        FinishObject = GameObject.Find("Finish");
        FinishMoji = FinishObject.GetComponent<Text>();
        FinishMoji.text = "終了ー！！";
        FinishObject.SetActive(false);

        //その他変数の初期化
        finish = false;
        start = -1;
        NoTime = false;
        countDownText.text = "Press Space Key !!";
        helpComment.text = "開始するにはスペースキーを押してください\nゲーム中、Tabキーを押すとやり直せます。";
        TrueTypeSum = 0;
        FalseTypeSum = 0;
        TypeSum = 0;
        UpdatedLimitFlag = false;
        audioSource.volume = soundManager.WhistleVolume;

    }

    // Update is called once per frame
    void Update()
    {   
        if(GameObject.Find("Audio")){
            //普通に音を調整する
            GameObject.Find("Audio").GetComponent<AudioSource>().volume = GameObject.Find("SoundManager").GetComponent<SoundManager>().SEVolume;
            //無音にする時
            if(!GameObject.Find("SoundManager").GetComponent<SoundManager>().SEExist)GameObject.Find("Audio").GetComponent<AudioSource>().volume = 0;
        }
        
        //アニメが再生された瞬間からの時間の経過に応じて
        if(animFlag)animedTime++;
        if(animedTime > 200){
            SceneManager.sceneLoaded += GiveScore;
            if(AudioFlag)audioSource.Stop();
            SceneManager.LoadScene("Result");
        }

        if(Input.GetKeyDown(KeyCode.Tab))Restart();

        //終わりの印がついていればゲームを終わらせる
        if(finish)Finish();

        //スタート前ならスペースキーを押せば始まる
        if(start == -1 && Input.GetKey(KeyCode.Space))start = 0;

        //スペースを押した後のカウントダウンの処理
        if(start == 0){
            helpComment.text = "";
            countDownTime += Time.deltaTime;
            if((int)(countDownTime - countDownTime/10) < 3){
                //カウント３までは数字を表示
                countDownText.text = ""  + (int)(4 - (countDownTime - countDownTime/10));
            }else{
                countDownText.text = "Start!!";
                if(soundManager.WhistleExist)audioSource.PlayOneShot(StartSound);
            }
            if((int)(countDownTime - countDownTime/10) == 4){
                //テキストを隠す
                start = 1;
                countDownText.text = "";
            }
        }


        //カウントダウンが終わって実際にゲームで遊ぶ時の処理
        if(start == 1){
            if(AudioFlag)audioSource.Stop();
            //正しくタイプされた時
            if(DicScript.flag == 1){
                TrueTypeSum++;
            //間違ってタイプした時
            }else if(DicScript.flag == -1){
                FalseTypeSum++;
            }
            //総タイプ数を出す
            TypeSum = FalseTypeSum + TrueTypeSum;   
        }
    }
    
    
    //やり直しメソッド(Tabを押したら)
    void Restart()
    {
        SceneManager.sceneLoaded += GiveMode;
        SceneManager.LoadScene("Typing");
    }

    //値を与えるメソッド(https://note.com/suzukijohnp/n/n050aa20a12f1)
    void GiveMode(Scene next, LoadSceneMode mode)
    {
        //シーン切り替え後のスクリプトを取得
        var SystemManager = GameObject.Find("GameManager").GetComponent<GameSystem>();

        //データを渡す処理
        SystemManager.Mode = this.Mode;

        //イベントから削除
        SceneManager.sceneLoaded -= GiveMode;
    }


    //ゲーム終了メソッド
    void Finish()
    {   
        //効果音を鳴らす
        if(AudioFlag){
            if(soundManager.WhistleExist)audioSource.PlayOneShot(EndSound);
            AudioFlag = false;
        }        
        //アニメーションを再生する
        if(animFlag == false)anim.Play();
        animFlag = true;        
        FinishObject.SetActive(true);
        TypedMojiSum = DicScript.KanaMojiSum;

        //一応他のスクリプトも参照するクラスなので最後に印をつけておく
        start = 2;                
    }


    //値を与えるメソッド(https://note.com/suzukijohnp/n/n050aa20a12f1)
    void GiveScore(Scene next, LoadSceneMode mode){
        //シーン切り替え後のスクリプトを取得
        /*
        var resultManager = GameObject.Find("ResultManager").GetComponent<ResultScript>();

        //データを渡す処理
        resultManager.Mode = this.Mode;
        resultManager.TypeSum = this.TypeSum;
        resultManager.TypedMojiSum = TypedMojiSum;
        //Debug.Log(DicScript.KanaMojiSum);
        resultManager.TrueTypeSum = this.TrueTypeSum;
        resultManager.FalseTypeSum = this.FalseTypeSum;
        resultManager.TypedMojiSum = this.TypedMojiSum;
        resultManager.Mode = this.Mode;
        //TimeSum = (int)TimeManager.countTime;//制限時間を記録(後で使うから)
        resultManager.GameTime = this.TimeSum;
        resultManager.ModeName = this.ModeName;//

        */

        if(Mode == 6){
            //resultManager.PlScore = BattleScript.PlScore;
            //resultManager.EnScore = BattleScript.EnScore;
        }

        //イベントから削除
        SceneManager.sceneLoaded -= GiveScore;
    }

        //値を与えるメソッド(https://note.com/suzukijohnp/n/n050aa20a12f1)
    void GiveSoundOption(Scene next, LoadSceneMode mode)
    {
        //シーン切り替え後のスクリプトを取得
        var SoundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();


        //BGM設定の更新
        SoundManager.BGMVolume = soundManager.BGMVolume;
        SoundManager.BGMExist = soundManager.BGMExist;
        //ボタン設定の更新
        SoundManager.ButtonVolume = soundManager.ButtonVolume;
        SoundManager.ButtonExist = soundManager.ButtonExist;
        //タイプ音設定の更新
        SoundManager.TypeVolume = soundManager.TypeVolume;
        SoundManager.TypeExist = soundManager.TypeExist;
        //ミス音設定の更新
        SoundManager.MissVolume = soundManager.MissVolume;
        SoundManager.MissExist = soundManager.MissExist;
        //笛音設定の更新
        SoundManager.WhistleVolume = soundManager.WhistleVolume;
        SoundManager.WhistleExist = soundManager.WhistleExist;
        //他の音の設定
        SoundManager.SEVolume = soundManager.SEVolume;
        SoundManager.SEExist = soundManager.SEExist;

        //イベントから削除
        SceneManager.sceneLoaded -= GiveSoundOption;
    }
}