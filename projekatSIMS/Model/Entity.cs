using projekatSIMS.CompositeComon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Model
{
    public class Entity : ViewModelBase
    {
        protected int id; 

        public int Id
        {
            get { return id; } 
            set 
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }


        public virtual string ExportToString()
        {
            return string.Empty;
        }

        public virtual void ImportFromString(string[] parts)
        {
            Id = int.Parse(parts[0]);
        }
    }
}
