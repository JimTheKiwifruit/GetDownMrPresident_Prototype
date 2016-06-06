using UnityEngine;
using UnityStandardAssets.ImageEffects;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

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

	public float roundTime = 90;
	public Text roundTimeText;

	public PlayerMovement disableAssassin;
	public PlayerTakedown disableAssassinTakedown;
	public PlayerMovement disableBodyguard;

	RoundState state = RoundState.Idle;
    
    GameScore gameScore;


	void Awake() {
		main = this;
		//roundTimeText.text = "" + Mathf.Round(roundTime);
    }

	void Start() {
		disableAssassin.enabled = false;
		disableAssassinTakedown.enabled = false;
		disableBodyguard.enabled = false;
		StartCoroutine(StartHideMainTitle());
        
        gameScore = GameObject.FindGameObjectWithTag("Environment").GetComponent<GameScore>();
    }

	void Update() {
		//if (state == RoundState.Playing) {
		//	roundTime -= Time.deltaTime;
		//	if (roundTime < 0) {
		//		roundTime = 0;
		//		OutOfTime();
		//	}
		//	roundTimeText.text = "" + Mathf.Round(roundTime);
		//}
	}

    public RoundState GetRoundState() {
        return state;
    }


	IEnumerator StartHideMainTitle() {
		while (mainTitle.GetCurrentAnimatorStateInfo(0).normalizedTime < 1) {
			yield return null;
		}
		mainTitle.gameObject.SetActive(false);
		blur.enabled = false;
		disableAssassin.enabled = true;
		disableAssassinTakedown.enabled = true;
		disableBodyguard.enabled = true;
		musicSource.Play();
		state = RoundState.Playing;

        gameScore.newRoundStarted();
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
        gameScore.assassinWins();

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
        gameScore.bodyguardWins();

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
        gameScore.bodyguardWins();

        yield return new WaitForSeconds(3);
		NewRound();
	}

	public void NewRound() {
		SceneManager.LoadScene(0);

        gameScore.newRoundStart();
    }

}
