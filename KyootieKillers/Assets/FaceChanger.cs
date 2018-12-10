using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceChanger : MonoBehaviour {
    
    [System.Serializable]
    public class Face{
        public SkinnedMeshRenderer renderer; // set manually
        public Material whaleSkin;
        public Material idleFace;
        public Material angryFace;
        public Material surprisedFace;
        public Material cryingFace;
        public Material ecksdeeFace;
        public Material kissFace;
        public Material hungryFace;
        public Material deadFace;
        public Material[] idle;
        public Material[] angry;
        public Material[] surprised;
        public Material[] crying;
        public Material[] ecksdee;
        public Material[] kiss;
        public Material[] hungry;
        public Material[] dead;
        public string emotion;
    }
    
    public Face face = new Face();
    
	void Start () {
		face.idle = new Material[]{face.idleFace, face.whaleSkin};
        face.angry = new Material[]{face.angryFace, face.whaleSkin};
        face.surprised = new Material[]{face.surprisedFace, face.whaleSkin};
        face.crying = new Material[]{face.cryingFace, face.whaleSkin};
        face.ecksdee = new Material[]{face.ecksdeeFace, face.whaleSkin};
        face.kiss = new Material[]{face.kissFace, face.whaleSkin};
        face.hungry = new Material[]{face.hungryFace, face.whaleSkin};
        face.dead = new Material[]{face.deadFace, face.whaleSkin};
        face.emotion = "idle";
	}
	
	void Update () {
		UpdateFace();
	}
    
    private void UpdateFace(){
        if (face.emotion.Equals("angry")){
            face.renderer.materials = face.angry;
        } else if (face.emotion.Equals("surprised")){
            face.renderer.materials = face.surprised;
        } else if (face.emotion.Equals("crying")){
            face.renderer.materials = face.crying;
        } else if (face.emotion.Equals("ecksdee")){
            face.renderer.materials = face.ecksdee;
        } else if (face.emotion.Equals("kiss")){
            face.renderer.materials = face.kiss;
        } else if (face.emotion.Equals("hungry")){
            face.renderer.materials = face.hungry;
        } else if (face.emotion.Equals("dead")){
            face.renderer.materials = face.dead;
        } else {
            face.renderer.materials = face.idle;
        }
    }
    
    public void ChangeEmotion(string newFace){
        face.emotion = newFace;
    }
}
