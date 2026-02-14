using System.Collections.Generic;
using UnityEngine;

public enum SkinID
{
    MaskGuy = 0,
    NinjaGuy = 1,
    PinkGuy = 2,
    VirtualGuy = 3
}

public class SkinData
{
    public SkinID skinID;
    public bool isPurchased = false;
    public string title;
    public float price;
    public bool isSelected;

    public SkinData(SkinID skinID, bool isPurchased, float price, string title, bool isSelected = false)
    {
        this.skinID = skinID;
        this.isPurchased = isPurchased;
        this.price = price;
        this.title = title;
        this.isSelected = isSelected;
    }
}

[CreateAssetMenu(fileName = "PlayerSkin", menuName = "CreatePlayerSkin", order = 0)]
public class PlayerSkin : ScriptableObject
{
    bool isInitialized = false;
    bool isPurchaseMode = false;
    List<SkinData> _skins = new List<SkinData>(){
            new SkinData(SkinID.MaskGuy, true, 0, "Mask Guy", true),
            new SkinData(SkinID.NinjaGuy, true, 50, "Ninja Guy"),
            new SkinData(SkinID.PinkGuy, false, 100, "Pink Guy"),
            new SkinData(SkinID.VirtualGuy, false, 200, "Virtual Guy")
        };

    public event System.Action<SkinID, SkinID> OnSkinChanged;

    public List<SkinData> skins
    {
        get
        {
            return _skins;
        }
        private set
        {
            _skins = value;
        }
    }

    private void OnEnable()
    {
        isInitialized = true;
    }

    public SkinData selectedSkin
    {
        get
        {
            if (isPurchaseMode)
            {
                return skins.Find(skin => skin.isSelected);
            }

            return skins.Find(skin => skin.isSelected && skin.isPurchased);
        }

        set
        {
            if (value != null && value.isPurchased)
            {
                SkinData previousSelectedSkin = selectedSkin;
                skins.ForEach(skin => skin.isSelected = false);
                value.isSelected = true;
                OnSkinChanged?.Invoke(previousSelectedSkin.skinID, value.skinID);
            }
        }
    }

    public SkinData GetSkin(SkinID skinID)
    {
        return skins.Find(skin => skin.skinID == skinID);
    }

    public List<SkinData> GetSkins()
    {
        return skins;
    }

    public void PurchaseSkin(SkinID skinID, System.Action onComplete)
    {
        SkinData skinToPurchase = skins.Find(skin => skin.skinID == skinID);
        if (skinToPurchase != null && !skinToPurchase.isPurchased)
        {
            skinToPurchase.isPurchased = true;
            onComplete();
        }
    }
}