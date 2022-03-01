using System;

class ClientItemView
{
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string Otchestvo { get; set; }
    public string Passport { get; set; }
    public string Phone { get; set; }
    public string DateChanged { get; set; }
    public string WhatChanged { get; set; }
    public string WhoChanged { get; set; }

    public ClientItemView(string LastName, string FirstName, string Otchestvo, string Passport, string Phone, string DateChanged, string WhatChanged, string WhoChanged)
    {
        this.LastName = LastName;
        this.FirstName = FirstName;
        this.Otchestvo = Otchestvo;
        this.Passport = Passport;
        this.Phone = Phone;
        this.DateChanged = DateChanged;
        this.WhatChanged = WhatChanged;
        this.WhoChanged = WhoChanged;
    }
}
