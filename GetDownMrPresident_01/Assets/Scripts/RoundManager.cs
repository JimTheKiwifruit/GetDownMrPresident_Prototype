using UnityEngine;
using UnityStandardAssets.ImageEffects;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

<<<<<<< HEAD
public class RoundManager : MonoBehaviour
{

    public static RoundManager main;
    public enum RoundState { Idle, Playing, AssassinRevealed, PresidentDown, PresidentSaved, TimeOut, ReadyUp };

    public Animator mainTitle;
    public Animator presidentDownTitle;
    public ParticleSystem presidentDownParticles;
    public ColorCorrectionCurves presidentDownColor;
    public AudioSource presidentDownSound;
    public Animator presidentSavedTitle;
    public Animator outOfTimeTitle;
    public Blur blur;
    public AudioSource musicSource;

    public PlayerMovement disableAssassin;
    public PlayerTakedown disableAssassinTakedown;
    public PlayerMovement disableBodyguard;


	public Image redDPad;
	public Image greenDPad;
	public Image redA;
	public Image greenA;

	RoundState state = RoundState.Idle;

    GameScore gameScore;


    public GameObject player;

    void Awake()
    {
        main = this;
    }

    void Start()
    {
		
		redDPad.enabled = false;
		greenDPad.enabled = false;
		redA.enabled = false;
		greenA.enabled = false;
		disableAssassin.enabled = false;
        disableAssassinTakedown.enabled = false;
        disableBodyguard.enabled = false;

        //Is this where we should be choosing the spawn point?
        SelectSpawn();

        StartCoroutine(StartHideMainTitle());

        gameScore = GameObject.FindGameObjectWithTag("Environment").GetComponent<GameScore>();
    }

    void Update()
    {
        
    }

    public RoundState GetRoundState()
    {
        return state;
    }


    IEnumerator StartHideMainTitle()
    {
		

		while (mainTitle.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        {
            yield return null;
        }

//		yield return new WaitForSeconds(3);
//		redDPad.enabled = false;
//		greenDPad.enabled = true;
//		redA.enabled = false;
//		greenA.enabled = true;
//
//		yield return new WaitForSeconds(3);

//		redDPad.enabled = false;
//		greenDPad.enabled = false;
//		redA.enabled = false;
//		greenA.enabled = false;

<<<<<<< Updated upstream
=======
        // General idea of this seems correct. Expect it could be done much more efficiently though.
        // Maybe best to create a new function that then calls this function once assassin selects
        // their starting location?

        //while (true)
        //{
        //    if (Input.GetAxis("DpadX1") > 0)
        //    {
        //        Instantiate(player, new Vector3(13, 0, 13), Quaternion.identity);
        //        break;
        //    }
        //    else if (Input.GetAxis("DpadX2") > 0)
=======
public class RoundManager : MonoBehaviour {

	public static RoundManager main;
	public enum RoundState { Idle, Playing, AssassinRevealed, PresidentDown, PresidentSaved, TimeOut };

	public Animator mainTitle;
	public Animator presidentDownTitle;
	public ParticleSystem presidentDownParticles;
	public ColorCorrectionCurves presidentDownColor;
	public AudioSource presidentDownSound;
	public Animator presidentSavedTitle;
	public Animator outOfTimeTitle;
	public Blur blur;
	public AudioSource musicSource;

	public float roundTime = 60;
	public Text roundTimeText;

	public PlayerMovement disableAssassin;
	public PlayerTakedown disableAssassinTakedown;
	public PlayerMovement disableBodyguard;

	RoundState state = RoundState.Idle;

    public GameObject player;

    void Awake() {
		main = this;
		roundTimeText.text = "" + Mathf.Round(roundTime);
	}

	void Start() {
		disableAssassin.enabled = false;
		disableAssassinTakedown.enabled = false;
		disableBodyguard.enabled = false;
		StartCoroutine(StartHideMainTitle());
	}

	void Update() {
		if (state == RoundState.Playing) {
			roundTime -= Time.deltaTime;
			if (roundTime < 0) {
				roundTime = 0;
				OutOfTime();
			}
			roundTimeText.text = "" + Mathf.Round(roundTime);
		}
	}

	IEnumerator StartHideMainTitle() {
		while (mainTitle.GetCurrentAnimatorStateInfo(0).normalizedTime < 1) {
			yield return null;
		}

        //while (true)
        //{
        //    if(Input.GetAxis("DpadX1") > 0)
        //    {
        //        Instantiate(player, new Vector3(13,0,13), Quaternion.identity);
        //        break;
        //    }else if(Input.GetAxis("DpadX2") > 0)
>>>>>>> master
        //    {
        //        Instantiate(player, new Vector3(-13, 0, -13), Quaternion.identity);
        //        break;
        //    }
        //    else if (Input.GetAxis("DpadY1") > 0)
        //    {
        //        Instantiate(player, new Vector3(-13, 0, 13), Quaternion.identity);
        //        break;
        //    }
        //    else if (Input.GetAxis("DpadY2") > 0)
        //    {
        //        Instantiate(player, new Vector3(13, 0, -13), Quaternion.identity);
        //        break;
        //    }
        //}

<<<<<<< HEAD
>>>>>>> Stashed changes
        mainTitle.gameObject.SetActive(false);
        blur.enabled = false;
        disableAssassin.enabled = true;
        disableAssassinTakedown.enabled = true;
        disableBodyguard.enabled = true;
        musicSource.Play();
        state = RoundState.Playing;

        gameScore.newRoundStarted();
    }

    public IEnumerator SelectSpawn()
    {

        while (state != RoundState.Playing)
        {
            if (Input.GetAxis("DpadX1") > 0)
            {
                Instantiate(player, new Vector3(13, 0, 13), Quaternion.identity);

            }
            else if (Input.GetAxis("DpadX1") < 0)
            {
                Instantiate(player, new Vector3(-13, 0, -13), Quaternion.identity);

            }
            else if (Input.GetAxis("DpadY1") > 0)
            {
                Instantiate(player, new Vector3(-13, 0, 13), Quaternion.identity);

            }
            else if (Input.GetAxis("DpadY1") < 0)
            {
                Instantiate(player, new Vector3(13, 0, -13), Quaternion.identity);

            }
            yield return new WaitForSeconds(4);
        }
    }
    public void AssassinRevealed()
    {

    }

    public void PresidentDown()
    {
        if (state != RoundState.Playing)
            return;
        state = RoundState.PresidentDown;
        presidentDownParticles.Play();
        presidentDownTitle.gameObject.SetActive(true);
        presidentDownTitle.SetTrigger("Go");
        presidentDownColor.enabled = true;
        presidentDownSound.Play();
        musicSource.volume = 0.5f;
        StartCoroutine(EPresidentDown());
    }

    IEnumerator EPresidentDown()
    {
        gameScore.assassinWins();

        yield return new WaitForSeconds(4);
        NewRound();
    }

    public void PresidentSaved()
    {
        if (state != RoundState.Playing)
            return;
        state = RoundState.PresidentSaved;
        blur.enabled = true;
        presidentSavedTitle.SetTrigger("Go");
        StartCoroutine(EPresidentSaved());
    }

    IEnumerator EPresidentSaved()
    {
        gameScore.bodyguardWins();

        yield return new WaitForSeconds(4);
        NewRound();
    }

    public void OutOfTime()
    {
        if (state != RoundState.Playing)
            return;

        state = RoundState.TimeOut;
        blur.enabled = true;
        outOfTimeTitle.SetTrigger("Go");

        StartCoroutine(EOutOfTime());
    }

    IEnumerator EOutOfTime()
    {
        gameScore.bodyguardWins();
        
        yield return new WaitForSeconds(3);
        NewRound();
    }

    public void NewRound()
    {
        SceneManager.LoadScene(1);

        gameScore.newRoundStart();
    }

	public void readyUp(){
		bool assassinReady = false;
		bool securityReady = false;
		while (!assassinReady || !securityReady) {
			if (Input.GetAxis ("Submit") > 0)
				securityReady = true;
			if (Input.GetAxis ("DpadX1") > 0)
				assassinReady = true;
			print ("This is looping");
		}
	}
=======
		mainTitle.gameObject.SetActive(false);
		blur.enabled = false;
		disableAssassin.enabled = true;
		disableAssassinTakedown.enabled = true;
		disableBodyguard.enabled = true;
		musicSource.Play();
		state = RoundState.Playing;
	}

	public void AssassinRevealed() {

	}

	public void PresidentDown() {
		if (state != RoundState.Playing)
			return;
		state = RoundState.PresidentDown;
		presidentDownParticles.Play();
		presidentDownTitle.gameObject.SetActive(true);
		presidentDownTitle.SetTrigger("Go");
		presidentDownColor.enabled = true;
		presidentDownSound.Play();
		musicSource.volume = 0.5f;
		StartCoroutine(EPresidentDown());
	}

	IEnumerator EPresidentDown() {
		yield return new WaitForSeconds(4);
		NewRound();
	}

	public void PresidentSaved() {
		if (state != RoundState.Playing)
			return;
		state = RoundState.PresidentSaved;
		blur.enabled = true;
		presidentSavedTitle.SetTrigger("Go");
		StartCoroutine(EPresidentSaved());
	}

	IEnumerator EPresidentSaved() {
		yield return new WaitForSeconds(4);
		NewRound();
	}

	public void OutOfTime() {
		if (state != RoundState.Playing)
			return;
		state = RoundState.TimeOut;
		blur.enabled = true;
		outOfTimeTitle.SetTrigger("Go");
		StartCoroutine(EOutOfTime());
	}

	IEnumerator EOutOfTime() {
		yield return new WaitForSeconds(3);
		NewRound();
	}

	public void NewRound() {
		SceneManager.LoadScene(0);
	}

>>>>>>> master
}
