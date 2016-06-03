using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.VoiceCommands;
using Windows.Storage;

namespace AllJoynApp3
{
    class VoiceCommands
    {
        static bool onStateRequested = false;
        // register command
        public async static void RegisterVoiceCommands()
        {
            StorageFile storageFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///VoiceCommands.xml"));
            await VoiceCommandDefinitionManager.InstallCommandDefinitionsFromStorageFileAsync(storageFile);
        }

        // process command
        public static void ProcessVoiceCommands(VoiceCommandActivatedEventArgs eventArgs)
        {
            switch (eventArgs.Result.RulePath[0])
            {
                case "ToggleLamp":
                    string switchableStateChange = eventArgs.Result.SemanticInterpretation.Properties["switchableStateChange"][0];
                    if (string.Equals(switchableStateChange, "on", StringComparison.OrdinalIgnoreCase))
                    {
                        onStateRequested = true;
                    }
                    else
                    {
                        onStateRequested = false;
                    }
                    LampHelper lampHelper = new LampHelper();
                    lampHelper.LampFound += LampHelper_LampFound;

                    break;
                default:
                    break;
            }
        }

        private static void LampHelper_LampFound(object sender, EventArgs e)
        {
            LampHelper lampHelper = sender as LampHelper;
            lampHelper.SetOnOffAsync(onStateRequested);
        }
    }
}
