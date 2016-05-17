using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Net;
using Android.Views.Accessibility;
using Java.Net;
using PaddleBuddy.Core.DependencyServices;
using Plugin.Connectivity;

namespace PaddleBuddy.Droid.DependencyServices
{
    public class NetworkAndroid : BaseDependencyServiceAndroid, INetwork
    {
        public bool IsOnline
        {
            get { return CrossConnectivity.Current.IsConnected; }
        }

        public bool IsServerAvailable
        {
            get
            {
                return IsOnline;
                //TODO: implement serveravailable once using actual server
                //var a = IsRemoteReachable("www.google.com").Result;
                //return a;
            }
        }

        //private async Task<bool> IsRemoteReachable(string host, int port = 80, int msTimeout = 1000)
        //{
        //    if (string.IsNullOrEmpty(host))
        //        throw new ArgumentNullException("host");

        //    if (!CrossConnectivity.Current.IsConnected)
        //        return false;

        //    host = host.Replace("http://www.", string.Empty).
        //      Replace("http://", string.Empty).
        //      Replace("https://www.", string.Empty).
        //      Replace("https://", string.Empty);

        //    return await Task.Run(async () =>
        //    {
        //        try
        //        {
        //            InetSocketAddress sockAddr =
        //                await RunBlockingFuncOnOtherThread(() => new InetSocketAddress(host, port), msTimeout);
        //            if (sockAddr == null) return false;
        //            using (var sock = new Socket())
        //            {
        //                await sock.ConnectAsync(sockAddr, msTimeout);
        //                return true;
        //            }
        //        }
        //        catch (Exception)
        //        {
        //            return false;
        //        }
        //    });
        //}

        //public async Task<T> RunBlockingFuncOnOtherThread<T>(Func<T> function, int msTimeout)
        //{
        //    var tcs = new TaskCompletionSource<T>();
        //    new System.Threading.Thread(() =>
        //    {
        //        T result = function.Invoke();
        //        if (!tcs.Task.IsCompleted)
        //            tcs.TrySetResult(result);
        //    }).Start();

        //    await Task.Run(async () =>
        //     {
        //         await Task.Delay(msTimeout);
        //         if (!tcs.Task.IsCompleted)
        //             tcs.TrySetResult(default(T));
        //     });

        //    return await tcs.Task;
        //}
    }
}