using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReactionTimeScript : MonoBehaviour
{
    public Text _reactionTimeText;
    private readonly string _goodReaction = "good reaction";
    private readonly string _excellentReaction = "excellent reaction";
    private readonly string _unbelievableReaction = "unbelievable reaction!";

	private void Start ()
    {
		
	}

    private void RemoveReactionTime()
    {
        _reactionTimeText.text = string.Empty;
    }

    public void SetReactionTime(float reactionTime)
    {
        if (reactionTime < 0)
        {
            _reactionTimeText.text = "to soon\n" + reactionTime.ToString("0.000");
        }
        else if (reactionTime < .05f)
        {
            _reactionTimeText.text = _unbelievableReaction + "\n" + reactionTime.ToString("0.000");
        }
        else if (reactionTime < .2f)
        {
            _reactionTimeText.text = _excellentReaction + "\n" + reactionTime.ToString("0.000");
        }
        else if (reactionTime < .35f)
        {
            _reactionTimeText.text = _goodReaction + "\n" + reactionTime.ToString("0.000");
        }
        else
        {
            _reactionTimeText.text = "reaction time" + "\n" + reactionTime.ToString("0.000");
        }

        Invoke("RemoveReactionTime", 4f);
    }
}
