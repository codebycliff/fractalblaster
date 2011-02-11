using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace FractalBlaster.Core.IO {
    
    public class MediaFile {

        public FileInfo FileInfo { get; private set; }

        public IEnumerable<MediaProperty> Metadata { get; private set; }

        public MediaFile(String path) {
            FileInfo = new FileInfo(path);
            Metadata = ReadMetadata();
        }

        #region [ Private ]

        private IEnumerable<MediaProperty> ReadMetadata() {
            List<MediaProperty> readprops = new List<MediaProperty>();
            foreach (IMetadataPlugin plugin in FamilyKernel.PluginManager.GetPlugins(typeof(IMetadataPlugin))) {
                readprops.AddRange(plugin.Analyze(this));
            }
            return readprops;
        }
        
        #endregion
    }
}
