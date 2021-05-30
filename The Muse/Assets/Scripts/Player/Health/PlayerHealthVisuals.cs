using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthVisuals : MonoBehaviour
{
    [SerializeField]
    private Sprite heart0Sprite;
    [SerializeField]
    private Sprite heart1Sprite;
    [SerializeField]
    private Sprite heart2Sprite;

    private List<HeartImage> heartImageList;
    public PlayerHealthSystem playerHealthSystem;

    [SerializeField]
    private LevelLoader transition;

    private void Awake()
    {
        heartImageList = new List<HeartImage>();
    }

    private void Start()
    {
        PlayerHealthSystem playerHealthSystem = new PlayerHealthSystem(5);

        SetHeartsHealthSystem(playerHealthSystem);
    }

    public void SetHeartsHealthSystem(PlayerHealthSystem playerHealthSystem)
    {
        this.playerHealthSystem = playerHealthSystem;

        List<PlayerHealthSystem.Heart> heartList = playerHealthSystem.GetHeartList();
        Vector2 heartAnchoredPosition = new Vector2(-860, 445);

        for (int i = 0; i < heartList.Count; i++)
        {
            PlayerHealthSystem.Heart heart = heartList[i];
            CreateHeartImage(heartAnchoredPosition).SetHeartFragments(heart.GetFragmentAmount());
            heartAnchoredPosition += new Vector2(105, 0);
        }

        playerHealthSystem.OnDamaged += PlayerHealthSystem_OnDamaged;
        playerHealthSystem.OnHealed += PlayerHealthSystem_OnHealed;
        playerHealthSystem.OnDead += PlayerHealthSystem_OnDead;
    }

    private void PlayerHealthSystem_OnDead(object sender, System.EventArgs e)
    {
        transition.GameOver();
    }

    private void PlayerHealthSystem_OnHealed(object sender, System.EventArgs e)
    {
        RefreshAllHearts();
    }

    private void PlayerHealthSystem_OnDamaged(object sender, System.EventArgs e)
    {
        RefreshAllHearts();
    }

    private void RefreshAllHearts()
    {
        List<PlayerHealthSystem.Heart> heartList = playerHealthSystem.GetHeartList();
        for (int i = 0; i < heartImageList.Count; i++)
        {
            HeartImage heartImage = heartImageList[i];
            PlayerHealthSystem.Heart heart = playerHealthSystem.GetHeartList()[i];
            heartImage.SetHeartFragments(heart.GetFragmentAmount());
        }
    }

    private HeartImage CreateHeartImage(Vector2 anchoredPosition)
    {
        GameObject heartGameObject = new GameObject("Heart", typeof(Image));
        //Set as child of transform
        heartGameObject.transform.parent = transform;
        heartGameObject.transform.localPosition = Vector3.zero;

        heartGameObject.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
        heartGameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(95, 95);
        //Set Heart Sprite
        Image heartImageUI = heartGameObject.GetComponent<Image>();
        heartImageUI.sprite = heart0Sprite;

        HeartImage heartImage = new HeartImage(this, heartImageUI);
        heartImageList.Add(heartImage);

        return heartImage;
    }

    //Represents a single heart
    public class HeartImage
    {
        private Image heartImage;
        private PlayerHealthVisuals playerHealthSystem;

        private 
        protected Sprite[] sprites { get; private set; }
        protected int fragments { get; private set; }

        public HeartImage(PlayerHealthVisuals playerHealthSystem, Image heartImage)
        {
            this.playerHealthSystem = playerHealthSystem;
            this.heartImage = heartImage;
        }

        public void SetHeartFragments(int fragments)
        {
            switch(fragments){
                case 0: heartImage.sprite = playerHealthSystem.heart0Sprite; break;

                case 1: heartImage.sprite = playerHealthSystem.heart1Sprite; break;

                case 2: heartImage.sprite = playerHealthSystem.heart2Sprite; break;
            }
        }

    }
}
