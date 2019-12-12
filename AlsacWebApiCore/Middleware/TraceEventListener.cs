using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using Serilog;

namespace AlsacWebApiCore.Middleware
{
    public class TraceEventListener : EventListener
    {
        private static IDisposable _listenerSubscription;
        private static IDisposable _sqlListenerSubscription;

        public TraceEventListener()
        {
            try
            {
                _listenerSubscription = DiagnosticListener.AllListeners.Subscribe(listener =>
                {
                    try
                    {
                        if (listener.Name == "SqlClientDiagnosticListener")
                            _sqlListenerSubscription = listener.Subscribe(e => SqlOnEvent(listener, e));
                    }
                    catch (Exception e)
                    {
                        Log.Error(e, "Error configuring individual DiagnosticListeners");
                    }
                });
            }
            catch (Exception e)
            {
                Log.Error(e, "Error configuring top level DiagnosticListener");
            }
        }

        public static void SqlOnEvent(DiagnosticListener listener, KeyValuePair<string, object> evnt)
        {
            if (evnt.Key.Equals("System.Data.SqlClient.WriteCommandAfter"))
            {
                if (!(evnt.Value.GetProperty("Command") is SqlCommand command)) return;
                Log.Logger.Debug(command.CommandText);
            }
            else if (evnt.Key.Equals("System.Data.SqlClient.WriteConnectionCloseAfter"))
            {
                if (!(evnt.Value.GetProperty("Connection") is SqlConnection c)) return;
                Log.Logger.Debug(c.ConnectionString);
            }
        }

#pragma warning disable CA1816 // Dispose methods should call SuppressFinalize
        public override void Dispose()
#pragma warning restore CA1816 // Dispose methods should call SuppressFinalize
        {
            _listenerSubscription?.Dispose();
            _sqlListenerSubscription?.Dispose();
            base.Dispose();
        }
    }
}
