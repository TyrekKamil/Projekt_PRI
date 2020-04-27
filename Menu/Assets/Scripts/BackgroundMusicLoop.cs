using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicLoop : MonoBehaviour
{
 static BackgroundMusicLoop instance = null;

 void Awake(){
   if(instance != null){
     Destroy(gameObject);
   }else{
     instance = this;
     DontDestroyOnLoad(gameObject);
   }
 }
 

}
