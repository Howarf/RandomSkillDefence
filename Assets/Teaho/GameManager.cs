using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * to do
 * 
 * - @1 define ending condtion  -> (clear)
 * - @1 ending(win,lose) -> ...
 * - @2 skills
 * - @3 ui
 * - @4 mole2, boss script
 * 
 * mole is a gamemanger
 * molegenerator is a gamemanager
 * Scene 전환 -> Clear
 * 
 */

public class GameManager : MonoBehaviour
{
    // define constant number
    public const int MAX_FIRST_BOSS_CAPACITY = 1;       //10stage boss 수
    public const int MAX_FINAL_BOSS_CAPACITY = 1;       //20stage boss 수
    public const int FINAL_STAGE = 20;      //스테이지 갯수
    public const int MIDDLE_STAGE = 10;     //중간 스테이지 (보스몹 나타나는 지점)
    public const int MAX_MOLE_CAPACITY= 100;        //몬스터 100마리 쌓였을 때 Game Over
    public const int TIME_TO_NEXT_STAGE = 20;       //다음 스테이지 넘어가는 시간
    public const int MY_MONEY = 200;                 //유저 재화
    public const int FINALBOSS_REMITE_TIME = 60;    //마지막 보스 잡는 시간
    public const int GOTCH_MONEY = 200;     //뽑기할 때 감소되는 MONEY

    //다른 스크립트에서 게임매니저 상속 받은 것은 MonoBehaviour로 바꾸고 GameManager.manager.~ 로 접근! -> 오류 최소화
    // 싱글톤 <- 단 하나의 객체만 존재하게 만드는 것.
    // Mole -> GameManager
    // MoleGenerator -> GameManager
    // 각각의 GameManager가 다른 객체라서 동기화가 안될뿐더러 말도 모순됨.
    public static GameManager manager = null;
    public attack attack;
    public Image pauseButtonImage;
    public Sprite playImage;
    public Sprite pauseImage;
    // 추후에 쓸 예정

    private bool _gameOver = false;
    private int _remainMoleCount = 0;
    private int _currentMoleCount = 0;
    private int _amountAddedBySkill = 0;
    private int _money = 0;
    [SerializeField] private int _gameWave;
    private float _waveTime;
    public int _remite_wave;
    private bool _bossStat;
    public List<GameObject> mobs;
    public List<GameObject> firstBoss;
    public List<GameObject> finalBoss;
    //총 100마리중 현재 몇마리가 돌아다니고있는지
    public Text stageText, livesText, moneyText, timerText, accelState, status_Dagage, status_Speed;
    public GameObject pause_interface, status, normal_interface;
    public AudioSource bgm_obj;

    public bool gameOver
    {
        get => _gameOver;
    }
    public int gameWave
    {
        get
        {
            return _gameWave;
        }
        set
        {
            _gameWave = value;
            stageText.text = "WAVE" + _gameWave.ToString();
        }
    }
    public int amountAddedBySkill
    {
        get => _amountAddedBySkill;
        set => _amountAddedBySkill = value;
    }
    public bool bossStat
    {
        get => _bossStat;
        set => _bossStat = value;
    }
    public int remite_wave 
    {
        get => _remite_wave;
        set => _remite_wave = value;
    }
    public float waveTime
    {
        get => _waveTime;
        set
        {
            _waveTime = value;
            timerText.text = Mathf.FloorToInt(_waveTime).ToString() + "/" + TIME_TO_NEXT_STAGE.ToString();
        }
    }
    public int remainMoleCount
    {
        get => _remainMoleCount;
        set
        {
            _remainMoleCount = value;
            livesText.text = "Live:"+_remainMoleCount + "/100";
        }
    }
    //stage에서 20마리중 현재 몇마리가 생성되어있는지
    public int currentMoleCount
    {
        get => _currentMoleCount;
        set => _currentMoleCount = value;
    }
    public int money
    {
        get => _money;
        set
        {
            //스킬에서 amountAddedBySkill값을 7~15, 8~16 이런식으로 바꿔줄거고
            //if 스킬의 onFlag가 amountAddedBySkill를 더해주고
            //else 
            _money = value;
            moneyText.text = "Money:" + _money;
        }
    }
    //          Awake 자기가 가진 변수 초기화
    //          Start 다른 클래스 혹은 인스턴스의 변수를 가져올때
    void Awake()
    {
        if (manager == null)
            manager = this;
        else
        {
            print("manager already assigned");
            Application.Quit();
        }
        mobs = new List<GameObject>();
        firstBoss = new List<GameObject>();
        finalBoss = new List<GameObject>();
        Time.timeScale = 1f;
        gameWave = 1;
        money = MY_MONEY + money;
        moneyText.text = "Money:" + money + "";
    }

    //게임 승리, 패배 판단 및 게임 종료
    void GameOver(bool win)
    {
        if (win)
        {
            EndScene();
            _gameOver = true;
        }
        else
        {
            EndScene();
            _gameOver = false;
        }
    }
    void Update()
    {
        //게임 승리, 패배 조건
        //@1
        if (gameWave >= FINAL_STAGE && mobs.Count == 0)
        {
            GameOver(true);
        }
        else if(gameWave > FINAL_STAGE && mobs.Count != 0)
        {
            GameOver(false);
        }
        else if (remainMoleCount > MAX_MOLE_CAPACITY)
        {
            GameOver(false);
        }
        else if(bossStat && gameWave > remite_wave)
        {
            GameOver(false);
        }
        waveTime += Time.deltaTime;
        if(gameWave == FINAL_STAGE)
        {
            timerText.text = Mathf.FloorToInt(_waveTime).ToString() + "/" + FINALBOSS_REMITE_TIME.ToString();
            if (waveTime >= FINALBOSS_REMITE_TIME)
            {
                _currentMoleCount = 0;
                waveTime = 0;
                gameWave++;
            }
        }
        else
        {
            if (waveTime >= TIME_TO_NEXT_STAGE)
            {
                _currentMoleCount = 0;
                waveTime = 0;
                gameWave++;
            }
        }
        status_Dagage.text = "Damage:" + ((attack.damage + attack.additionalDamage)*attack.multipliedDamage);
        status_Speed.text = "Speed:" + ((attack.shootPerSec + attack._additionalShotPerSecond) * attack.delayPercentageByWeapon) + "/s";
    }
    //=======================Button Action=========================
    public void EndScene()
    {
        SceneManager.LoadScene("EndScene");
    }
    public void OnTogglePauseButton()
    {
        if (Time.timeScale == 0) //멈춰있으면
        {
            pauseButtonImage.sprite = pauseImage;
            if (accelState.text == "▶X2 On")
            {
                Time.timeScale = 2f;
            }
            else
            {
                Time.timeScale = 1f; //시작
            }
            pause_interface.SetActive(false);
            normal_interface.SetActive(true);
            status.SetActive(true);
            moneyText.gameObject.SetActive(true);
            livesText.gameObject.SetActive(true);
            bgm_obj.Play();
        }
        else //움직이면
        {
            pauseButtonImage.sprite = playImage;
            Time.timeScale = 0; //멈추기
            pause_interface.SetActive(true);
            normal_interface.SetActive(false);
            status.SetActive(false);
            moneyText.gameObject.SetActive(false);
            livesText.gameObject.SetActive(false);
            bgm_obj.Pause();
        }
    }
    public void OnToggleAcceleration()
    {
        if(Time.timeScale == 1)//기본속도일시 2배
        {
            Time.timeScale = 2f;
            accelState.text = "▶X2 On";
        }
        else if(Time.timeScale == 2)//2배속이면 원래대로
        {
            Time.timeScale = 1f;
            accelState.text = "▶X2 Off";
        }
    }
    public void GoToLOBBY()
    {
        SceneManager.LoadScene("startScene");
    }
    public void EndGame()
    {
        Application.Quit();
    }
}
