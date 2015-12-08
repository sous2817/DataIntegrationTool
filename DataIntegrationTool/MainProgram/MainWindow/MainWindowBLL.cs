﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Deployment.Application;
using System.Diagnostics;
using System.ServiceModel.Configuration;
using DataIntegrationTool.BaseClasses;
using DataIntegrationTool.MainProgram.CleanData;
using DataIntegrationTool.MainProgram.EvaluateMatches;
using DataIntegrationTool.MainProgram.ExportData;
using DataIntegrationTool.MainProgram.ImportData;
using DataIntegrationTool.MainProgram.Welcome;
using DataIntegrationTool.MessengerPackages;
using DataIntegrationTool.Resources.Enums;
using GalaSoft.MvvmLight;
using MahApps.Metro.Controls.Dialogs;

namespace DataIntegrationTool.MainProgram.MainWindow
{
    public static class MainWindowBLL
    {

        public static ReadOnlyCollection<ViewModelBase> GetWizardSteps()
        {

            var steps = new List<ViewModelBase>
            {
                new WelcomeViewModel(),
                new ImportDataViewModel(new DialogCoordinator()),
                new CleanDataViewModel(),
                new EvaluateMatchesViewModel(),
                new ExportDataViewModel()
            };

            return new ReadOnlyCollection<ViewModelBase>(steps);
        }

        public static string GetCurrentVerision()
        {
            return ApplicationDeployment.IsNetworkDeployed ?
                ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString() :
                "Debugging";
        }

        public static void NextStepPackage(DataIntegrationViewModelBase currentStep)
        {
            switch (currentStep.LocatorName)
            {
                case WizardSteps.LocatorNames.ImportData:
                    var importStep = (ImportDataViewModel) currentStep;

                    var dataPackages = new List<ImportDataPackage>();

                    if (importStep.BaseData.ImportedData != null)
                    {
                        dataPackages.Add(importStep.BaseData);
                    }

                    var importToCleanPackage = new ImportToCleanPackage {ImportedPackages = dataPackages};

                    break;
                default:
                    break;
            }
        }
        public static string GetCurrentEnvironment()
        {
            var clientSection = (ClientSection)ConfigurationManager.GetSection("system.serviceModel/client");
            var address = string.Empty;
            for (var i = 0; i < clientSection.Endpoints.Count; i++)
            {
                if (clientSection.Endpoints[i].Name != "Semio.Provide") continue;
                address = clientSection.Endpoints[i].Address.ToString();
                break;
            }
            var environemntName = "Unknown";
            switch (address)
            {
                case "http://id-dev-web.quintiles.net:8001/Service.svc":
                    environemntName = "Development";
                    return environemntName;
                case "usndywl10062:8001/Service.svc":
                    environemntName = "Local";
                    return environemntName;
                case "https://infosario-design-prod.quintiles.com:8001/Service.svc":
                    environemntName = "Production";
                    return environemntName;
                case "https://infosario-design-uat.quintiles.com:8001/Service.svc":
                    environemntName = "UAT";
                    return environemntName;
                case "https://infosario-design-test.quintiles.com:8001/Service.svc":
                    environemntName = "Test";
                    return environemntName;
                case "https://infosario-design-test01.quintiles.com:8001/Service.svc":
                    environemntName = "Test 1";
                    return environemntName;
                case "https://infosario-design-test02.quintiles.com:8001/Service.svc":
                    environemntName = "Test 2";
                    return environemntName;
            }
            return environemntName;
        }

    }
}
