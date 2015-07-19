using System;
using System.IO;
using System.Xml;

namespace AH64TD
{
    class Map
    {
        public string strMapName;

        const string strTerrainArtPath = "Content\\Art\\Terrain\\";
        const string strMapPath = "Content\\Maps\\";

        public Map(string mapName)
        {
            strMapName = mapName;
            LoadMap();
        }

        private bool LoadMap()
        {
            string strMapFilePath = strMapPath + strMapName + ".xml";

            using (StreamReader srMap = File.OpenText(strMapFilePath))
            {
                XmlDocument xmlMap = new XmlDocument();
                try
                {
                    xmlMap.Load(srMap);
                }
                catch (XmlException)
                {
                    // Need a way to display errors. DllImports don't seem to work.
                }
            }

            return true;
        }

    }
}