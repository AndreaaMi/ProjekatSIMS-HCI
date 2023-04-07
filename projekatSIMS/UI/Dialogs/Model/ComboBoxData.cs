using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.UI.Dialogs.Model
{
    public class ComboBoxData<T>
    {
      public string Name { get; set; } //Ono sto se vidi na View
      public T Value { get; set; } //Je objekat
   
    }
}
