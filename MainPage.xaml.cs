using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AllJoynApp3
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private LampHelper lampHelper = null;
        bool lampFound = false;

        public MainPage()
        {
            this.InitializeComponent();

            slider.Maximum = uint.MaxValue;
            slider1.Maximum = uint.MaxValue;
            slider2.Maximum = uint.MaxValue;

            lampHelper = new LampHelper();
            lampHelper.LampFound += LampHelper_LampFound;
            lampHelper.LampStateChanged += LampHelper_LampStateChanged;
        }

       private void LampHelper_LampFound(object sender, EventArgs e)
        {
            lampFound = true;
            GetLampState();
        }

        private void LampHelper_LampStateChanged(object sender, EventArgs e)
        {
            GetLampState();
        }


        private void toggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            if(lampFound)
            {
                lampHelper.SetOnOffAsync((sender as ToggleSwitch).IsOn);
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (lampFound)
            {
                lampHelper.SetHueAsync((uint)slider.Value);
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (lampFound)
            {
                lampHelper.SetSaturationAsync((uint)slider1.Value);

            }
            
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {

            if (lampFound)
            {
                lampHelper.SetBrightnessAsync((uint)slider2.Value);

            }
            
        }

        private async void GetLampState()
        {
            if (lampFound)
            {
                toggleSwitch.IsOn = await lampHelper.GetOnOffAsync();
                slider.Value = await lampHelper.GetHueAsync();
                slider1.Value = await lampHelper.GetSaturationAsync();
                slider2.Value = await lampHelper.GetBrightnessAsync();
            }
        }

        
    }
}
