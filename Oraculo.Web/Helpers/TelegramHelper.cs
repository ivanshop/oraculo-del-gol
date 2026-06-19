using Oraculo.Web.Models;
using Microsoft.JSInterop;
using System.Text.Json;

namespace Oraculo.Web.Helpers;

public class TelegramHelper(IJSRuntime js) : IAsyncDisposable
{

    private DotNetObjectReference<TelegramHelper>? _dotNetRef;

    public event Func<Task>? OnMainButtonClicked;
    public event Func<Task>? OnBackButtonClicked;

    public async Task<bool> IsAvailableAsync()
        => await js.InvokeAsync<bool>("TelegramInterop.isAvailable");

    public async Task ReadyAsync()
        => await js.InvokeVoidAsync("TelegramInterop.ready");

    public async Task<TelegramUserModel?> GetUserAsync()
    {
        var json = await js.InvokeAsync<string?>("TelegramInterop.getUser");
        return json is null ? null : JsonSerializer.Deserialize<TelegramUserModel>(json);
    }

    public async Task<string> GetColorSchemeAsync()
        => await js.InvokeAsync<string>("TelegramInterop.getColorScheme");


    public async Task ShowMainButtonAsync(string text)
    {
        await js.InvokeVoidAsync("TelegramInterop.showMainButton", text);
        _dotNetRef ??= DotNetObjectReference.Create(this);
        await js.InvokeVoidAsync("TelegramInterop.onMainButtonClick", _dotNetRef);
    }

    public async Task HideMainButtonAsync()
        => await js.InvokeVoidAsync("TelegramInterop.hideMainButton");

    public async Task ShowBackButtonAsync()
    {
        await js.InvokeVoidAsync("TelegramInterop.showBackButton");
        _dotNetRef ??= DotNetObjectReference.Create(this);
        await js.InvokeVoidAsync("TelegramInterop.onBackButtonClick", _dotNetRef);
    }

    public async Task HideBackButtonAsync()
        => await js.InvokeVoidAsync("TelegramInterop.hideBackButton");

    public async Task ShowAlertAsync(string message)
        => await js.InvokeVoidAsync("TelegramInterop.showAlert", message);

    public async Task<bool> ShowConfirmAsync(string message)
        => await js.InvokeAsync<bool>("TelegramInterop.showConfirm", message);

    public async Task HapticImpactAsync(string style = "medium")
        => await js.InvokeVoidAsync("TelegramInterop.hapticImpact", style);

    public async Task HapticNotificationAsync(string type = "success")
        => await js.InvokeVoidAsync("TelegramInterop.hapticNotification", type);

    public async Task SendDataAsync(object data)
        => await js.InvokeVoidAsync("TelegramInterop.sendData", JsonSerializer.Serialize(data));

    public async Task CloseAsync()
        => await js.InvokeVoidAsync("TelegramInterop.close");


    [JSInvokable]
    public async Task OnMainButtonClicked_Invoke()
    {
        if (OnMainButtonClicked is not null)
            await OnMainButtonClicked.Invoke();
    }

    [JSInvokable("OnMainButtonClicked")]
    public Task OnMainButtonClickedJs() => OnMainButtonClicked_Invoke();

    [JSInvokable("OnBackButtonClicked")]
    public async Task OnBackButtonClickedJs()
    {
        if (OnBackButtonClicked is not null)
            await OnBackButtonClicked.Invoke();
    }

    public ValueTask DisposeAsync()
    {
        _dotNetRef?.Dispose();
        return ValueTask.CompletedTask;
    }

}
