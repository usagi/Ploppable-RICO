using ICities;
using UnityEngine;


//using GrowableOverhaul;

namespace PloppableRICO
{
    public class Loading : LoadingExtensionBase
    {

        public GameObject RICODataManager;
        public RICOPrefabManager xmlManager;

        private ConvertPrefabs convertPrefabs;


        public override void OnLevelLoaded(LoadMode mode)
        {
            base.OnLevelLoaded(mode);

            //if (mode != LoadMode.LoadGame && mode != LoadMode.NewGame)
            //return;

            //Load xml only from main menu. 
            if (xmlManager == null)
            {
                xmlManager = new RICOPrefabManager();
                xmlManager.Run();
            }

            //Assign xml settings to prefabs.
            convertPrefabs = new ConvertPrefabs();
            convertPrefabs.run();

            //Init GUI
            PloppableTool.Initialize();
            RICOSettingsPanel.Initialize();

            //Deploy Detour
            Detour.BuildingToolDetour.Deploy();
            Debug.Log("Detour Deployed");
        }

        public override void OnLevelUnloading()
        {
            Detour.BuildingToolDetour.Revert();
            Util.AssignServiceClass();
            base.OnLevelUnloading();
        }
    }
}