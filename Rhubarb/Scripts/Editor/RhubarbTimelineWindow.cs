using System.IO;
using UnityEditor;
using UnityEngine.Timeline;
using UnityEngine;
using UnityEngine.Playables;

namespace FriendlyMonster.RhubarbTimeline
{
    public class RhubarbTimelineWindow : EditorWindow
    {
        private string _rhubarbPath;
        private const string rhubarbPathKey = "Rhubarb_RhubarbPath";

        private PlayableDirector _timeline;
        private RhubarbSprite _rhubarbSprite;
        private AudioSource _audioSource;

        private AudioClip _audioClip;
        private bool _isUseDialog;
        private string _dialogText;
        private bool _isExtendedMouthShapesFoldout;
        private bool _isMouthShapeG = true;
        private bool _isMouthShapeH = true;
        private bool _isMouthShapeX = true;

        [MenuItem("Window/Sequencing/Rhubarb Timeline")]
        private static void OpenWindow()
        {
            GetWindow<RhubarbTimelineWindow>("Rhubarb Timeline");
        }

        private void OnEnable()
        {
            _rhubarbPath = EditorPrefs.GetString(rhubarbPathKey);
        }

        private void OnDisable()
        {
            EditorPrefs.SetString(rhubarbPathKey, _rhubarbPath);
        }

        private void OnGUI()
        {
            if (IsRhubarbPathValid())
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Rhubarb Path", _rhubarbPath);

                if (GUILayout.Button("Edit", GUILayout.Width(50)))
                {
                    _rhubarbPath = null;
                    LocateRhubarb();
                }

                EditorGUILayout.EndHorizontal();

                EditorGUILayout.Separator();

                _timeline = (PlayableDirector) EditorGUILayout.ObjectField("Timeline", _timeline, typeof(PlayableDirector), true);
                _rhubarbSprite = (RhubarbSprite) EditorGUILayout.ObjectField("Rhubarb Sprite", _rhubarbSprite, typeof(RhubarbSprite), true);
                _audioSource = (AudioSource) EditorGUILayout.ObjectField("Audio Source", _audioSource, typeof(AudioSource), true);

                EditorGUILayout.Separator();

                _audioClip = (AudioClip) EditorGUILayout.ObjectField("Audio Clip", _audioClip, typeof(AudioClip), false);
                
                _isUseDialog = EditorGUILayout.Toggle("Use Dialog", _isUseDialog);
                if (_isUseDialog)
                {
                    EditorGUI.indentLevel++;
                    _dialogText = EditorGUILayout.TextField("Dialog Text", _dialogText);
                    EditorGUI.indentLevel--;
                }

                _isExtendedMouthShapesFoldout = EditorGUILayout.Foldout(_isExtendedMouthShapesFoldout, "Extended Mouth Shapes");
                if (_isExtendedMouthShapesFoldout)
                {
                    EditorGUI.indentLevel++;
                    _isMouthShapeG = EditorGUILayout.Toggle("G", _isMouthShapeG);
                    _isMouthShapeH = EditorGUILayout.Toggle("H", _isMouthShapeH);
                    _isMouthShapeX = EditorGUILayout.Toggle("X", _isMouthShapeX);
                    EditorGUI.indentLevel--;
                }

                EditorGUILayout.Separator();

                EditorGUI.BeginDisabledGroup(_timeline == null || _rhubarbSprite == null || _audioSource == null || _audioClip == null);
                if (GUILayout.Button("Generate"))
                {
                    if (_audioClip != null && !IsAudioClipWavOrOgg())
                    {
                        EditorUtility.DisplayDialog("Error", "Rhubarb currently only supports .wav and .ogg files", "OK");
                        _audioClip = null;
                    }
                    else
                    {
                        Generate();
                    }
                }

                EditorGUI.EndDisabledGroup();
            } else
            {
                if (GUILayout.Button("Locate Rhubarb"))
                {
                    LocateRhubarb();
                }
            }
        }

        private bool IsAudioClipWavOrOgg()
        {
            string audioPath = AssetDatabase.GetAssetPath(_audioClip).ToLower();
            return audioPath.EndsWith(".wav") || audioPath.EndsWith(".ogg");
        }

        private void LocateRhubarb()
        {
#if UNITY_EDITOR_WIN
            _rhubarbPath = EditorUtility.OpenFilePanel("Open Rhubarb exe", "", "exe");
    #endif
#if UNITY_EDITOR_OSX
            _rhubarbPath = EditorUtility.OpenFilePanel("Open Rhubarb", "", "");
    #endif
        }

        private bool IsRhubarbPathValid()
        {
            return RhubarbEditorProcess.IsValid(_rhubarbPath);
        }

        private void Generate()
        {
            Undo.RecordObject(_timeline, "Added track");

            CreateAudio();
            CreateRhubarb();

            Undo.PerformRedo();
        }

        private void CreateAudio()
        {
            TimelineAsset timelineAsset = (TimelineAsset) _timeline.playableAsset;
            AudioTrack audioTrack = timelineAsset.CreateTrack<AudioTrack>(null, "Audio Track");
            _timeline.SetGenericBinding(audioTrack, _audioSource);
            TimelineClip clip = audioTrack.CreateClip<AudioPlayableAsset>();
            AudioPlayableAsset asset = (AudioPlayableAsset) clip.asset;
            asset.clip = _audioClip;
            clip.duration = _audioClip.length;
        }

        private void CreateRhubarb()
        {
            TimelineAsset timelineAsset = (TimelineAsset) _timeline.playableAsset;
            RhubarbPlayableTrack track = timelineAsset.CreateTrack<RhubarbPlayableTrack>(null, "Rhubarb Track");
            _timeline.SetGenericBinding(track, _rhubarbSprite);
            string audioPath = Path.Combine(Directory.GetCurrentDirectory(), AssetDatabase.GetAssetPath(_audioClip));
            RhubarbTrack rhubarbTrack = RhubarbEditorProcess.Auto(_rhubarbPath, audioPath, _isUseDialog ? _dialogText : null, _isMouthShapeG, _isMouthShapeH, _isMouthShapeX);

            for (int i = 0; i < rhubarbTrack.keyframes.Count - 1; i++)
            {
                RhubarbKeyframe keyframe = rhubarbTrack.keyframes[i];
                RhubarbKeyframe nextKeyframe = rhubarbTrack.keyframes[i + 1];
                TimelineClip clip = track.CreateClip<RhubarbPlayableClip>();
                clip.start = Rhubarb.FrameToTime(keyframe.frame);
                clip.duration = Rhubarb.FrameToTime(nextKeyframe.frame - keyframe.frame);
                ((RhubarbPlayableClip) clip.asset).template.MouthShape = keyframe.phoneme;
            }
        }
    }
}