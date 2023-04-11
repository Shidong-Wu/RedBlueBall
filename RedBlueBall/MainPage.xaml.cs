using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace RedBlueBall
{
    public partial class MainPage : ContentPage
    {
        //List<Lottery> lotteries = new List<Lottery>(); 
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Lottery lottery = RandomBalls.GenerateLottery();
            var horizonLayout = new StackLayout { Orientation = StackOrientation.Horizontal, Padding = 10, HorizontalOptions = LayoutOptions.FillAndExpand };
            List<string> redBallStringList = new List<string>();
            if (lottery.RedBalls.Any())
            {
                foreach (var red in lottery.RedBalls)
                {
                    redBallStringList.Add(red.ToString("00"));
                }
            }
            var redBalllabel = new Label { Text = string.Join("  ", redBallStringList), TextColor = Color.Red, FontSize = 20, HorizontalOptions = LayoutOptions.FillAndExpand, Padding = new Thickness(0, 5, 0, 5) };
            var blueBalllabel = new Label { Text = lottery.BlueBall.ToString("00"), TextColor = Color.Blue, FontSize = 20, HorizontalOptions = LayoutOptions.FillAndExpand, Padding = new Thickness(0, 5, 0, 5) };
            var sendButton = new Button { Text = "Buy", TextColor = Color.OrangeRed, BackgroundColor = Color.OldLace, HorizontalOptions = LayoutOptions.FillAndExpand };
            string smsCommand = string.Format("00CP#01#{0}*{1}#1", string.Join("", redBallStringList), lottery.BlueBall.ToString("00"));
            string recipient = "106937629999";
            sendButton.Clicked += async delegate
            {
                await SendSms(smsCommand, recipient);
            };

            horizonLayout.Children.Add(redBalllabel); 
            horizonLayout.Children.Add(blueBalllabel);
            horizonLayout.Children.Add(sendButton);
            resultLayout.Children.Add(horizonLayout);
        }

        private void Clear_Clicked(object sender, EventArgs e)
        {
            resultLayout.Children.Clear();
        }

        public async Task SendSms(string messageText, string recipient)
        {
            try
            {
                var message = new SmsMessage(messageText, new[] { recipient });
                await Sms.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException ex)
            {
                // Sms is not supported on this device.
            }
            catch (Exception ex)
            {
                // Other error has occurred.
            }
        }
    }
}
