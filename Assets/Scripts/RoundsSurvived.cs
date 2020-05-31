using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RoundsSurvived : MonoBehaviour
{
    public Text survivedRounds;
    
    private void OnEnable()
    {
        StartCoroutine(AnimateText());
    }

    IEnumerator AnimateText()
    {
        survivedRounds.text = "0";
        int round = 0;
        
        yield return new WaitForSeconds(.7f);

        while (round < PlayerStats.roundsSurvived)
        {
            round++;
            survivedRounds.text = round.ToString();
            
            yield return new WaitForSeconds(.05f);
        }
    }

}
