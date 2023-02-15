using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
public class TypeWriterEffect : MonoBehaviour {

	public float delay;

	public AudioClip[] type;
	
	public AudioSource randomType;

	private string currentText = "";
	
	public float timeChange;

	public GameObject playerHolder;

	public MenuManager pause;

	public ChatLog currentText2;

	int randomClip;
	
	[HideInInspector]
	public bool isEnded;


	// Use this for initialization
	void Awake () 
	{
		playerHolder.SetActive(false);
		isEnded = true;
		StartCoroutine(ShowText());


		//fullText = currentText2.chatLog;
	}

    private void Update()
    {

	
	}

	IEnumerator ShowText()
	{
			

		for (int i = 0; i < currentText2.chatLog.Length; i++) 
			{
				currentText = currentText2.chatLog.Substring(0, i);
				this.GetComponent<TextMeshProUGUI>().text = currentText;
				randomType.clip = type[0];
				yield return new WaitForSeconds(delay);
				randomType.Play();

		}

		yield return new WaitForSeconds(timeChange);

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		playerHolder.SetActive(true);
		pause.EndChat();
		/*imageBg.SetActive(false);
		this.GetComponent<TextMeshProUGUI>().enabled = false;
		isEnded = false;*/
		//Destroy(this.gameObject);
		

	}
}
