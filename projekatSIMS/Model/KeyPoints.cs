using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Model
{
    public class KeyPoints : Entity
    {
        private string name;
        private bool isActive = false;
        private int belongs;
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

        public int Belongs
        {
            get { return belongs; }
            set  
            { 
            belongs = value;
            OnPropertyChanged(nameof(Belongs));
            }
        }

        public override string ExportToString()
        {
            return id + "|" + name + "|" + isActive + "|" + belongs;
        }

        public override void ImportFromString(string[] parts)
        {
            base.ImportFromString(parts);
            Name = parts[1];
            isActive = bool.Parse(parts[2]); 
            Belongs = int.Parse(parts[3]);
        }
    }
}
