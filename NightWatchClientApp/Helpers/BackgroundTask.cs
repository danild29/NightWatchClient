using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NightWatchClientApp.Helpers;


public delegate void UpdateCallback(CancellationToken ct);
public sealed class BackgroundTask
{
    private Task? _task;
    private readonly PeriodicTimer _timer;
    private CancellationTokenSource _cts;
    private UpdateCallback Callback;

    public BackgroundTask(TimeSpan timeSpan)
    {
        _timer = new(timeSpan);
        
    }

    public void Start(UpdateCallback callback)
    {
        _cts = new CancellationTokenSource();
        Callback = callback;
        _task = DoWorkAsync();
    }

    private async Task DoWorkAsync()
    {
        try
        {
            while(await _timer.WaitForNextTickAsync(_cts.Token))
            {
                Callback(CancellationToken.None);
            }
        }
        catch (OperationCanceledException)
        {
        }
        catch {
            throw;
        }
    }

    public async Task StopTask()
    {
        if (_task is null) return;

        _cts.Cancel();
        //await _task;
        _cts.Dispose();

    }
}
