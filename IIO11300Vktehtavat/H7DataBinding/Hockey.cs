using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H7DataBinding
{
    public class HockeyPlayer : INotifyPropertyChanged
    {
        private string name;
        public string Name
        {
            set
            {
                name = value;
                Notify("Name");
                Notify("NameAndNumber");
            }
            get
            {
                return name;
            }
        }
        private string number;
        public string Number
        {
            get { return number; }
            set
            {
                number = value;
                Notify("Number");
                Notify("NameAndNumber");
            }

        }
        public string NameAndNumber
        {
            get { return name + " #" + number; }
        }

        public HockeyPlayer(string name, string number)
        {
            this.name = name;
            this.number = number;
        }

        public override string ToString()
        {
            return name + " #" + number;
        }

        #region PROPERTY EVENT HANDLER

        public event PropertyChangedEventHandler PropertyChanged;

        void Notify(string propName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        #endregion
    }
    public class HockeyTeam
    {
        #region PROPERTIES
        //huom public field ei kelpaa WPF:n databindingissä,
        //pitää olla property
        public string Name { get; set; }
        public string City { get; set; }
        #endregion
        #region CONSTERUCTORS
        public HockeyTeam()
        {
            Name = "";
            City = "unknown";
        }
        public HockeyTeam(string name, string city)
        {
            Name = name;
            City = city;
        }
        #endregion
        public override string ToString()
        {
            return Name + "@" + City;
        }

    }

    public class HockeyLeague
    {

        //perustetaan SM-liig, sisältää x kappaletta joukkueita
        //HUOM: jos halutaan että databindaus huomaa automaattisesti
        //muutokset kokoelmassa käytä ObservableCollection -kokoelmaa
        List<HockeyTeam> teams = new List<HockeyTeam>(); 
        public HockeyLeague()
        {
            teams.Add(new HockeyTeam("HIFK", "Helsinki"));
            teams.Add(new HockeyTeam("JYP", "Jyväskylä"));
            teams.Add(new HockeyTeam("Kalpa", "Kuopio"));
            teams.Add(new HockeyTeam("Sport", "Vaasa"));
        }
        //metodi joka palauttaa liigan joukkueet

        public List<HockeyTeam> GetTeams()
        {
            return teams;
        }
    }
}
