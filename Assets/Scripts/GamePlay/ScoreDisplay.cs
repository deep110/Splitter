using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

	private Text m_Text;
	public GamePlayManager gamePlayManager;
	private int oldScore;

	void Start () {
		m_Text = GetComponent<Text>();
		oldScore = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(oldScore!= gamePlayManager.Score){
			m_Text.text = gamePlayManager.Score.ToString();
		}
	}
}
