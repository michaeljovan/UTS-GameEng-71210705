using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ballController : MonoBehaviour
{
public int force;
int scoreKuning;
int scoreMerah;
Text scoreUIKuningA;
Text scoreUIMerahA;
Rigidbody2D rigid;
GameObject panelSelesai;
Text txPemenang;

    void Start()
    {
    rigid = GetComponent<Rigidbody2D>();
    Vector2 arah = new Vector2(0,2).normalized;
    rigid.AddForce(arah*force);
    scoreMerah=0;
    scoreKuning=0;
    scoreUIMerahA = GameObject.Find("scoreMerahA").GetComponent<Text>();
    scoreUIKuningA = GameObject.Find("scoreKuningA").GetComponent<Text>();
    panelSelesai = GameObject.Find("panelSelesai");
    panelSelesai.SetActive(false);
    }


    void Update()
    {
        
    }

    void tampilScore(){
        Debug.Log("Score Merah: "+ scoreKuning + "Score Biru: "+ scoreMerah);
        scoreUIMerahA.text = scoreMerah + "";
        scoreUIKuningA.text = scoreKuning + "";
    }

    private void OnCollisionEnter2D(Collision2D coll){
        if(coll.gameObject.name == "gawang1"){
            scoreMerah += 1;
            tampilScore();
            if(scoreMerah == 7){
                panelSelesai.SetActive(true);
                txPemenang = GameObject.Find("winInfo").GetComponent<Text>();
                txPemenang.text = "Player Merah Menang!";
                Destroy(gameObject);
                return;
            }
            resetBall();
            Vector2 arah = new Vector2(0,2).normalized;
            rigid.AddForce(arah*force);
        }
        if(coll.gameObject.name == "gawang2"){
            scoreKuning += 1;
            tampilScore();
            if(scoreKuning == 7){
                panelSelesai.SetActive(true);
                txPemenang = GameObject.Find("winInfo").GetComponent<Text>();
                txPemenang.text = "Player Kuning Menang!";
                Destroy(gameObject);
                return;
            }
            resetBall();
            Vector2 arah = new Vector2(0,-2).normalized;
            rigid.AddForce(arah*force);
        }
        if(coll.gameObject.name == "Penjaga1" || coll.gameObject.name == "Penjaga2"){
            float sudut = (transform.position.x - coll.transform.position.x)*5f;
            Vector2 arah = new Vector2(sudut, rigid.velocity.y).normalized;
            rigid.velocity = new Vector2(0,0);
            rigid.AddForce(arah * force * 2);
        }
    }

    void resetBall(){
        transform.localPosition = new Vector2(0,0);
        rigid.velocity = new Vector2(0,0);
    }
}
