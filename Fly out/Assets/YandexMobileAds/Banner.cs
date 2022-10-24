/*
 * This file is a part of the Yandex Advertising Network
 *
 * Version for Unity (C) 2018 YANDEX
 *
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at https://legal.yandex.com/partner_ch/
 */

using System;
using YandexMobileAds.Base;
using YandexMobileAds.Common;
using YandexMobileAds.Platforms;

namespace YandexMobileAds
{
    /// A class for displaying banner ad view.
    public class Banner
    {
        private AdRequestCreator adRequestFactory;
        private IBannerClient client;

        /// Notifies that the banner is loaded. At this time, you can add banner if you haven’t done so yet.
        public event EventHandler<EventArgs> OnAdLoaded;

        /// Notifies that the banner failed to load.
        public event EventHandler<AdFailureEventArgs> OnAdFailedToLoad;
        
        /// Called when user returned to application after click.
        public event EventHandler<EventArgs> OnReturnedToApplication;

        /// <summary>
        /// Notifies that the app will become inactive now because the user clicked on the banner
        /// ad and is about to switch to a different application (Phone, App Store, and so on).
        /// </summary>
        public event EventHandler<EventArgs> OnLeftApplication;

        /// Notifies that the user has clicked on the banner.
        public event EventHandler<EventArgs> OnAdClicked;

        /// Notifies delegate when an impression was tracked.
        public event EventHandler<ImpressionData> OnImpression;

        /// <summary>
        /// Initializes an object of the Banner class to display the banner with the specified size.
        /// <param name="blockId"> Unique ad placement ID created at partner interface. Example: R-M-DEMO-320x50.</param>
        /// <param name="adSize"> The size of banner ad. <see cref="YandexMobileAds.Base.AdSize"/></param>
        /// <param name="position"> Banner position on screen <see cref="YandexMobileAds.Base.AdPosition"/></param>
        /// </summary>
        public Banner(string blockId, AdSize adSize, AdPosition position)
        {
            this.adRequestFactory = new AdRequestCreator();
            this.client = YandexMobileAdsClientFactory.BuildBannerClient(blockId, adSize, position);

            ConfigureBannerEvents();
        }

        /// <summary>
        /// Loads Banner with data for targeting.
        /// </summary>
        /// <param name="request">Data for targeting.</param>
        public void LoadAd(AdRequest request)
        {
            client.LoadAd(adRequestFactory.CreateAdRequest(request));
        }

        /// <summary>
        /// Hides Banner from screen.
        /// </summary>
        public void Hide()
        {
            client.Hide();
        }

        /// <summary>
        /// Shows Banner on screen.
        /// </summary>
        public void Show()
        {
            client.Show();
        }

        /// <summary>
        /// Destroys Banner.
        /// </summary>
        public void Destroy()
        {
            client.Destroy();
        }

        private void ConfigureBannerEvents()
        {
            this.client.OnAdLoaded += (sender, args) =>
            {
                if (this.OnAdLoaded != null)
                {
                    this.OnAdLoaded(this, args);
                }
            };

            this.client.OnAdFailedToLoad += (sender, args) =>
            {
                if (this.OnAdFailedToLoad != null)
                {
                    this.OnAdFailedToLoad(this, args);
                }
            };

            this.client.OnReturnedToApplication += (sender, args) =>
            {
                if (this.OnReturnedToApplication != null)
                {
                    this.OnReturnedToApplication(this, args);
                }
            };

            this.client.OnLeftApplication += (sender, args) =>
            {
                if (this.OnLeftApplication != null)
                {
                    this.OnLeftApplication(this, args);
                }
            };

            this.client.OnAdClicked += (sender, args) =>
            {
                if (this.OnAdClicked != null)
                {
                    this.OnAdClicked(this, args);
                }
            };

            this.client.OnImpression += (sender, args) =>
            {
                if (this.OnImpression != null)
                {
                    this.OnImpression(this, args);
                }
            };
        }
    }
}