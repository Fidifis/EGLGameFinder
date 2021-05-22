using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EGLGamesFinder
{
    class ManifestGenerator
    {
        public int Addeditems { get; private set; } = 0;
        public string ErrorMessage { get; private set; } = "Unknown error";

        public bool GenerateManifests(string path)
        {
            if (path[path.Length - 1] != '\\')
                path += "\\";
            if (Directory.Exists(path))
            {
                try
                {
                    string[] folders = Directory.GetDirectories(path);

                    foreach (string folder in folders)
                    {
                        string egstore = folder + "\\.egstore";
                        if (Directory.Exists(egstore))
                        {
                            string[] files = Directory.GetFiles(egstore);
                            string mancpn;
                            try
                            {
                                mancpn = files.Single(s => s.EndsWith(".mancpn"));
                            }
                            catch (Exception) { continue; }
                            string mancpnContent = File.ReadAllText(mancpn);

                            string guid = Path.GetFileNameWithoutExtension(mancpn);
                            string generatedContent = GenerateContent(mancpnContent, folder, guid);

                            string manifests = @"C:\ProgramData\Epic\EpicGamesLauncher\Data\Manifests";
                            if (Directory.Exists(manifests))
                            {
                                string item = manifests + "\\" + guid + ".item";
                                if (!File.Exists(item))
                                {
                                    File.WriteAllText(item, generatedContent);
                                    Addeditems++;
                                }
                            }
                            else
                            {
                                ErrorMessage = "Epic's manifests folder does not exist\n" +
                                    "You must run the Epic Games Launcher at least once for the folder to be created\n" +
                                    "If you have already done so this is a bug";
                                return false;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ErrorMessage = ex.Message;
                    return false;
                }
            }
            else
            {
                ErrorMessage = "path does not exist\n" + path;
                return false;
            }

            if (Addeditems == 0)
            {
                ErrorMessage = "No game found. You have selected the wrong folder or all games have already been added.";
                return false;
            }

            return true;
        }

        private string GenerateContent(string mancpn, string gameFolder, string guid)
        {
            Dictionary<string, string> known = GetMancpnContent(mancpn);

            string gameFolderSlash = gameFolder.Replace(@"\", @"\\");

            string content = "{\r\n" +
                "\t\"FormatVersion\": " + known["FormatVersion"] + ",\r\n" +
                "\t\"ManifestLocation\": \"" + gameFolderSlash + "/.egstore\",\r\n" +
                "\t\"AppName\": \"" + known["AppName"] + "\",\r\n" +
                "\t\"CatalogItemId\": \"" + known["CatalogItemId"] + "\",\r\n" +
                "\t\"CatalogNamespace\": \"" + known["CatalogNamespace"] + "\",\r\n" +
                "\t\"InstallationGuid\": \"" + guid + "\",\r\n" +
                "\t\"InstallLocation\": \"" + gameFolderSlash + "\",\r\n" +
                "\t\"StagingLocation\": \"" + gameFolderSlash + "/.egstore/bps\",\r\n" +
                "\t\"MandatoryAppFolderName\": \"" + Path.GetFileName(gameFolder) + "\"\r\n" +
                "}";

            return content;
        }

        private Dictionary<string, string> GetMancpnContent(string content)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string[] lines = content.Split('\n');
            foreach(string line in lines)
            {
                var match = Regex.Match(line, "\"(.+?)\": (?:\"|)(.+?)(?:\"|,)");
                if (match.Success)
                    dic.Add(match.Groups[1].Value, match.Groups[2].Value);
            }

            return dic;
        }
    }
}
