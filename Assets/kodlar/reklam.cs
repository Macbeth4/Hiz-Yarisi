using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System.Threading.Tasks;

public class reklam : MonoBehaviour
{
    private static reklam instance;
    private InterstitialAd Interstitial;

    public GameObject inspaneli;
    public GameObject menupanel;

    void Start()
    {
        MobileAds.Initialize(InitStatus => { });
        this.RequestInterstitial();
    }

    public async void reklamgosterAsync()
    {
        
        
    
        await Task.Delay(100);
        if(inspaneli != null){inspaneli.SetActive(true);}
        if(menupanel != null){menupanel.SetActive(false);}
        
        this.Interstitial.Show();

    }

    private void Awake() {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // Singleton nesneyi ayarla ve sahneler arasında yok etme
        instance = this;
        DontDestroyOnLoad(gameObject);

        inspaneli.SetActive(false);
    }


    void RequestInterstitial()
    {

        string reklamID = "ca-app-pub-1664062912484400/5282948700";

        this.Interstitial = new InterstitialAd(reklamID);
        AdRequest request = new AdRequest.Builder().Build();
        this.Interstitial.LoadAd(request);
    }
}

