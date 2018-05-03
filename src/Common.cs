using System;
using System.ComponentModel;
using System.IO;
using System.Management;
using System.Reflection;
using System.Windows.Forms;
using CnSharp.VisualStudio.Extensions;
using CnSharp.VisualStudio.SourceControl.Tfs;
using EnvDTE;

namespace CnSharp.VisualStudio.NuPack
{
    public class Common
    {
        public const string ProductName = "NuPack";

        public static readonly string[] SupportedProjectTypes = { ".csproj", ".vbproj",".fsproj" };

        public static void CheckTfs(Project project)
        {
            var file = project.FileName + ".vspscc";
            Host.Instance.SourceControl = File.Exists(file) ? new TfsSourceControl() : null;
        }

        public const string SymbolServer = "https://nuget.smbsrc.net/";

        public static string GetOrganization()
        {
            var c = new ManagementClass("Win32_OperatingSystem");
            foreach (var o in c.GetInstances())
            {
                //Console.WriteLine("Registered User: {0}, Organization: {1}", o["RegisteredUser"], o["Organization"]);
                if (!string.IsNullOrWhiteSpace(o["Organization"]?.ToString()))
                    return o["Organization"].ToString();
            }
            return null;
            //return (string)Microsoft.Win32.Registry.GetValue(@"HKEY_LOCAL_MACHINE\Software\Microsoft\Windows NT\CurrentVersion", "RegisteredOrganization", "");
        }
    }

    class Validation
    {
        public static bool HasValidationErrors(System.Windows.Forms.Control.ControlCollection controls)
        {
            bool hasError = false;

            // Now we need to loop through the controls and deterime if any of them have errors
            foreach (Control control in controls)
            {
                // check the control and see what it returns
                bool validControl = IsValid(control);
                // If it's not valid then set the flag and keep going.  We want to get through all
                // the validators so they will display on the screen if errorProviders were used.
                if (!validControl)
                    hasError = true;

                // If its a container control then it may have children that need to be checked
                if (control.HasChildren)
                {
                    if (HasValidationErrors(control.Controls))
                        hasError = true;
                }
            }
            return hasError;
        }

        // Here, let's determine if the control has a validating method attached to it
        // and if it does, let's execute it and return the result
        private static bool IsValid(object eventSource)
        {
            string name = "EventValidating";

            Type targetType = eventSource.GetType();

            do
            {
                FieldInfo[] fields = targetType.GetFields(
                     BindingFlags.Static |
                     BindingFlags.Instance |
                     BindingFlags.NonPublic);

                foreach (FieldInfo field in fields)
                {
                    if (field.Name == name)
                    {
                        EventHandlerList eventHandlers = ((EventHandlerList)(eventSource.GetType().GetProperty("Events",
                            (BindingFlags.FlattenHierarchy |
                            (BindingFlags.NonPublic | BindingFlags.Instance))).GetValue(eventSource, null)));

                        Delegate d = eventHandlers[field.GetValue(eventSource)];

                        if (d != null)
                        {
                            Delegate[] subscribers = d.GetInvocationList();

                            // ok we found the validation event,  let's get the event method and call it
                            foreach (Delegate d1 in subscribers)
                            {
                                // create the parameters
                                object sender = eventSource;
                                CancelEventArgs eventArgs = new CancelEventArgs();
                                eventArgs.Cancel = false;
                                object[] parameters = new object[2];
                                parameters[0] = sender;
                                parameters[1] = eventArgs;
                                // call the method
                                d1.DynamicInvoke(parameters);
                                // if the validation failed we need to return that failure
                                return !eventArgs.Cancel;
                            }
                        }
                    }
                }

                targetType = targetType.BaseType;

            } while (targetType != null);

            return true;
        }

    }
}