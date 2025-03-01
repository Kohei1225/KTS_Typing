using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    /*
    //MainMenuScript MainMenuManager;//メインメニューマネージャー
    //ControlSound BGMOption;//BGMの設定
    //ControlSound ButtonOption;//ボタンの設定
    //ControlSound TypeOption;//タイプ音の設定
    ControlSound MissOption;//ミス音の設定
    ControlSound WhistleOption;//笛音の設定
    ControlSound SEOption;//その他の音の設定
    */

    public float BGMVolume = 0.5f;
    public bool BGMExist = true;
    public float ButtonVolume = 0.2f;
    public bool ButtonExist = true;
    public float TypeVolume = 1f;
    public bool TypeExist = true;
    public float MissVolume = 1f;
    public bool MissExist = true;
    public float WhistleVolume = 0.1f;
    public bool WhistleExist = true;
    public float SEVolume = 1f;
    public bool SEExist = true;

    /*
    // Start is called before the first frame update
    void Start()
    {
        //メインメニューだったら
        if(GameObject.Find("MainMenuManager")){
            MainMenuManager = GameObject.Find("MainMenuManager").GetComponent<MainMenuScript>();
            BGMOption = GameObject.Find("BGMVolume").GetComponent<ControlSound>();
            ButtonOption = GameObject.Find("ButtonVolume").GetComponent<ControlSound>();
            TypeOption = GameObject.Find("TypeVolume").GetComponent<ControlSound>();
            MissOption = GameObject.Find("MissVolume").GetComponent<ControlSound>();
            WhistleOption = GameObject.Find("WhistleVolume").GetComponent<ControlSound>();
            SEOption = GameObject.Find("SEVolume").GetComponent<ControlSound>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.Find("MainMenuManager")){
            if(MainMenuManager.flag == 3){
                //BGM設定の更新
                BGMVolume = BGMOption.Volume;
                BGMExist = BGMOption.Exist;
                //ボタン設定の更新
                ButtonVolume = ButtonOption.Volume;
                ButtonExist = ButtonOption.Exist;
                //タイプ音設定の更新
                TypeVolume = TypeOption.Volume;
                TypeExist = TypeOption.Exist;
                //ミス音設定の更新
                MissVolume = MissOption.Volume;
                MissExist = MissOption.Exist;
                //笛音設定の更新
                WhistleVolume = WhistleOption.Volume;
                WhistleExist = WhistleOption.Exist;
                //他の音の設定
                SEVolume = SEOption.Volume;
                SEExist = SEOption.Exist;
            }
        }

    }
    */
}
