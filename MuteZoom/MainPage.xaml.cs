using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Media;
using Windows.Media.Playback;
using Windows.System;
using Windows.UI.Input.Preview.Injection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MuteZoom
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            MediaPlayer mediaPlayer = new MediaPlayer();
            var systemMediaTransportControls = mediaPlayer.SystemMediaTransportControls;

            systemMediaTransportControls.IsEnabled = true;
            systemMediaTransportControls.IsNextEnabled = true;
            systemMediaTransportControls.ButtonPressed += SystemControls_ButtonPressed;
        }

        void SystemControls_ButtonPressed(SystemMediaTransportControls sender, SystemMediaTransportControlsButtonPressedEventArgs args)
        {
            switch (args.Button)
            {
                case SystemMediaTransportControlsButton.Next:
                    InputInjector inputInjector = InputInjector.TryCreate();
                    var alt = new InjectedInputKeyboardInfo();
                    alt.VirtualKey = (ushort)(VirtualKey.LeftMenu);
                    alt.KeyOptions = InjectedInputKeyOptions.None;

                    var a = new InjectedInputKeyboardInfo();
                    a.VirtualKey = (ushort)(VirtualKey.A);
                    a.KeyOptions = InjectedInputKeyOptions.None;

                    inputInjector.InjectKeyboardInput(new[] { alt, a });
                    
                    var altUp = new InjectedInputKeyboardInfo();
                    altUp.VirtualKey = (ushort)(VirtualKey.LeftMenu);
                    altUp.KeyOptions = InjectedInputKeyOptions.KeyUp;
                    inputInjector.InjectKeyboardInput(new[] { altUp });

                    Debug.WriteLine("Next");
                    break;
                default:
                    break;
            }
        }
    }
}
