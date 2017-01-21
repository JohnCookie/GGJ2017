using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JPoint{
	public int x { get; set; }
	public int y { get; set; }
	public JPoint(int x, int y){
		this.x = x;
		this.y = y;
	}

	public string ToString(){
		return "<"+x.ToString()+","+y.ToString()+">"; 
	}
}

public class TextureDrawing : MonoBehaviour {
	Texture2D lastCacheTexture;

	Texture2D tempTexture;
	Color touming = new Color (0, 0, 0, 0);
	Color[] redColorBrush = { Color.red, Color.red, Color.red, Color.red, Color.red, Color.red, Color.red, Color.red, Color.red };
	public MeshRenderer planeRenderer;

	public int singleRandomPointNum = 20;
	public int graintLineNum = 10;
	public int seperateSingleLineNum = 20;

	JPoint startPoint = new JPoint(200,200);

	public Player player;
	public Game mGame;
	float currTime = 0;

	Color currColor=Color.red;
	float currHSL = 0f;
	// Use this for initialization
	void Start () {
		tempTexture = planeRenderer.sharedMaterial.mainTexture as Texture2D;
		initTexture ();
	}
	
	// Update is called once per frame
	void Update () {
		if (mGame.isRun) {
			currTime += Time.deltaTime;
			if (currTime >= 0.5f) {
				JPoint[] p = player.getPositions ();
				currHSL += 0.5f;
				if (currHSL >= 360f) {
					currHSL = 0f;
				}
				currColor = Color.HSVToRGB (currHSL / 360f, 1f, 1f);
				generateOneLine (p [0], p [1], p [2], p [3]);
			}
		}
	}

	void test(){
		generateSeveralLines ();
	}

	void generateSeveralLines(){
//		for (int i = 0; i < 20; i++) {
//			generateOneLine ();
//			startPoint.x += 5;
//			startPoint.y += 5;
//		}
	}

	void generateOneLine(JPoint p0, JPoint p1, JPoint p2, JPoint p3){
//		p0 = new JPoint (336, 392);
//		p1 = new JPoint (355, 457);
//		p2 = new JPoint (482, 502);
//		p3 = new JPoint (501, 567);
//		Debug.Log("p0 "+p0.x+","+p0.y +" p1 "+p1.x+","+p1.y+" p2 "+p2.x+","+p2.y+" p3 "+p3.x+","+p3.y);

		drawBezier (p0, p1, p2, p3);
	}

	public void initTexture(){
		for (int x = 0; x < tempTexture.width; x++) {
			for (int y = 0; y < tempTexture.height; y++) {
				tempTexture.SetPixel (x, y, touming);
			}
		}
		tempTexture.Apply ();
	}

	void OnGUI(){
		if(GUI.Button(new Rect(10,10,100,20),"Draw")){
			test ();
		}
		if(GUI.Button(new Rect(10,40,100,20),"Clear")){
			initTexture ();
		}
	}

	void drawBezier(JPoint p0, JPoint p1, JPoint p2, JPoint p3){
		List<JPoint> pList = getBezierPointsList (p0, p1, p2, p3);
		for (int i = 0; i < pList.Count; i++) {
			//tempTexture.SetPixels (pList [i].x-1, pList [i].y-1, 3, 3, redColorBrush);
			tempTexture.SetPixel (pList [i].x, pList [i].y, currColor);
		}
		tempTexture.Apply ();
	}

	// p0 start p1 control1 p2 control2 p3 end
	List<JPoint> getBezierPointsList(JPoint p0, JPoint p1, JPoint p2, JPoint p3){
		List<JPoint> points = new List<JPoint> ();
		int length = p3.x - p0.x;
		float rate = 1f / length;
		for (int i = 0; i < length; i++) {
			JPoint bezierPoint = pointOnBezier (p0, p1, p2, p3, rate * i);
			points.Add (bezierPoint);
		}
		return points;
	}

	JPoint pointOnBezier(JPoint p0, JPoint p1, JPoint p2, JPoint p3, float t){
		float ax, bx, cx, dx;
		float ay, by, cy, dy;
		ax = (1 - t) * (1 - t) * (1 - t) * (float)p0.x;
		bx = 3 * t * (1 - t) * (1 - t) * (float)p1.x;
		cx = 3 * t * t * (1 - t) * (float)p2.x;
		dx = t * t * t * (float)p3.x;

		ay= (1 - t) * (1 - t) * (1 - t) * (float)p0.y;
		by = 3 * t * (1 - t) * (1 - t) * (float)p1.y;
		cy = 3 * t * t * (1 - t) * (float)p2.y;
		dy = t * t * t * (float)p3.y;
		JPoint result = new JPoint ((int)(ax + bx + cx +dx), (int)(ay + by + cy + dy));
		return result;
	}

	public void saveDieTexture(){
		lastCacheTexture = tempTexture;
	}

	public bool generateTarget(int x, int y){
		if (lastCacheTexture == null) {
			return false;
		}
		Color[] colors = lastCacheTexture.GetPixels (x - 2, y - 2, 5, 5);
		float totalR=0f, totalG=0f, totalB=0f, totalA=0f;
		for (int i = 0; i < colors.Length; i++) {
			totalR += colors [i].r;
			totalG += colors [i].g;
			totalB += colors [i].b;
			totalA += colors [i].a;
		}
		float avgR=0f, avgG=0f, avgB=0f, avgA=0f;
		avgR = totalR / (float)colors.Length;
		avgG = totalG / (float)colors.Length;
		avgB = totalB / (float)colors.Length;
		avgA = totalA / (float)colors.Length;

		Debug.Log (avgA);
		if (avgA < 0.05f) {
			return false;
		} else {
			return true;
		}
	}
}
