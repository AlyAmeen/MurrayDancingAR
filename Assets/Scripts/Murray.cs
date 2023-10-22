using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Murray : MonoBehaviour
{
    public Animator MurrayAnimator;
    public GameObject Portal;
    public  SkinnedMeshRenderer MurrayRenderer;
    public SkinnedMeshRenderer MurrayEyeRenderer;

    public AudioClip Jump;
    public AudioClip GasSound;
    public AudioClip TalkingMurray;

    public GameObject GasVFX;

    public GameObject OptInCanvas;

    IEnumerator AnimationCour;
    IEnumerator BlinkCour;

    bool isFinished = false;
    // Start is called before the first frame update
    void OnEnable()
    {
        if (!isFinished)
        {
   
            MurrayAnimator.gameObject.SetActive(false);
            AnimationCour = DoAnimations();
            StartCoroutine(AnimationCour);
        }

        BlinkCour = BlinkRout();
        StartCoroutine(BlinkCour);
        MurrayEyeRenderer.enabled = true;
        MurrayRenderer.enabled = true;
    }
    private void OnDisable()
    {
        MurrayRenderer.enabled = false;
        MurrayEyeRenderer.enabled = false;
        StopCoroutine(AnimationCour);
        StopCoroutine(BlinkCour);


    }
    IEnumerator DoAnimations()
    {
        Portal.transform.localScale = new Vector3(0, 0, 0);
        Portal.GetComponent<AudioSource>().Play();

        float timer = 0;
        while (timer < 1)
        {
            Portal.transform.localScale += new Vector3(1.5f, 1.5f, 1) * Time.deltaTime;
            timer += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(1.5f);
        GetComponent<AudioSource>().PlayOneShot(Jump);
        GetComponent<AudioSource>().Play();
        MurrayAnimator.enabled = true;
        MurrayAnimator.gameObject.SetActive(true);
        GasVFX.SetActive(true); 
        timer = 0;
        while (timer < 2)
        {
            transform.position += transform.forward * Time.deltaTime * 1;
            timer += Time.deltaTime;
            Debug.Log("Yup");
            yield return null;
        }

        GetComponent<AudioSource>().PlayOneShot(TalkingMurray);
        isFinished = true;
        OptInCanvas.SetActive(true);

        yield return new WaitForSeconds(6);
        MurrayAnimator.SetBool("FinishedTalking",true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator BlinkRout()
    {

        while (true)
        {
            MurrayEyeRenderer.enabled = false;
            yield return new WaitForSeconds(0.1f);
            MurrayEyeRenderer.enabled = true;
            yield return new WaitForSeconds(Random.Range(0.35f, 1f));
        }

    }
}
