using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace hw_111
{


    public partial class MainWindow : Window
    {
        List<Client> clients = new List<Client>();
        Client selectedClient;

        bool isConsultant;

        public void RefreshClients()
        {

            List<ClientItemView> clientItems = clients.Select((c) => { return new ClientItemView(c.LastName, c.FirstName, c.Otchestvo, "********", c.Phone, c.DateChanged.ToString(), c.WhatChanged, c.WhoChanged); }).ToList();

            Clientslb.ItemsSource = clientItems;

            Clientslb.Items.Refresh();
        }

        public void RefreshSelectedClient()
        {
            if (selectedClient != null)
            {
                Client client = selectedClient;

                txtUserLastName.Text = client.LastName;
                txtUserName.Text = client.FirstName;
                txtUserOtchestvo.Text = client.Otchestvo;
                if (isConsultant)
                {
                    txtUserPassport.Text = "******";
                }
                else
                {
                    txtUserPassport.Text = client.Passport;
                }
                txtUserPhone.Text = client.Phone;
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            List<string> userTypes = new List<string>();
            userTypes.Add("Consultant");
            userTypes.Add("Manager");
            cbUserType.ItemsSource = userTypes;

            cbUserType.SelectionChanged += (s, e) =>
            {
                if ((string)cbUserType.SelectedItem == "Consultant")
                {
                    isConsultant = true;
                    txtUserLastName.IsEnabled = false;
                    txtUserName.IsEnabled = false;
                    txtUserOtchestvo.IsEnabled = false;
                    txtUserPassport.IsEnabled = false;
                    txtUserPhone.IsEnabled = true;
                    btnNewUser.IsEnabled = false;
                }
                else
                {
                    isConsultant = false;
                    txtUserLastName.IsEnabled = true;
                    txtUserName.IsEnabled = true;
                    txtUserOtchestvo.IsEnabled = true;
                    txtUserPassport.IsEnabled = true;
                    txtUserPhone.IsEnabled = true;
                    btnNewUser.IsEnabled = true;
                }
                RefreshSelectedClient();
            };

            cbUserType.SelectedIndex = 0;

            try { clients = JsonConvert.DeserializeObject<List<Client>>(System.IO.File.ReadAllText("Clients.json")); }
            catch (Exception e) { Console.WriteLine(e.Message); };


            RefreshClients();

            Clientslb.SelectionChanged += (s, e) =>
            {
                if (Clientslb.SelectedIndex >= 0)
                {
                    selectedClient = clients[Clientslb.SelectedIndex];
                }
                else
                {
                    selectedClient = null;
                }

                RefreshSelectedClient();

            };

            btnNewUser.Click += (s, e) =>
            {

                clients.Add(new Client("", "", "", "", "", DateTime.Now, "", ""));
                RefreshClients();
            };

            Phonebtn.Click += (s, e) =>
            {
                if (selectedClient == null)
                {
                    return;
                }

                string WhatChanged = "";

                Client client = selectedClient;


                if (txtUserName.Text != client.FirstName)
                {
                    client.FirstName = txtUserName.Text;
                    WhatChanged += "First Name, ";
                }
                if (txtUserLastName.Text != client.LastName)
                {
                    client.LastName = txtUserLastName.Text;
                    WhatChanged += "Last Name, ";
                }
                if (txtUserOtchestvo.Text != client.Otchestvo)
                {
                    client.Otchestvo = txtUserOtchestvo.Text;
                    WhatChanged += "Otchestvo, ";
                }

                if (txtUserPassport.IsEnabled && txtUserPassport.Text != client.Passport)
                {
                    client.Passport = txtUserPassport.Text;
                    WhatChanged += "Passport, ";
                }

                if (txtUserPhone.Text != client.Phone)
                {
                    client.Phone = txtUserPhone.Text;
                    WhatChanged += "Phone, ";
                }

                string WhoChanged = isConsultant ? "Consultant" : "Manager";

                client.WhatChanged = WhatChanged;
                client.WhoChanged = WhoChanged;
                client.DateChanged = DateTime.Now;




                RefreshClients();

            };

            Closing += (s, e) =>
            {
                string text = JsonConvert.SerializeObject(clients);
                System.IO.File.WriteAllText("Clients.json", text);
            };

        }
    }
}
