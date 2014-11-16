using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Utilities
{
    public class PropertyPair
    {
        public string Identifier
        {
            set;
            get;
        }

        public string Val
        {
            set;
            get;
        }

        public PropertyPair(string identifier, string val)
        {
            Identifier = identifier;
            Val = val;
        }
    }
}
