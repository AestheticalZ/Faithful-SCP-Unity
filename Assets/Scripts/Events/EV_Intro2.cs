﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EV_Intro2 : MonoBehaviour
{
    public GameObject door1, door2, tri1, tri2, tri3, lightmana, emelight, d1, d2, sci, guard, flask, RerenderProbe1, RerenderProbe2;
    public Transform[] path1, path2, path3;
    public Transform ata1, ata2, ata3, deadlook, TeleportAnchor;
    GameObject objPlayer;
    EV_Puppet_Controller d1_, d2_, sci_, guard_;
    public AudioClip[] Dialogs, Refuse, NewAmbiance, Horror, GeneralSFX, Alarm;
    public AudioClip MusicChange;
    int eventstat = 0;
    float Timer, TimerSecondary=0.1f;
    public float Timer1, Timer2, Timer3, Timer4, Timer5;
    bool ActiveTimer, StopTimer, check2, check3, StopTimer2, ActiveTimer2;
    // Start is called before the first frame update
    void OnEnable()
    {
        objPlayer = GameController.instance.player;
        d1_ = d1.GetComponent<EV_Puppet_Controller>();
        d2_ = d2.GetComponent<EV_Puppet_Controller>();
        sci_ = sci.GetComponent<EV_Puppet_Controller>();
        guard_ = guard.GetComponent<EV_Puppet_Controller>();

        GameController.instance.ChangeMusic(MusicChange);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (eventstat == 0)
        {
            if (tri1.GetComponent<BoxTrigger>().GetState())
            {
                eventstat = 1;
                door2.GetComponent<Object_Door>().DoorSwitch();
                d1_.SetLookAt(objPlayer.transform);
                d2_.SetLookAt(objPlayer.transform);
                guard_.SetRota(objPlayer.transform);
                Timer = Timer1;
                ActiveTimer = true;

            }
        }

        if (check2 == true)
        {
            if (tri2.GetComponent<BoxTrigger>().GetState())
            {
                door1.GetComponent<Object_Door>().DoorSwitch();
                d1_.AnimTrigger(1, true);
                sci_.PlaySound(Dialogs[1], true);
                check2 = false;
                Timer = 4f;
                eventstat = 5;
                ActiveTimer = true;
                StopTimer = false;
            }
        }




        if (ActiveTimer)
            Timer -= Time.deltaTime;
        if (Timer <= 0.0f && StopTimer == false)
        {
            switch (eventstat)
            {
                case 1:
                    {
                        d1_.StopLookAt();
                        d2_.StopLookAt();
                        door1.GetComponent<Object_Door>().DoorSwitch();
                        sci_.PlaySound(Dialogs[0],true);
                        Timer = Timer2;
                        eventstat = 2;
                        break;
                    }

                case 2:
                    {
                        d1_.SetPath(path1);
                        GameController.instance.PlayHorror(Horror[0], ata1, npc.none);
                        eventstat = 3;
                        Timer = 0.6f;
                        break;
                    }

                case 3:
                    {
                        d2_.SetPath(path2);
                        eventstat = 4;
                        Timer = Timer3;
                        break;
                    }

                case 4:
                    {
                        check2 = true;
                        StopTimer = true;
                        d1_.SetLookAt(GameController.instance.npcObjects[(int)npc.scp173].transform);
                        d2_.SetLookAt(GameController.instance.npcObjects[(int)npc.scp173].transform);
                        break;
                    }

                case 5:
                    {
                        d1_.SetPath(path3);
                        eventstat = 6;
                        Timer = 3f;
                        break;
                    }
                case 6:
                    {
                        door1.GetComponent<Object_Door>().DoorSwitch();
                        sci_.PlaySound(Dialogs[2], true);
                        d2_.SetRota(door1.transform);
                        d2_.StopLookAt();
                        eventstat = 7;
                        Timer = Timer4;
                        break;

                    }
                case 7:
                    {
                        objPlayer.GetComponent<Player_Control>().FakeBlink(0.3f);
                        lightmana.SetActive(false);
                        eventstat = 8;
                        Timer = 0.5f;
                        d1_.AnimTrigger(-1, true);
                        d1_.PlaySFX(GeneralSFX[3]);
                        GameController.instance.npcObjects[(int)npc.scp173].transform.rotation = Quaternion.Euler(0, 90, 0);
                        GameController.instance.Warp173(false, ata1);
                        d1_.SetLookAt(deadlook);
                        
                        break;
                    }
                case 8:
                    {
                        d2_.SetLookAt(GameController.instance.npcObjects[(int)npc.scp173].transform);
                        lightmana.SetActive(true);
                        sci_.PlaySFX(GeneralSFX[0]);
                        eventstat = 9;
                        Timer = 1f;
                        GameController.instance.PlayHorror(Horror[1], null, npc.none);
                        break;
                    }
                case 9:
                    {
                        lightmana.SetActive(false);
                        objPlayer.GetComponent<Player_Control>().FakeBlink(0.3f);
                        eventstat = 10;
                        Timer = 0.5f;
                        GameController.instance.Warp173(false, ata2);
                        d2_.AnimTrigger(-2, true);
                        d2_.SetLookAt(deadlook);
                        d2_.PlaySFX(GeneralSFX[3]);
                        break;
                    }
                case 10:
                    {
                        GameController.instance.npcTable[(int)npc.scp173].Event_Spawn(true, ata2.transform.position);
                        sci_.PlaySFX(GeneralSFX[1]);
                        lightmana.SetActive(true);
                        Timer = Timer5;
                        eventstat = 11;
                        break;
                    }
                case 11:
                    {
                        lightmana.SetActive(false);
                        objPlayer.GetComponent<Player_Control>().FakeBlink(0.3f);
                        eventstat = 12;
                        Timer = 0.5f;
                        GameController.instance.npcObjects[(int)npc.scp173].transform.rotation = Quaternion.Euler(0, -90, 0);
                        GameController.instance.Warp173(false, ata3);
                        guard_.SetRota(GameController.instance.npcObjects[(int)npc.scp173].transform);
                        sci_.PlaySFX(GeneralSFX[5]);
                        break;
                    }

                case 12:
                    {
                        sci_.PlaySFX(GeneralSFX[2]);
                        emelight.SetActive(true);
                        guard_.AnimTrigger(1, true);
                        Timer = 2f;
                        eventstat = 13;
                        ActiveTimer2 = true;
                        RerenderProbe1.GetComponent<ReflectionProbe>().RenderProbe();
                        RerenderProbe2.GetComponent<ReflectionProbe>().RenderProbe();
                        break;
                    }
                case 13:
                    {
                        emelight.SetActive(false);
                        objPlayer.GetComponent<Player_Control>().FakeBlink(0.3f);
                        eventstat = 14;
                        Timer = 0.5f;
                        guard_.AnimTrigger(-2, true);
                        guard_.SetLookAt(deadlook);
                        guard_.PlaySFX(GeneralSFX[3]);
                        sci_.PlaySFX(GeneralSFX[5]);
                        sci_.PlaySFX(GeneralSFX[6]);
                        StopTimer2 = true;
                        flask.SetActive(false);
                        break;
                    }

                case 14:
                    {
                        objPlayer.GetComponent<Player_Control>().FakeBlink(0.6f);
                        sci_.SetSeq(Alarm);

                        guard_.StopRota();
                        objPlayer.GetComponent<Player_Control>().playerWarp((GameController.instance.WorldAnchor.transform.position + ((GameController.instance.WorldAnchor.transform.rotation * Quaternion.Inverse(TeleportAnchor.transform.rotation)) * (objPlayer.transform.position - TeleportAnchor.position))), GameController.instance.WorldAnchor.transform.eulerAngles.y - TeleportAnchor.transform.eulerAngles.y);
                        d1_.puppetWarp(GameController.instance.WorldAnchor.transform.position + ((GameController.instance.WorldAnchor.transform.rotation * Quaternion.Inverse(TeleportAnchor.transform.rotation)) * (d1.transform.position - TeleportAnchor.position)));
                        d2_.puppetWarp(GameController.instance.WorldAnchor.transform.position + ((GameController.instance.WorldAnchor.transform.rotation * Quaternion.Inverse(TeleportAnchor.transform.rotation)) * (d2.transform.position - TeleportAnchor.position)));
                        guard_.puppetWarp(GameController.instance.WorldAnchor.transform.position + ((GameController.instance.WorldAnchor.transform.rotation * Quaternion.Inverse(TeleportAnchor.transform.rotation)) * (guard.transform.position - TeleportAnchor.position)));
                        GameController.instance.canSave = true;
                        RenderSettings.fog = true;

                        StopTimer = true;

                        
                        GameController.instance.SetMapPos(0, 10);
                        GameController.instance.startEv.GetComponent<EV_Intro>().End();

                        if (GameController.instance.isAlive)
                        {
                            GameController.instance.doGameplay = true;
                            GameController.instance.Action_QuickSave();
                        }
                        break;
                    }

            }


        }






        if (ActiveTimer2)
            TimerSecondary -= Time.deltaTime;
        if (TimerSecondary <= 0.0f && StopTimer2 == false)
        {
            if (flask.activeSelf == true)
                flask.SetActive(false);
            else
                flask.SetActive(true);

            guard_.PlaySFX(GeneralSFX[4]);
            TimerSecondary = 0.1f;
        }




    }




}
