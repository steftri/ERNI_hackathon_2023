using ERNI.BerlinSpartans.Hackathon.Services.MqttClient;
using ERNI.BerlinSpartans.Hackathon.Services.MqttClient.Models;
using ERNI.BerlinSpartans.Hackathon.Services.RemoteControl;
using ERNI.BerlinSpartans.Hackathon.Services.RemoteControl.Models;
using Microsoft.Extensions.Logging;
using MQTTnet.Server;

namespace ERNI.BerlinSpartans.Hackathon.WFTestApp
{
    public partial class Form1 : Form
    {
        public IRemoteControlService RemoteControlService { get; set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            var options = new MqttClientConnectionOptions
            {
                BrokerAddress = "10.213.90.68: 1883",
                SpinTimeout  = 10000
            };

            var loggerFactory = LoggerFactory.Create(builder => { });

            
            this.RemoteControlService = new RemoteControlService(new MqttClientService(options, loggerFactory.CreateLogger<MqttClientService>()));
            //10.230.90.68: 1883
            var t = this.RemoteControlService.SendAsync(new RemoteCommand
            {
                CommandType = RemoteCommandType.Set
            });
            Task.WaitAll(t);

            var result = t.Result.ErrorMessage;

            this.txtLog.Text += result + Environment.NewLine;
            // /set
        }
    }
}