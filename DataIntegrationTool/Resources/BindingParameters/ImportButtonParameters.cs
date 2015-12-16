
using DataIntegrationTool.Resources.Enums;

namespace DataIntegrationTool.BindingParameters
{
    public class ImportButtonParameters 
    {
        public GroupBoxToPopulate.Name GroupBoxName { get; set; }
        public ImportSource.FileSource SelectedImportOption { get; set; }
    }
}
