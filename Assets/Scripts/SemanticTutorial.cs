using System.Collections;
using System.Collections.Generic;

using Niantic.ARDK;
using Niantic.ARDK.AR;
using Niantic.ARDK.Extensions;
using Niantic.ARDK.AR.ARSessionEventArgs;
using Niantic.ARDK.AR.Configuration;
using Niantic.ARDK.AR.Awareness;
using Niantic.ARDK.AR.Awareness.Semantics;

using UnityEngine;
using UnityEngine.UI;

using Niantic.ARDK.Helpers;

public class SemanticTutorial : MonoBehaviour
{
    public Material _shaderMaterial;

    Texture2D _semanticTexture;

    public ARSemanticSegmentationManager _semanticManager;

    ARVideoFeed _videoFeed;
    void Start()
    {
        //add a callback for catching the updated semantic buffer
        _semanticManager.SemanticBufferUpdated += OnSemanticsBufferUpdated;
        //on initialisation
        ARSessionFactory.SessionInitialized += OnSessionInitialized;
    }

    private void OnSessionInitialized(AnyARSessionInitializedArgs args)
    {
        Resolution resolution = new Resolution();
        resolution.width = Screen.width;
        resolution.height = Screen.height;
        ARSessionFactory.SessionInitialized -= OnSessionInitialized;

        _videoFeed = new ARVideoFeed(
        args.Session,
        resolution);
    }

    private void OnSemanticsBufferUpdated(ContextAwarenessStreamUpdatedArgs<ISemanticBuffer> args)
    {

        //get the current buffer
        ISemanticBuffer semanticBuffer = args.Sender.AwarenessBuffer;
        //get the index for sky
        int channel = semanticBuffer.GetChannelIndex("ground");

        semanticBuffer.CreateOrUpdateTextureARGB32(
                ref _semanticTexture, channel
            );
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        //pass in our texture
        //Our Depth Buffer
        _shaderMaterial.SetTexture("_SemanticTex", _semanticTexture);
        _shaderMaterial.SetTexture("_CameraTex", _videoFeed.GPUTexture);


        //pass in our transform
        _shaderMaterial.SetMatrix("_semanticTransform", _semanticManager.SemanticBufferProcessor.SamplerTransform);

        //blit everything with our shader
        Graphics.Blit(source, destination, _shaderMaterial);
    }
}