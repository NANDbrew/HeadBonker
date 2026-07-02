using UnityEngine;
using HarmonyLib;
using UnityEngine.Networking;
using System.Collections;
using System.IO;

namespace HeadBonker
{
    internal class HeadBonker : MonoBehaviour
    {
        public float threshold = 5.0f;
        Rigidbody body;
        public PlayerCrouching crouching;
        AudioSource sound;

        public void Awake()
        {
            body = gameObject.GetComponent<Rigidbody>() ?? gameObject.AddComponent<Rigidbody>();
            body.isKinematic = true;
            GetComponent<Collider>().isTrigger = true;
            sound = gameObject.AddComponent<AudioSource>();

            StartCoroutine(LoadAudioclip());
        }

        private IEnumerator LoadAudioclip()
        {
            string path = Path.Combine("file://", Directory.GetParent(Plugin.instance.Info.Location).FullName, "bonk.wav");
            Debug.Log("path = " + path);
            var req = UnityWebRequestMultimedia.GetAudioClip(path, AudioType.WAV);
            yield return req.SendWebRequest();
            sound.clip = DownloadHandlerAudioClip.GetContent(req);

        }
        private IEnumerator WaitForWakeup()
        {
            yield return new WaitUntil(() => !GameState.sleeping);
            MouseLook.ToggleMouseLook(true);
        }

        public void OnTriggerEnter(Collider other)
        {
            if (!Refs.charController.enabled) return;
            if (other.GetComponent<GPButtonSailPusher>() is GPButtonSailPusher pusher)
            {
                Rigidbody sailRigidbody = (Rigidbody)AccessTools.Field(pusher.GetType(), "body").GetValue(pusher);
                float colVelocity = (sailRigidbody.GetPointVelocity(transform.position) - body.velocity).magnitude;

                if (colVelocity > threshold)
                {
                    //NotificationUi.instance.ShowNotification("BONK!!");

                    AccessTools.Field(crouching.GetType(), "crouching").SetValue(crouching, true);
                    sound.volume = Mathf.InverseLerp(threshold * 0.8f, threshold * 3, colVelocity);
                    sound.Play();

                    if (colVelocity > threshold * 3.5f)
                    {
                        Sleep.instance.FallAsleep();
                        StartCoroutine(WaitForWakeup());
                        return;
                    }

                    PlayerNeeds.alcohol = Mathf.Max(colVelocity * 3, PlayerNeeds.alcohol);
                }

                //NotificationUi.instance.ShowNotification("collision velocity = " + colVelocity + "\nvolume = " + sound.volume);
            }
        }

    }

}
