using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum BrickType {
    Small, // 2x2x2
    Medium, // 4x4x4
    Large, // 8x8x8
};

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // Variables to store between menu scene and simulation scene
    public int lineNumber;
    public Slider lineNumberSlider;
    public TMP_Text lineNumberText;

    public int soldierPerLine;
    public Slider soldierPerLineSlider;
    public TMP_Text soldierPerLineText;

    public int castleHeight;
    public Slider castleHeightSlider;
    public TMP_Text castleHeightText;

    public int castleWidth;
    public Slider castleWidthSlider;
    public TMP_Text castleWidthText;

    public int castleDepth;
    public Slider castleDepthSlider;
    public TMP_Text castleDepthText;
    
    public int cannonNumber;
    public Slider cannonNumberSlider;
    public TMP_Text cannonNumberText;

    public bool explosionEffectActive;
    public Toggle explosionEffectToggle;

    public bool bloodMagicEffectActive;
    public Toggle bloodMagicEffectToggle;

    public BrickType brickType = BrickType.Small;
    public TMP_Dropdown brickTypeDropdown;

    private string currentScene = "MenuScene";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        lineNumberSlider.onValueChanged.AddListener(UpdateLineNumberText);
        lineNumber = (int)lineNumberSlider.value;
        lineNumberText.text = lineNumber.ToString();

        soldierPerLineSlider.onValueChanged.AddListener(UpdateSoldierPerLineText);
        soldierPerLine = (int)soldierPerLineSlider.value;
        soldierPerLineText.text = soldierPerLine.ToString();

        castleHeightSlider.onValueChanged.AddListener(UpdateCastleHeightText);
        castleHeight = (int)castleHeightSlider.value;
        castleHeightText.text = castleHeight.ToString();

        castleWidthSlider.onValueChanged.AddListener(UpdateCastleWidthText);
        castleWidth = (int)castleWidthSlider.value;
        castleWidthText.text = castleWidth.ToString();

        castleDepthSlider.onValueChanged.AddListener(UpdateCastleDepthText);
        castleDepth = (int)castleDepthSlider.value;
        castleDepthText.text = castleDepth.ToString();

        cannonNumberSlider.onValueChanged.AddListener(UpdateCannonNumberText);
        cannonNumber = (int)cannonNumberSlider.value;
        cannonNumberText.text = cannonNumber.ToString();

        explosionEffectToggle.onValueChanged.AddListener(UpdateExplosionEffect);
        explosionEffectActive = explosionEffectToggle.isOn;

        bloodMagicEffectToggle.onValueChanged.AddListener(UpdateBloodMagicEffect);
        bloodMagicEffectActive = bloodMagicEffectToggle.isOn;

        brickTypeDropdown.onValueChanged.AddListener(UpdateBrickTypeDropdown);
        brickType = (BrickType)brickTypeDropdown.value;
    }

    private void UpdateLineNumberText(float value)
    {
        lineNumber = (int)value;
        lineNumberText.text = lineNumber.ToString();
        Debug.Log("New Line Number: " + lineNumber);
    }

    private void UpdateSoldierPerLineText(float value)
    {
        soldierPerLine = (int)value;
        soldierPerLineText.text = soldierPerLine.ToString();
        Debug.Log("New Soldier Per Line: " + soldierPerLine);
    }

    private void UpdateCastleHeightText(float value)
    {
        castleHeight = (int)value;
        castleHeightText.text = castleHeight.ToString();
        Debug.Log("New Castle Height: " + castleHeight);
    }

    private void UpdateCastleWidthText(float value)
    {
        castleWidth = (int)value;
        castleWidthText.text = castleWidth.ToString();
        Debug.Log("New Castle Width: " + castleWidth);
    }

    private void UpdateCastleDepthText(float value)
    {
        castleDepth = (int)value;
        castleDepthText.text = castleDepth.ToString();
        Debug.Log("New Castle Depth: " + castleDepth);
    }

    private void UpdateCannonNumberText(float value)
    {
        cannonNumber = (int)value;
        cannonNumberText.text = cannonNumber.ToString();
        Debug.Log("New Cannon Number: " + cannonNumber);
    }

    private void UpdateExplosionEffect(bool value)
    {
        explosionEffectActive = value;
        Debug.Log("Explosion Effect is Activated: " + explosionEffectActive);
    }

    private void UpdateBloodMagicEffect(bool value)
    {
        bloodMagicEffectActive = value;
        Debug.Log("BloodMagic Effect is Activated: " + bloodMagicEffectActive);
    }

    private void UpdateBrickTypeDropdown(int value)
    {
        brickType = (BrickType)value;
        Debug.Log("New Brick Type: " + brickType);
    }
 

    private void Update()
    {
        return;

        // TODO: Implement transitions of menu to simulation and vice versa
        // if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        // {
        //     if (currentScene == "MenuScene")
        //     {
        //         ChangeSceneToSimulation();
        //     } else
        //     {
        //         ChangeSceneToMenu();
        //     }
        // }
    }

    private void ChangeSceneToMenu()
    {
        currentScene = "MenuScene";
        SceneManager.LoadScene(currentScene);
    }

    public void ChangeSceneToSimulation()
    {
        currentScene = "SimulationScene";
        SceneManager.LoadScene(currentScene);
    }
}
