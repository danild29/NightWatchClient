using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NightWatchClientApp.Helpers;

public static class TaskExtension
{
    public static async void Await(this Task task, Action<Exception> errorCallBack = null)
    {
		try
		{
			await task;

		}
		catch (Exception ex)
		{
			if (errorCallBack != null) errorCallBack?.Invoke(ex);
			else
	            throw;
		}
    }

}
