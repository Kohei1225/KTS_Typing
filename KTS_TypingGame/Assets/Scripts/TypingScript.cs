using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//タイピングに関する制御をしてほしいクラス。とは言ってもキーが押されたらそれを保存するだけ。
public class TypingScript : MonoBehaviour
{
    GameObject Dic;
    TypingDictionary DicScript;
    GameSystem SystemScript;

    public string typeChar;//打った文字(一文字)

    public bool type;  //タイプされた時の判定用
    bool ex;    //特殊な場合の処理判定用
    bool shift;

    public AudioClip TypeSound;
    public AudioClip MissSound;

    AudioSource audioSource;
    SoundManager soundManager;

    // Start is called before the first frame update
    void Start()
    {
        //一応nullに初期化
        //trueChar = null;
        //typeChar = null;
        Dic = GameObject.Find("Dictionary");

        //ゲームオブジェクトDictionaryのスクリプトを取得する
        DicScript = Dic.GetComponent<TypingDictionary>();
        SystemScript = GameObject.Find("GameManager").GetComponent<GameSystem>();

        audioSource = GetComponent<AudioSource>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        audioSource.volume = soundManager.TypeVolume;
        if(!soundManager.TypeExist)audioSource.volume = 0;

    }

    // Update is called once per frame
    //ここで永遠に文字を打ちたい訳だけどInput.GetKeyDown()を使う方法しか思いつかない
    void Update()
    {
        type = false;//毎回最初は文字が打たれていないからfalseにする
        ex = false;//そもそもまだ打ってないので最初はfalse
        shift  = false;
        //毎回フレームの最初に文字を初期化
        typeChar = "";
        
        //何かキーが打たれたら
        if(Input.anyKeyDown){

            //Shiftキーを押すとそれも間違い判定されるのでそこをなんとかしたい。が、もう眠いのでまた今度
            if(Input.GetKeyDown(KeyCode.LeftShift)||Input.GetKeyDown(KeyCode.RightShift)){
                ex = true;//打っても文字にならないキーは判定しない(Shift単体など)
            }

            //それの他にShiftが押されてるかどうかの判定も行う
            if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)){
                shift = true;
            }

            //今はShift以外を押したとき。単体で打って文字入力に影響があるもの以外を除外する目的
            if(!ex){
                //打ったキーに応じてtypeCharの文字を決める(英語の全角)
                if(Input.GetKeyDown(KeyCode.A) && !shift)typeChar = "Ａ";
                else if(Input.GetKeyDown(KeyCode.B) && !shift)typeChar = "Ｂ";
                else if(Input.GetKeyDown(KeyCode.C) && !shift)typeChar = "Ｃ";
                else if(Input.GetKeyDown(KeyCode.D) && !shift)typeChar = "Ｄ";
                else if(Input.GetKeyDown(KeyCode.E) && !shift)typeChar = "Ｅ";
                else if(Input.GetKeyDown(KeyCode.F) && !shift)typeChar = "Ｆ";
                else if(Input.GetKeyDown(KeyCode.G) && !shift)typeChar = "Ｇ";
                else if(Input.GetKeyDown(KeyCode.H) && !shift)typeChar = "Ｈ";
                else if(Input.GetKeyDown(KeyCode.I) && !shift)typeChar = "Ｉ";
                else if(Input.GetKeyDown(KeyCode.J) && !shift)typeChar = "Ｊ";
                else if(Input.GetKeyDown(KeyCode.K) && !shift)typeChar = "Ｋ";
                else if(Input.GetKeyDown(KeyCode.L) && !shift)typeChar = "Ｌ";
                else if(Input.GetKeyDown(KeyCode.M) && !shift)typeChar = "Ｍ";
                else if(Input.GetKeyDown(KeyCode.N) && !shift)typeChar = "Ｎ";
                else if(Input.GetKeyDown(KeyCode.O) && !shift)typeChar = "Ｏ";
                else if(Input.GetKeyDown(KeyCode.P) && !shift)typeChar = "Ｐ";
                else if(Input.GetKeyDown(KeyCode.Q) && !shift)typeChar = "Ｑ";
                else if(Input.GetKeyDown(KeyCode.R) && !shift)typeChar = "Ｒ";
                else if(Input.GetKeyDown(KeyCode.S) && !shift)typeChar = "Ｓ";
                else if(Input.GetKeyDown(KeyCode.T) && !shift)typeChar = "Ｔ";
                else if(Input.GetKeyDown(KeyCode.U) && !shift)typeChar = "Ｕ";
                else if(Input.GetKeyDown(KeyCode.V) && !shift)typeChar = "Ｖ";
                else if(Input.GetKeyDown(KeyCode.W) && !shift)typeChar = "Ｗ";
                else if(Input.GetKeyDown(KeyCode.X) && !shift)typeChar = "Ｘ";
                else if(Input.GetKeyDown(KeyCode.Y) && !shift)typeChar = "Ｙ";
                else if(Input.GetKeyDown(KeyCode.Z) && !shift)typeChar = "Ｚ";
                else if(Input.GetKeyDown(KeyCode.Minus) && !shift)typeChar = "－";
                else if(Input.GetKeyDown(KeyCode.Exclaim) && !shift)typeChar = "！";                
                else if((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                &&Input.GetKeyDown(KeyCode.Alpha1))typeChar = "！";
                else if(Input.GetKeyDown(KeyCode.Question) && !shift)typeChar = "？";
                else if((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                &&Input.GetKeyDown(KeyCode.Slash))typeChar = "？";
                else if(Input.GetKeyDown(KeyCode.LeftParen) && !shift)typeChar = "（";                
                else if((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                &&Input.GetKeyDown(KeyCode.Alpha8) && !shift)typeChar = "（";
                else if(Input.GetKeyDown(KeyCode.RightParen) && !shift)typeChar = "）";                
                else if((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                &&Input.GetKeyDown(KeyCode.Alpha9))typeChar = "）";
                else if(Input.GetKeyDown(KeyCode.Comma) && !shift)typeChar = "，";
                else if(Input.GetKeyDown(KeyCode.Period) && !shift)typeChar = "．";
                else if(Input.GetKeyDown(KeyCode.Alpha1) && !shift)typeChar = "１";
                else if(Input.GetKeyDown(KeyCode.Alpha2) && !shift)typeChar = "２";
                else if(Input.GetKeyDown(KeyCode.Alpha3) && !shift)typeChar = "３";
                else if(Input.GetKeyDown(KeyCode.Alpha4) && !shift)typeChar = "４";
                else if(Input.GetKeyDown(KeyCode.Alpha5) && !shift)typeChar = "５";
                else if(Input.GetKeyDown(KeyCode.Alpha6) && !shift)typeChar = "６";
                else if(Input.GetKeyDown(KeyCode.Alpha7) && !shift)typeChar = "７";
                else if(Input.GetKeyDown(KeyCode.Alpha8) && !shift)typeChar = "８";
                else if(Input.GetKeyDown(KeyCode.Alpha9) && !shift)typeChar = "９";
                else if(Input.GetKeyDown(KeyCode.Alpha0) && !shift)typeChar = "０";
                else typeChar = "　";


                //マウスの入力じゃなかったら印をつける
                if(!Input.GetMouseButtonDown(0) && !Input.GetMouseButtonDown(1) && !Input.GetMouseButtonDown(2))type = true;//文字が打たれましたよっていう印
            }
        }
        if(SystemScript.start == 1){
            if(DicScript.flag == 1 && soundManager.TypeExist){
                audioSource.volume = soundManager.TypeVolume;
                audioSource.PlayOneShot(TypeSound);
            }
            if(DicScript.flag == -1 && soundManager.MissExist){
                audioSource.volume = soundManager.MissVolume;
                audioSource.PlayOneShot(MissSound);    
            }    
        }

    }
}
