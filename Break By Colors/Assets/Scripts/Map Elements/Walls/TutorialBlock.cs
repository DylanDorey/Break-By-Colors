using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBlock : MonoBehaviour
{
    public Color ThisWallColor;

    private TutorialWall _wallParent;

    private PlayerData player;

    private void Start()
    {
        _wallParent = transform.parent.GetComponent<TutorialWall>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject.GetComponent<PlayerData>();

            if (_wallParent.willMatch)
            {
                if (player.GetTargetColor() == ThisWallColor)
                {
                    MatchWall();
                }
                else
                {
                    PlayerController.Instance.ResetPlayerPosition();
                    _wallParent.Track.transform.position += new Vector3(0f, 0f, 45f);

                    if (_wallParent.PreviousTrack != null)
                    {
                        player.SetNewTargetColor(_wallParent.PreviousTrack.NextTargetColor);
                    }
                    else
                    {
                        player.SetNewTargetColor(Color.blue);
                    }
                    //transform.parent.transform.parent.transform.position = Vector3.zero;
                }
            }
            else
            {
                PlayerController.Instance.ResetPlayerPosition();
                _wallParent.Track.transform.position += new Vector3(0f, 0f, 45f);

                if (_wallParent.PreviousTrack != null)
                {
                    player.SetNewTargetColor(_wallParent.PreviousTrack.NextTargetColor);
                }
                else
                {
                    player.SetNewTargetColor(Color.blue);
                }
            }
        }
    }

    private void MatchWall()
    {
        player.plusTwoParticle.Play();
        player.hitParticle.Play();
        player.SetNewTargetColor(_wallParent.NextTargetColor);
        player.AddCurrentScore(_wallParent.ScoreValue);
        AudioManager.Instance.PlayAudio(_wallParent.wallAudioSource, _wallParent.wallBreakSounds[Random.Range(0, _wallParent.wallBreakSounds.Length)], false);
    }
}
