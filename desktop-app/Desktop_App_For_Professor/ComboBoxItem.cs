using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_App_For_Professor
{
    internal class ComboBoxItem
    {
        public int Value { get; set; } // The underlying value (e.g., class_id)
        public string Text { get; set; } // The display text (e.g., class_name)

        public override string ToString()
        {
            return Text; // This determines what is shown in the ComboBox
        }
    }
}
