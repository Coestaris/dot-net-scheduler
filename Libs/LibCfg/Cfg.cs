using System.Xml;

namespace LibCfg
{
    public static class SCHEnvironment
    {
        public static string ProjectLocation => "d:\\WAMPServer\\www\\private\\scheduler\\";
        public static string LibDir => ProjectLocation;
        public static string CfgDir => ProjectLocation + "..\\cfg\\";
        public static string LogDir => ProjectLocation + "..\\logs\\tasks\\";
        public static string ImgDir => ProjectLocation + "..\\logs\\images\\";
    }
    
    public class Cfg
    {
        public string GetValue(string name)
        {
            foreach (var a in document.ChildNodes[1])
            {
                if (((XmlNode)a).Attributes[0].Value.ToLower() == name.ToLower())
                    return ((XmlNode)a).LastChild.Value;
            }
            return null;
        }

        private XmlDocument document;

        public Cfg(string fileName)
        {
            document = new XmlDocument();
            document.Load(fileName);
        }
    }
}
