using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Formats.Alembic.Importer;
using UnityEngine.Formats.Alembic.Timeline;
using UnityEngine.Playables;


public class LoadAlembic : MonoBehaviour
{
    private PlayableDirector director;
    public GameObject controlPanel;

    private void Awake()
    {
        director = GetComponent<PlayableDirector>();
        director.played += Director_Played;
        director.stopped += Director_Stopped;
    }

    private void Director_Stopped(PlayableDirector obj)
    {
        controlPanel.SetActive(true);
    }

    private void Director_Played(PlayableDirector obj)
    {
        controlPanel.SetActive(false);
    }
    void LoadAlembicModelFromData(byte[] data)
    {
        // Create a new AlembicStream
        var alembicStream = new AlembicStreamPlayer();
        alembicStream.LoadFromFile(Application.dataPath + "/Resources/file.abc");

        // Create a new GameObject to contain the Alembic data
        var alembicGO = new GameObject("AlembicData");

        // Attach the AlembicStreamPlayer component to the GameObject
        var stream = alembicGO.AddComponent<AlembicStreamPlayer>();

        // Set the AlembicStreamPlayer's AlembicStream property to the loaded Alembic stream
        stream = alembicStream;

        // Call the AlembicStreamPlayer's Play() method to start playing the Alembic animation
        //stream.;
    }
}
