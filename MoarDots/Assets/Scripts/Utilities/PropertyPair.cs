using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Utilities
{
    class PropertyPair
    {
        public string Identifier
        {
            set;
            get;
        }

        public float Val
        {
            set;
            get;
        }

        public PropertyPair(string identifier, float val)
        {
            Identifier = identifier;
            Val = val;
        }
    }
}
