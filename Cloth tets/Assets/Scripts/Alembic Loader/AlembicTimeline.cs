using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Formats.Alembic.Importer;
using UnityEngine.Formats.Alembic.Timeline;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class AlembicTimeline : MonoBehaviour
{
    public GameObject obj;
    public ExposedReference<AlembicStreamPlayer> expRef;

    public void Play()
    {
        //// Load the Alembic file into memory
        AlembicStreamPlayer abcPlayer = obj.AddComponent<AlembicStreamPlayer>();
        abcPlayer.LoadFromFile(Application.dataPath + "/Resources/file.abc");
        AlembicStreamSettings settings = new AlembicStreamSettings();
        settings.ScaleFactor = 1f;
        abcPlayer.Settings = settings;

        // Create a timeline
        PlayableDirector director = obj.AddComponent<PlayableDirector>();
        TimelineAsset timeline = ScriptableObject.CreateInstance<TimelineAsset>();

        AlembicTrack track = timeline.CreateTrack<AlembicTrack>("abc");
        TimelineClip clip = track.CreateDefaultClip();
        AlembicShotAsset shotAsset = new AlembicShotAsset();
        clip.duration = abcPlayer.MediaDuration;
        clip.asset = shotAsset;
        expRef.defaultValue = abcPlayer;
        shotAsset.StreamPlayer = expRef;

        director.playableAsset = timeline;

        // Preview the timeline
        director.Play();
    }


}
