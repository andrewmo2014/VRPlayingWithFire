using UnityEngine;
using System.Collections;

public class ChangeParticleColor : MonoBehaviour {

    private ParticleSystem[] particleSystems;

    private Color[] colorsRainbow;

    private int currentColorIndex = 0;
    private int nextColorIndex;

    private float changeColourTime = 2.0f;

    private float lastColorChange = 0.0f;
    private float timer = 0.0f;


    void Awake()
    {
        colorsRainbow = new Color[7] {  new Color(255, 0, 0),       //RED
                                        new Color(255, 127, 0),     //ORANGE
                                        new Color(255, 255, 0),     //YELLOW
                                        new Color(0, 255, 0),       //GREEN
                                        new Color(0, 0, 255),       //BLUE
                                        new Color(75, 0, 130),      //INDIGO
                                        new Color(148, 0, 211)      //VIOLET
        };
    }


    // Use this for initialization
    void Start () {
        particleSystems = GetComponentsInChildren<ParticleSystem>();

        if (colorsRainbow == null || colorsRainbow.Length < 2)
            Debug.Log("Need to setup colors array in inspector");

        nextColorIndex = (currentColorIndex + 1) % colorsRainbow.Length;

}
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;

        if (timer > changeColourTime)
        {
            currentColorIndex = (currentColorIndex + 1) % colorsRainbow.Length;
            nextColorIndex = (currentColorIndex + 1) % colorsRainbow.Length;
            timer = 0.0f;

        }

        UpdateParticleSystems();

    }

    /// <summary>
    /// Updates Particle System Components
    /// </summary>
    private void UpdateParticleSystems()
    {
        foreach (ParticleSystem ps in particleSystems)
        {
            float originalAlpha = ps.startColor.a;
            Color currentColor = colorsRainbow[currentColorIndex];
            Color nextColor = colorsRainbow[nextColorIndex];

            Color psColor = Color.Lerp( new Color( currentColor.r, currentColor.g, currentColor.b, originalAlpha ),
                                        new Color( nextColor.r, nextColor.g, nextColor.b, originalAlpha),
                                        timer / changeColourTime
                                      );
            ps.startColor = new Color(psColor.r, psColor.g, psColor.b, originalAlpha);
        }
    }

}
