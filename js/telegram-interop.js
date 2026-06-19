window.TelegramInterop = {
    isAvailable: function () {
        return !!(window.Telegram && window.Telegram.WebApp && window.Telegram.WebApp.initData);
    },
    ready: function () {
        if (this.isAvailable()) {
            window.Telegram.WebApp.ready();
            window.Telegram.WebApp.expand();

            if (window.Telegram.WebApp.disableVerticalSwipes) {
                window.Telegram.WebApp.disableVerticalSwipes();
            }
        }
    },

    getUser: function () {
        if (this.isAvailable() && window.Telegram.WebApp.initDataUnsafe?.user) {
            const user = window.Telegram.WebApp.initDataUnsafe.user;
            return JSON.stringify({
                id: user.id,
                firstName: user.first_name || "",
                lastName: user.last_name || "",
                username: user.username || "",
                languageCode: user.language_code || ""
            });
        }
        return null;
    },

    getColorScheme: function () {
        return window.Telegram?.WebApp?.colorScheme || "light";
    },

    showMainButton: function (text) {
        const btn = window.Telegram.WebApp.MainButton;
        btn.text = text;
        btn.show();
    },

    hideMainButton: function () {
        window.Telegram.WebApp.MainButton.hide();
    },

    onMainButtonClick: function (dotnetHelper) {
        window.Telegram.WebApp.MainButton.onClick(function () {
            dotnetHelper.invokeMethodAsync("OnMainButtonClicked");
        });
    },

    showBackButton: function () {
        window.Telegram.WebApp.BackButton.show();
    },

    hideBackButton: function () {
        window.Telegram.WebApp.BackButton.hide();
    },

    onBackButtonClick: function (dotnetHelper) {
        window.Telegram.WebApp.BackButton.onClick(function () {
            dotnetHelper.invokeMethodAsync("OnBackButtonClicked");
        });
    },

    showAlert: function (message) {
        window.Telegram.WebApp.showAlert(message);
    },

    showConfirm: function (message) {
        return new Promise(function (resolve) {
            window.Telegram.WebApp.showConfirm(message, function (confirmed) {
                resolve(confirmed);
            });
        });
    },

    hapticImpact: function (style) {
        window.Telegram.WebApp.HapticFeedback.impactOccurred(style);
    },

    hapticNotification: function (type) {
        window.Telegram.WebApp.HapticFeedback.notificationOccurred(type);
    },

    sendData: function (data) {
        window.Telegram.WebApp.sendData(data);
    },

    close: function () {
        window.Telegram.WebApp.close();
    }
};
