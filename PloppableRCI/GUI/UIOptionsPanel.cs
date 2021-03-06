﻿using ColossalFramework;
using ColossalFramework.UI;
using UnityEngine;

namespace PloppableRICO
{
    /// <summary>
    ///The far right column of the settigns panel. Contains the drop downs and entry fields that allows players to assign RICO settings. 
    /// </summary>

    public class UIBuildingOptions : UIScrollablePanel

    {
        
        string[] Service = new string[]{
        "None",
        "Residential",
        "Industrial",
        "Office",
        "Commercial",
        "Extractor",
        "Dummy"
        };

        string[] OfficeSub = new string[]{
        "Generic",
        //"IT Cluster"
        };

        string[] ResSub = new string[]{
        "High",
        "Low",
        //"High Eco",
        //"Low Eco"
        };

        string[] ComSub = new string[]{
        "High",
        "Low",
        "Tourist",
        "Leisure",
        //"Eco"
        };

        string[] IndustrialSub = new string[]{
        "Generic",
        "Forest",
        "Oil",
        "Ore",
        "Farming"
        };

        string[] ExtractorSub = new string[]{
        "Forest",
        "Oil",
        "Ore",
        "Farming"
        };

        string[] Level = new string[]{
        "1",
        "2",
        "3",
        };

        string[] resLevel = new string[]{
        "1",
        "2",
        "3",
        "4",
        "5"
        };

        string[] extLevel = new string[]{
        "1",
        };

        string[] UICategory = new string[]{
           "Res Low",
           "Res High",
           "Com Low",
           "Com High",
           "Office",
           "Industrial",
            "Farming",
            "Oil",
            "Forest",
            "Ore",
            "Tourist",
            "Leisure",
            "None",
        };

        public bool disableEvents;
        public RICOBuilding currentSelection;
        //Enable RICO
        public UICheckBox ricoEnabled;
        public UIPanel enableRICOPanel;

        public UIDropDown service;
        public UIDropDown subService;
        public UIDropDown level;
        public UIDropDown uiCategory;



        public UITextField manual;



        //Pollution
        public UICheckBox pollutionEnabled;
        public UIPanel pollutionPanel;

        public UICheckBox popBalanceEnabled;
        public UICheckBox manualWorkersEnabled;
        public UIPanel manualPanel;

        //Construction
        public UICheckBox constructionCostEnabled;
        public UIPanel constructionPanel;
        public UITextField construction;

        public UILabel label;
        public UIPanel labelpanel;

        private static UIBuildingOptions _instance;
        public static UIBuildingOptions instance

        {
            get { return _instance; }
        }

        public override void Start()
        {
            base.Start();

            _instance = this;
            isVisible = true;
            canFocus = true;
            isInteractive = true;
            backgroundSprite = "UnlockingPanel";
            //padding = new RectOffset(5, 5, 5, 0);
            autoLayout = true;
            autoLayoutDirection = LayoutDirection.Vertical;
            autoLayoutPadding.top = 5;
            autoLayoutPadding.right = 5;
            builtinKeyNavigation = true;
            clipChildren = true;
            freeScroll = false;
            scrollWheelDirection = UIOrientation.Vertical;
            verticalScrollbar = new UIScrollbar();
            scrollWheelAmount = 10;
            verticalScrollbar.stepSize = 1f;
            verticalScrollbar.incrementAmount = 10f;
            SetupControls();
        }

        private void SetupControls()
        {
            labelpanel = this.AddUIComponent<UIPanel>();
            labelpanel.height = 20;

            label = labelpanel.AddUIComponent<UILabel>();
            label.relativePosition =  new Vector3(80,0);
            label.width = 240;
            label.textAlignment = UIHorizontalAlignment.Center;
            label.text = "No Settings";

            ricoEnabled = UIUtils.CreateCheckBar(this, "Enable RICO" );

            enableRICOPanel = this.AddUIComponent<UIPanel>();
            enableRICOPanel.height = 0;
            enableRICOPanel.isVisible = false;
            enableRICOPanel.name = "OptionsPanel";

            ricoEnabled.eventCheckChanged += (c, state) =>
            {
                if (!state)
                {
                    enableRICOPanel.height = 0;
                    enableRICOPanel.isVisible = false;
                }

                else {
                    enableRICOPanel.height = 240;
                    enableRICOPanel.isVisible = true;
                }
            };

            service = UIUtils.CreateDropDown(enableRICOPanel, 0, "Service");
            service.items = Service;
            service.selectedIndex = 0;
            service.eventSelectedIndexChanged += UpdateService; 

            subService = UIUtils.CreateDropDown(enableRICOPanel, 30, "Sub-Service");
            subService.selectedIndex = 0;

            uiCategory = UIUtils.CreateDropDown(enableRICOPanel, 60, "UI Category");
            uiCategory.selectedIndex = 0;
            uiCategory.items = UICategory;

            level = UIUtils.CreateDropDown(enableRICOPanel, 90, "Level");
            level.selectedIndex = 0;
            level.items = Level;

            construction = UIUtils.CreateTextField(enableRICOPanel, 120, "Construction Cost");

            manual = UIUtils.CreateTextField(enableRICOPanel, 150, "Worker/Home Count");

            popBalanceEnabled = UIUtils.CreateCheckBox(enableRICOPanel, 180, "Use WG Realistic Pop");

            //popBalanceEnabled = UIUtils.CreateCheckBox(enableRICOPanel, 210, "Use Education Ratios");

            pollutionEnabled = UIUtils.CreateCheckBox(enableRICOPanel, 210, "Enable Pollution");


        }

        public void UpdateService(UIComponent c, int value)
        {
            //Update options panel if the service is changed. 

            if (!disableEvents)
            {
                if (value == 0) UpdateElements("none");
                else if (value == 1) UpdateElements("residential");
                else if (value == 2) UpdateElements("industrial");
                else if (value == 3) UpdateElements("office");
                else if (value == 4) UpdateElements("commercial");
                else if (value == 5) UpdateElements("extractor");
                else if (value == 6) UpdateElements("dummy");
            }
        }

        public void SaveRICO()
        {

            //Reads current settings from UI elements, and saves them to the XMLData. 


            if (service.selectedIndex == 0)
            {
                currentSelection.service = "none";
            }

            else if (service.selectedIndex == 1)
            {
                currentSelection.service = "residential";
                if (subService.selectedIndex == 0) currentSelection.subService = "high";
                else if (subService.selectedIndex == 1) currentSelection.subService = "low";
               // else if (subService.selectedIndex == 2) currentSelection.subService = "high eco";
                //else if (subService.selectedIndex == 3) currentSelection.subService = "low eco";
            }
            else if (service.selectedIndex == 2)
            {
                currentSelection.service = "industrial";

                if (subService.selectedIndex == 0) currentSelection.subService = "generic";
                else if (subService.selectedIndex == 1) currentSelection.subService = "forest";
                else if (subService.selectedIndex == 2) currentSelection.subService = "oil";
                else if (subService.selectedIndex == 3) currentSelection.subService = "ore";
                else if (subService.selectedIndex == 4) currentSelection.subService = "farming";
            }
            else if (service.selectedIndex == 3)
            {
                currentSelection.service = "office";

                if (subService.selectedIndex == 0) currentSelection.subService = "none";
                //else if (subService.selectedIndex == 1) currentSelection.subService = "high tech";
            }
            else if (service.selectedIndex == 4)
            {
                currentSelection.service = "commercial";
                if (subService.selectedIndex == 0) currentSelection.subService = "high";
                else if (subService.selectedIndex == 1) currentSelection.subService = "low";
                else if (subService.selectedIndex == 2) currentSelection.subService = "tourist";
                else if (subService.selectedIndex == 3) currentSelection.subService = "leisure";
                //else if (subService.selectedIndex == 4) currentSelection.subService = "eco";
            }
            else if (service.selectedIndex == 5)
            {
                currentSelection.service = "extractor";
                if (subService.selectedIndex == 0) currentSelection.subService = "forest";
                else if (subService.selectedIndex == 1) currentSelection.subService = "oil";
                else if (subService.selectedIndex == 2) currentSelection.subService = "ore";
                else if (subService.selectedIndex == 3) currentSelection.subService = "farming";

            }
            else if (service.selectedIndex == 6)
            {
                currentSelection.service = "dummy";
                currentSelection.subService = "none";

            }

            var d = Util.WorkplaceDistributionOf(currentSelection.service, currentSelection.subService, "Level" + currentSelection.level);
            var a = WorkplaceAIHelper.distributeWorkplaceLevels(int.Parse(manual.text), d, new int[] { 0, 0, 0, 0 });

            currentSelection.workplaces = a; 

            currentSelection.constructionCost = int.Parse(construction.text);
            
            currentSelection.homeCount = int.Parse(manual.text);


            if (uiCategory.selectedIndex == 0) currentSelection.uiCategory = "reslow";
            else if (uiCategory.selectedIndex == 1) currentSelection.uiCategory = "reshigh";
            else if (uiCategory.selectedIndex == 2) currentSelection.uiCategory = "comlow";
            else if (uiCategory.selectedIndex == 3) currentSelection.uiCategory = "comhigh";
            else if (uiCategory.selectedIndex == 4) currentSelection.uiCategory = "office";
            else if (uiCategory.selectedIndex == 5) currentSelection.uiCategory = "industrial";
            else if (uiCategory.selectedIndex == 6) currentSelection.uiCategory = "farming";
            else if (uiCategory.selectedIndex == 7) currentSelection.uiCategory = "oil";
            else if (uiCategory.selectedIndex == 8) currentSelection.uiCategory = "forest";
            else if (uiCategory.selectedIndex == 9) currentSelection.uiCategory = "ore";
            else if (uiCategory.selectedIndex == 10) currentSelection.uiCategory = "tourist";
            else if (uiCategory.selectedIndex == 11) currentSelection.uiCategory = "leisure";
            else if (uiCategory.selectedIndex == 12) currentSelection.uiCategory = "none";

            currentSelection.level = level.selectedIndex + 1;
            currentSelection.ricoEnabled = ricoEnabled.isChecked;
            currentSelection.popbalanceEnabled = !popBalanceEnabled.isChecked;

       
        }     

        public void SelectionChanged(BuildingData buildingData) {

            //When dropdowns are updated, this disables the event logic
            disableEvents = true;

            ricoEnabled.Enable();
            service.Enable();
            subService.Enable();
            level.Enable();
            uiCategory.Enable();
            construction.Enable();
            manual.Enable();
            popBalanceEnabled.Enable();

            //If selected asset has local settings, update option UI elements with those settings. 
            if (buildingData.hasLocal)
            {
                currentSelection = buildingData.local;
                UpdateElements(buildingData.local.service);
                UpdateValues(buildingData.local);
                label.text = "Local Settings";
                disableEvents = false;
                return;
            }
            else if (buildingData.hasAuthor)
            {
                currentSelection = buildingData.author;
                UpdateElements(buildingData.author.service);
                UpdateValues(buildingData.author);
                label.text = "Author Settings";
                ricoEnabled.Disable();
                service.Disable();
                subService.Disable();
                level.Disable();
                uiCategory.Disable();
                construction.Disable();
                manual.Disable();
                popBalanceEnabled.Disable();
                disableEvents = false;
                return;
            }
            else if (buildingData.hasMod)
            {
                currentSelection = buildingData.mod;
                label.text = "Mod Settings";
                UpdateElements(buildingData.mod.service);
                UpdateValues(buildingData.mod);           
                ricoEnabled.Disable();
                service.Disable();
                subService.Disable();
                level.Disable();
                uiCategory.Disable();
                construction.Disable();
                manual.Disable();
                popBalanceEnabled.Disable();
                disableEvents = false;
                return;
            }
            else {
                
                ricoEnabled.isChecked = false;
                ricoEnabled.Disable();
                label.text = "No Settings";
            }

            disableEvents = false;
        }

        public void NoSettings() {

            //Hide all options if selected building has no RICO settings. 

            ricoEnabled.Disable();
        }

        public void UpdateValues(RICOBuilding buildingData)
        {
            //Updates the values in the RICO options panel to match the selected building. 

            manual.text = buildingData.workplaceCount.ToString();
          

            if (buildingData.service == "residential")
                {
                manual.text = buildingData.homeCount.ToString();
                service.selectedIndex = 1;


                if (currentSelection.subService == "high") subService.selectedIndex = 0;
                else if (currentSelection.subService == "low") subService.selectedIndex = 1;
               // else if (currentSelection.subService == "high eco") subService.selectedIndex = 2;
                //else if (currentSelection.subService == "low eco") subService.selectedIndex = 3;

            }

                else if (buildingData.service == "industrial")
                {
                    service.selectedIndex = 2;
                    subService.items = IndustrialSub;

                if (currentSelection.subService == "generic") subService.selectedIndex = 0;
                else if (currentSelection.subService == "forest") subService.selectedIndex = 1;
                else if (currentSelection.subService == "oil") subService.selectedIndex = 2;
                else if (currentSelection.subService == "ore") subService.selectedIndex = 3;
                else if (currentSelection.subService == "farming") subService.selectedIndex = 4;

            }

                else if (buildingData.service == "office")
                {             
                    service.selectedIndex = 3;
                    subService.items = OfficeSub;

                if (currentSelection.subService == "none") subService.selectedIndex = 0;
                //else if (currentSelection.subService == "high tech") subService.selectedIndex = 1;

            }

                else if (buildingData.service == "commercial")
                {
                    service.selectedIndex = 4;
                    subService.items = ComSub;

                if (currentSelection.subService == "high") subService.selectedIndex = 0;
                else if (currentSelection.subService == "low") subService.selectedIndex = 1;
                else if (currentSelection.subService == "tourist") subService.selectedIndex = 2;
                else if (currentSelection.subService == "leisure") subService.selectedIndex = 3;
                //else if (currentSelection.subService == "eco") subService.selectedIndex = 4;

            }

                else if (buildingData.service == "extractor")
                {      
                    service.selectedIndex = 5;
                    subService.items = ExtractorSub;

                if (currentSelection.subService == "forest") subService.selectedIndex = 0;
                else if (currentSelection.subService == "oil") subService.selectedIndex = 1;
                else if (currentSelection.subService == "ore") subService.selectedIndex = 2;
                else if (currentSelection.subService == "farming") subService.selectedIndex = 3;
                }

            else if (buildingData.service == "dummy")
            {
                service.selectedIndex = 6;
                subService.selectedIndex = 0;
                subService.items = OfficeSub;

            }

            if (buildingData.uiCategory == "reslow") uiCategory.selectedIndex = 0;
                else if (buildingData.uiCategory == "reshigh") uiCategory.selectedIndex = 1;
                else if (buildingData.uiCategory == "comlow") uiCategory.selectedIndex = 2;
                else if (buildingData.uiCategory == "comhigh") uiCategory.selectedIndex = 3;
                else if (buildingData.uiCategory == "office") uiCategory.selectedIndex = 4;
                else if (buildingData.uiCategory == "industrial") uiCategory.selectedIndex = 5;
                else if (buildingData.uiCategory == "farming") uiCategory.selectedIndex = 6;
                else if (buildingData.uiCategory == "oil") uiCategory.selectedIndex = 7;
                else if (buildingData.uiCategory == "forest") uiCategory.selectedIndex = 8;
                else if (buildingData.uiCategory == "ore") uiCategory.selectedIndex = 9;
                else if (buildingData.uiCategory == "tourist") uiCategory.selectedIndex = 10;
                else if (buildingData.uiCategory == "leisure") uiCategory.selectedIndex = 11;
                else if (buildingData.uiCategory == "none") uiCategory.selectedIndex = 12;

            level.selectedIndex = (buildingData.level - 1);

                popBalanceEnabled.isChecked = !buildingData.popbalanceEnabled;
                construction.text = buildingData.constructionCost.ToString();

               ricoEnabled.isChecked = buildingData.ricoEnabled;
        }

        public void UpdateElements(string service) {

            //Reconfigure the RICO options panel to display relevant options for a given service.
            //This simply hides/shows different option fields for the various services. 

            pollutionEnabled.enabled = true;


            if (service == "residential")
                {
                    level.items = resLevel;
                    subService.items = ResSub;
               
                pollutionEnabled.enabled = false;

            }
                else if (service == "office")
                {
                    level.items = Level;
                    subService.items = OfficeSub;
                    pollutionEnabled.enabled = false;
            }
                else if (service == "industrial")
                {
                    level.items = Level;
                    subService.items = IndustrialSub;
                }
                else if (service == "extractor")
                {
                    level.items = extLevel;
                    subService.items = ExtractorSub;
                }
                else if (service == "commercial")
                {
                    level.items = Level;
                    subService.items = ComSub;
                    pollutionEnabled.enabled = false;
            }
                else if (service == "none" || service == "dummy")
                {

                pollutionEnabled.enabled = false;

            }
            
        }
    }
}