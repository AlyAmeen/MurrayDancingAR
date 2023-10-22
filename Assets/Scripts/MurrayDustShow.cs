using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MurrayDustShow : MonoBehaviour
{
    public Animator MurrayAnimator;
    public GameObject MurrayCenter;
    public SkinnedMeshRenderer MurrayRenderer;
    public SkinnedMeshRenderer MurrayEyeRenderer;

    public AudioClip Jump;
    public AudioClip GasSound;
    public AudioClip TalkingMurray;

    public GameObject DustExplosion;
    public GameObject DustStorm;
    public GameObject DustStorm2;

    public GameObject OptInCanvas;

    IEnumerator AnimationCour;
    IEnumerator BlinkCour;
    IEnumerator FadeBlackSmokeCour;

    bool isFinished = false;
    Vector3 MurrayStartingPosition;
    // Start is called before the first frame update
    private void Start()
    {
        MurrayStartingPosition = transform.position;
    }
    void OnEnable()
    {
        if (!isFinished)
        {
            MurrayAnimator.gameObject.SetActive(false);
            AnimationCour = DoAnimations();
            MurrayCenter.transform.localScale = new Vector3(0, 0, 0);
            StartCoroutine(AnimationCour);
            transform.position = MurrayStartingPosition;
        }
        else
        {
            DustStorm.gameObject.SetActive(true);
            FadeBlackSmokeCour = FadeBlackFogAnim();
            StartCoroutine(FadeBlackSmokeCour);
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
        if (FadeBlackSmokeCour != null)
            StopCoroutine(FadeBlackSmokeCour);
        DustStorm2.gameObject.SetActive(false);
        DustStorm.gameObject.SetActive(false);
        DustExplosion.gameObject.SetActive(false);
    }
    IEnumerator DoAnimations()
    {
        MurrayCenter.transform.localScale = new Vector3(0, 0, 0);
        DustStorm.transform.localScale = new Vector3(0, 0, 0);
        DustStorm2.transform.localScale = new Vector3(0, 0, 0);
        MurrayAnimator.gameObject.SetActive(true);
        DustExplosion.gameObject.SetActive(true);

        yield return new WaitForSeconds(2);
        MurrayAnimator.enabled = true;
        float timer = 0;
        while (timer < 2)
        {
            MurrayCenter.transform.localScale += new Vector3(1f, 1f, 1) * Time.deltaTime/2f;
            transform.position += transform.forward * Time.deltaTime * 1;
            timer += Time.deltaTime;
            yield return null;
        }
        // GetComponent<AudioSource>().PlayOneShot(Jump);
        //GetComponent<AudioSource>().Play();
        //MurrayAnimator.enabled = true;
        yield return new WaitForSeconds(1.5f);
        DustStorm.transform.localScale = new Vector3(0.2f, 0.3f, 0.3f) / 1.25f;
        DustStorm2.transform.localScale = new Vector3(0.4f, 0.5f, 0.5f) / 1.25f;
        DustStorm2.gameObject.SetActive(true);
        DustStorm.gameObject.SetActive(true);
        timer = 0;
        Color blackColor = new Color(0, 0, 0, 0);
        var main = DustStorm2.GetComponent<ParticleSystem>().main;
        while (timer < 1)
        {
            main.startColor = blackColor;
            blackColor.a += 1 * Time.deltaTime;
            timer += Time.deltaTime;
            Debug.Log("Yup");
            yield return null;
        }

        isFinished = true;
        OptInCanvas.SetActive(true);

        yield return new WaitForSeconds(6);
        MurrayAnimator.SetBool("FinishedTalking", true);
    }
    private void Update()
    {
        //DustStorm.transform.Rotate(new Vector3(0, 0, -10) * Time.deltaTime);
        //DustStorm2.transform.Rotate(new Vector3(0, 0, 10f) * Time.deltaTime);
    }
    IEnumerator FadeBlackFogAnim()
    {
        Color blackColor = new Color(0, 0, 0, 0);
        var main = DustStorm2.GetComponent<ParticleSystem>().main;
        main.startColor = blackColor;
        DustStorm2.gameObject.SetActive(true);
        while (blackColor.a < 1)
        {
            main.startColor = blackColor;
            blackColor.a += 0.5f * Time.deltaTime;
            yield return null;
        }
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
