using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Model
{
    internal class KeyPoints : Entity
    {
        private string name;
        private bool isActive = false;
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

        public override string ExportToString()
        {
            return id + "|" + name + "|" + isActive;
        }

        public override void ImportFromString(string[] parts)
        {
            base.ImportFromString(parts);
            Name = parts[1];
            //isActive = parts[2]; string to bool error
        }
    }
}
