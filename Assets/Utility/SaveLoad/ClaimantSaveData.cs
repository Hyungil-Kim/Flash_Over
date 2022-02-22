using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClaimantSaveData
{
	public int targetPlayerIndex;
	public int id;
	public int eventid;

	public float posx;
	public float posy;
	public float posz;
	public bool stun;
	public bool confuse;
	public bool eventOn;
	public bool exit;
	public int claimantArea;
	public int hp;
	public int airGauge;//산소통 이름변경 필요
	public int speed;
	public int weight;
	public int num;
	public bool moveEnd;

	public int oxygentank ;//산소탱크
	public int ap; // 현재폐활량
	public int Maxap ; // 최대폐활량
	public int lung; // 폐 hp

	public int index;
}
