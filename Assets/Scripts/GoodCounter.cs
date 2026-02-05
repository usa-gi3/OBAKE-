using UnityEngine;
using System.Collections.Generic;

public class GoodCounter : MonoBehaviour
{
    public ChoiceServer choiceServer;

    private int good = 0;
    private int bad = 0;

    /// <summary>
    /// ñºïÎÇ…Ç¢Ç»Ç¢êlÇ…çÏïiÇï‘Ç≥Ç»Ç©Ç¡ÇΩÇÁ Good
    /// </summary>
    public void Judge(
        bool isInMeibo,
        bool returnedWork
    )
    {
        // ñºïÎÇ…Ç¢Ç»Ç¢ Åï ï‘Ç≥Ç»Ç©Ç¡ÇΩ Å® Good
        if (!isInMeibo && !returnedWork)
        {
            good++;
        }
        else
        {
            bad++;
        }
    }

    public int GetGood()
    {
        return good;
    }

    public int GetBad()
    {
        return bad;
    }

    public void ResetScores()
    {
        good = 0;
        bad = 0;
    }
}
