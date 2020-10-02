using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace SOEventSystem
{
    public class EventCreator : MonoBehaviour, ISerializationCallbackReceiver
    {
        public string path;
        public string varType;
        public bool createEvent;



        public void OnAfterDeserialize()
        {
            if (createEvent)
            {
                createEvent = false;
                CreateFiles();
            }
        }

        public void OnBeforeSerialize()
        {

        }





        private void CreateFiles()
        {
            string newPath = $"{path}/{CapitalizeFirstLetter(varType)} Event";

            CreateVariableDirectory(newPath);
            string capitalizedVarType = CapitalizeFirstLetter(varType);

            string soFileName = $"{newPath}/{CapitalizeFirstLetter(varType)}EventSO.cs";
            string soFormat = $"using UnityEngine; \n\nnamespace SOEventSystem \n{{ \n\t[CreateAssetMenu(menuName = \"Events/{capitalizedVarType} Event\")] \n\tpublic class {capitalizedVarType}EventSO : VarEventSO<{varType}> \n\t{{ \n\t}} \n}}";

            string listenerFileName = $"{newPath}/{CapitalizeFirstLetter(varType)}EventListener.cs";
            string listenerFormat = $"namespace SOEventSystem \n{{ \n\tpublic class {capitalizedVarType}EventListener : VarEventListener<{varType}> \n\t{{ \n\t}} \n}}";

            string callerFileName = $"{newPath}/{CapitalizeFirstLetter(varType)}EventCaller.cs";
            string callerFormat = $"namespace SOEventSystem \n{{ \n\tpublic class {capitalizedVarType}EventCaller : VarEventCaller<{varType}> \n\t{{ \n\t}} \n}}";

            CreateFile(soFileName, soFormat);
            CreateFile(listenerFileName, listenerFormat);
            CreateFile(callerFileName, callerFormat);
        }

        private void CreateFile(string filename, string contents)
        {
            FileStream file;
            if (!File.Exists(filename))
            {
                file = File.Create(filename);
            }
            else
            {
                file = File.OpenWrite(filename);
            }
            byte[] contentBytes = Encoding.ASCII.GetBytes(contents);
            file.Write(contentBytes, 0, contents.Length);
        }

        private void CreateVariableDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        private string CapitalizeFirstLetter(string word)
        {
            return word.ToUpper().Substring(0, 1) + word.Substring(1);
        }

    }
}
