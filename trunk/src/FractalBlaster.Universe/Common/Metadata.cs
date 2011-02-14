using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FractalBlaster.Universe {
    public class Metadata : IEnumerable<MediaProperty> {

        public Metadata() {
            Properties = new List<MediaProperty>();
        }

        public MediaProperty this[String key] {
            get { return Properties.Where(p => p.Name.CompareTo(key) == 0).First(); }
            set { Properties.Add(value);  }
        }

        public IEnumerable<String> Keys { get { return Properties.Select(p => p.Name); } }

        public IEnumerable<String> Values { get { return Properties.Select(p => p.Value.ToString()); } }

        public Boolean ContainsKey(String key) {
            return Properties.Any(p => p.Name.CompareTo(key) == 0);
        }

        #region [ IEnumerable ]

        public IEnumerator<MediaProperty> GetEnumerator() {
            return Properties.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return Properties.GetEnumerator();
        }
        
        #endregion
        
        #region [ Private ]

        private List<MediaProperty> Properties { get; set; }
        
        #endregion
    
    }
}
