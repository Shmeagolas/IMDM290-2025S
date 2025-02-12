using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class Heartbeat : MonoBehaviour
{
    private float beatInterval = 1.5f;
    private Heart4[] hearts;
    private int numHearts = 9, numPoints = 200;
    [SerializeField] private Color cMin, cMax, CBeat;
    void Start()
    {
        hearts = new Heart4[numHearts];
        for(int i = 0; i < numHearts; i++)
        {
            hearts[i] = gameObject.AddComponent<Heart4>();
            hearts[i].MakeHeart(numPoints * i + 100, 2 * i + 2.5f, 2 * i + .5f, 10f, cMin, cMax, CBeat);
        }
        StartCoroutine(Pulse());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Pulse()
    {
        while(true)
        {
            yield return new WaitForSeconds (beatInterval - .5f);
            for(int i = 0; i < numHearts; i++)
            {
                hearts[i].Beat(.3f, 1.25f);
            }
            yield return new WaitForSeconds (.5f);
            for(int i = 0; i < numHearts; i++)
            {
                hearts[i].Beat(.2f, 1.2f);
            }
        }
    }
}
