using System.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScoreTextAnimator : MonoBehaviour
{
    public float waitBetween;
    public float waitEnd;

    public int jumpItemCount;
    int currentItem;

    public bool reverse;

    public bool alwaysAnimateNext = true;
    public bool alwaysReverseOnEnd;
    public bool alwaysUpdateList;

    List<Animator> animators;

    void Start()
    {
        UpdateAnimatorList();
    }

    public IEnumerator Play()
    {
        while (true)
        {
            if (!alwaysAnimateNext)
            {
                yield return new WaitForEndOfFrame();

                continue;
            }

            if (alwaysUpdateList)
            {
                UpdateAnimatorList();
            }

            IEnumerable<Animator> tempAnimators = animators;

            if (reverse)
            {
                tempAnimators.Reverse();
            }

            foreach (Animator animator in tempAnimators)
            {
                if (currentItem == System.Int32.MaxValue)
                {
                    currentItem = 0;
                }

                currentItem++;

                if (jumpItemCount + 1 != 0 && currentItem % (jumpItemCount + 1) != 0)
                {
                    continue;
                }

                animator.SetTrigger("DoAnimation");

                if (!Mathf.Approximately(waitBetween, 0))
                {
                    yield return new WaitForSeconds(waitBetween);
                }
            }
            
            yield return new WaitForSeconds(waitEnd);

            if (alwaysReverseOnEnd)
            {
                reverse = !reverse;
            }
        }
    }

    public void UpdateAnimatorList()
    {
        animators = GetComponentsInChildren<Animator>().ToList();
    }
    
    public void SetVisible(bool visible)
    {
        Component[] texts = gameObject.GetComponentsInChildren(typeof(TextMeshPro));

        foreach (Component text in texts)
        {
            TextMeshPro c = (TextMeshPro) text;
            c.enabled = visible;
        }
    }

    public void startAnimation()
    {
        StartCoroutine(Play());
    }
}