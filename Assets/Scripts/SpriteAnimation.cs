using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] List<Sprite> sprites;
    [SerializeField] float m_speedAnimation;
    [SerializeField] bool m_loop;
    void Start()
    {
        if (m_loop)
        {
            StartCoroutine("animateThatShit");
        }
        
    }
    public void AnimateOnce()
    {
        StartCoroutine("Animate");
    }
    public void TimedAnimation(float _duration)
    {
        StartCoroutine("TimedAnimate",_duration);
    }


    IEnumerator TimedAnimate(float _duration)
    {
        float timer = _duration;
            foreach (var sprite in sprites)
            {
                GetComponent<SpriteRenderer>().sprite = sprite;
                yield return new WaitForSeconds(m_speedAnimation);
                timer -= Time.deltaTime;
                if (timer <= 0) yield return null;
            }       
    }

    IEnumerator Animate()
    {
        foreach (var sprite in sprites)
        {
            GetComponent<SpriteRenderer>().sprite = sprite;
            yield return new WaitForSeconds(m_speedAnimation);
        }
    }
    IEnumerator animateThatShit()
    {
        while (true)
        {
            foreach (var sprite in sprites)
            {
                GetComponent<SpriteRenderer>().sprite = sprite;
                yield return new WaitForSeconds(m_speedAnimation);
            }
        }
    }
}
