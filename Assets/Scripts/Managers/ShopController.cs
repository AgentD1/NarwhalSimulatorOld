using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour {
    public List<ShopItemScriptableObject> shopItems;
    public List<string> shopCatagories;

    List<ShopItemScriptableObject> currentShopItems;

    public GameObject shopTemplate;
    public GameObject shopGridLayout;

    public GameObject shopCatagorySelect;
    public GameObject shopCatagoryButtonTemplate;

    public GameObject searchButton;
    public GameObject searchText;

    public GameObject PauseUI;
    public GameObject ShopUI;

    public Dropdown sortDropDown;

    public void Start() {

        sortDropDown.onValueChanged.AddListener(new UnityEngine.Events.UnityAction<int>(OnSortDropdownChanged));

        currentShopItems = shopItems;

        searchButton.GetComponent<Button>().onClick.AddListener(delegate { OnSearchButtonSelect(); });

        searchText.GetComponent<InputField>().onEndEdit.AddListener(delegate { OnSearchButtonSelect(); });

        OnSortDropdownChanged(0);
        
        LoadShop();

        
        // EquipItemToPlayer(shopItems[2], GameObject.FindGameObjectWithTag("Player"));
    }

    public void Update() {
        if(Input.GetKeyDown(KeyCode.Escape) && ShopUI.activeSelf) {
            PauseUI.SetActive(true);
            ShopUI.SetActive(false);
        }
    }

    public void LoadShop() {
        shopCatagories = new List<string>();

        

        for (int i = 0; i < shopGridLayout.transform.childCount; i++) {
            Destroy(shopGridLayout.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < shopCatagorySelect.transform.childCount; i++) {
            Destroy(shopCatagorySelect.transform.GetChild(i).gameObject);
        }

        shopCatagories.Add("All");

        foreach (ShopItemScriptableObject item in currentShopItems) {
            GameObject go = Instantiate(shopTemplate, shopGridLayout.transform);
            GameObject titleText = go.transform.Find("TitleText").gameObject;
            GameObject typeText = go.transform.Find("TypeText").gameObject;
            GameObject briefDescription = go.transform.Find("DescriptionText").gameObject;
            GameObject image = go.transform.Find("ImageContainer").GetChild(0).gameObject;

            GameObject purchaseButton = go.transform.Find("BuyButton").gameObject;
            GameObject viewButton = go.transform.Find("ViewButton").gameObject;

            GameObject purchaseButtonText = purchaseButton.transform.GetChild(0).gameObject;
            
            titleText.GetComponent<Text>().text = item.itemName;
            typeText.GetComponent<Text>().text = item.itemType;
            briefDescription.GetComponent<Text>().text = item.briefDescription;
            image.GetComponent<Image>().sprite = item.shopThumbnail;
            image.GetComponent<Image>().color = item.color;

            if (FindObjectOfType<InventoryManager>().ownedItems.Contains(item.itemName)) {
                purchaseButton.GetComponent<Button>().onClick.AddListener(delegate { OnEquipButtonClicked(item.itemName); });
                purchaseButtonText.GetComponent<Text>().text = "Equip Item";
            } else {
                purchaseButton.GetComponent<Button>().onClick.AddListener(delegate { OnPurchaseButtonClicked(item.itemName); });
                purchaseButtonText.GetComponent<Text>().text = "Purchase($" + item.cost + ")";
            }
        }

        foreach (ShopItemScriptableObject item in shopItems) {
            if (!shopCatagories.Contains(item.itemType)) {
                shopCatagories.Add(item.itemType);
            }
        }

        foreach (string catagory in shopCatagories) {
            GameObject go = Instantiate(shopCatagoryButtonTemplate, shopCatagorySelect.transform);
            GameObject text = go.transform.GetChild(0).gameObject;
            text.GetComponent<Text>().text = catagory;
            go.GetComponent<Button>().onClick.AddListener(delegate { OnCatagorySortSelect(catagory); });
        }
    }

    public void GiveItem(string item) {
        InventoryManager im = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
        if (im.ownedItems.Contains(item)) {
            Debug.LogWarning("Player already owns " + item + "! Returning...");
            return;
        }

        im.ownedItems.Add(item);
    }

    public void OnReturnToMenuButtonPressed() {
        PauseUI.SetActive(true);
        ShopUI.SetActive(false);
    }

    public void OnSearchButtonSelect() {
        // currentShopItems = shopItems.Where(stringToCheck => stringToCheck.itemName.Contains(searchText.GetComponent<InputField>().text)) as List<ShopItemScriptableObject>;

        string searchTerm = searchText.GetComponent<InputField>().text.ToLower();

        currentShopItems.Clear();

        foreach (ShopItemScriptableObject item in shopItems) {
            if (item.itemName.ToLower().Contains(searchTerm)) {
                currentShopItems.Add(item);
            }
        }

        OnSortDropdownChanged(sortDropDown.value);

        LoadShop();
    }

    public void OnCatagorySortSelect(string catagory) {
        if(catagory == "All") {
            currentShopItems = shopItems;
        } else {
            currentShopItems = new List<ShopItemScriptableObject>();
            
            for (int i = 0; i < shopItems.Count; i++) {
                if(shopItems[i].itemType == catagory) {
                    currentShopItems.Add(shopItems[i]);
                }
            }
        }

        OnSortDropdownChanged(sortDropDown.value);
    }

    public void OnSortDropdownChanged(int id) {
        if(id == 0) {
            // A-Z
            currentShopItems = currentShopItems.OrderBy(c => c.itemName).ToList();
        } else if (id == 1) {
            // Z-A
            currentShopItems = currentShopItems.OrderBy(c => c.itemName).ToList();
            currentShopItems.Reverse();
        } else if (id == 2) {
            // Cost Asc.
            currentShopItems = currentShopItems.OrderBy(c => c.cost).ToList();
        } else if (id == 3) {
            // Cost Dec.
            currentShopItems = currentShopItems.OrderBy(c => c.cost).ToList();
            currentShopItems.Reverse();
        }

        LoadShop();
    }

    public void PurchaseItem(string itemName) {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        ShopItemScriptableObject item = shopItems.Find(delegate (ShopItemScriptableObject i) { return i.itemName == itemName; });

        if (player.score < item.cost) {
            Debug.Log("Player hasn't got enough money to purchase " + item.itemName);
            return;
        }

        player.score -= item.cost;
        
        GiveItem(itemName);
    }

    public void EquipItemToPlayer(string item) {
        ShopItemScriptableObject shopItem = shopItems.Find(delegate (ShopItemScriptableObject i) { return i.itemName == item; });

        EquipItemToPlayer(shopItem);
    }

    public void EquipItemToPlayer(ShopItemScriptableObject item) {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        string itemType = item.itemType;
        GameObject part = Instantiate(item.prefab);
        part.transform.parent = player.transform;
        if (player.bodyParts.ContainsKey(itemType)) {
            Destroy(player.bodyParts[itemType], 0.001f);
            player.bodyParts.Remove(itemType);
        } else {
            Debug.Log("No itemType on player" + itemType);
        }

        player.bodyParts.Add(itemType, part);
        

        part.transform.localPosition = Vector3.zero;
        part.transform.localRotation = Quaternion.identity;
    }

    public void OnEquipButtonClicked(string itemName) {
        EquipItemToPlayer(itemName);
    }

    public void OnPurchaseButtonClicked(string itemName) {
        /* Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        ShopItemScriptableObject item = shopItems.Find(delegate (ShopItemScriptableObject i) { return i.itemName == itemName; });


        InventoryManager im = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
        if (im.ownedItems.Contains(itemName)) {
            Debug.LogWarning("Player already owns " + itemName + "! Returning...");
            return;
        }

        if(player.score < item.cost) {
            Debug.Log("Player hasn't got enough money to purchase " + item.itemName);
            return;
        }

        player.score -= item.cost;

        im.ownedItems.Add(itemName);
        */

        PurchaseItem(itemName);

        LoadShop();
    }
}
