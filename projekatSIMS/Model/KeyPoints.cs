using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Model
{
    public class KeyPoints : Entity
    {
        private string name;
        private bool isActive = false;
        private int associatedTour;
        public KeyPoints() { }

        public KeyPoints(string name, bool isActive)
        {
            this.name = name;
            this.isActive = isActive;
        }

        public string Name
        {  get { return name; } 
           set 
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            
            }
        
        }

        public bool IsActive
        {
            get { return isActive; }
            set
            {
                isActive = value;
                OnPropertyChanged(nameof(IsActive));

            }

        }

        public int AssociatedTour
        {
            get { return associatedTour; }
            set  
            { 
            associatedTour = value;
            OnPropertyChanged(nameof(AssociatedTour));
            }
        }

        public override string ExportToString()
        {
            return id + "|" + name + "|" + isActive + "|" + associatedTour;
        }

        public override void ImportFromString(string[] parts)
        {
            base.ImportFromString(parts);
            Name = parts[1];
            isActive = bool.Parse(parts[2]);
            associatedTour = int.Parse(parts[3]);
        }
    }
}
