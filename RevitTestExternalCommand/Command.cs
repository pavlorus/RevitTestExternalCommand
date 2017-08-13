using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitTestExternalCommand
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            //FilteredElementCollector col
            //    = new FilteredElementCollector(doc)
            //        .WhereElementIsNotElementType()
            //        .OfCategory(BuiltInCategory.INVALID)
            //        .OfClass(typeof(Wall));


            // Modify document within a transaction

            using (Transaction tx = new Transaction(doc))
            {
                tx.Start("Transaction Name");

                //SharedParameterBindingManager manager = new SharedParameterBindingManager();
                
                //DefinitionGroup dGroup = dFile.Groups.Create("Demo group");
                //manager.Definition = dGroup.Definitions.Create(manager.GetCreationOptions());
                //manager.AddCategory(BuiltInCategory.OST_ProjectInformation);
                //manager.AddBindings(doc);





                 app.SharedParametersFilename =
                    @"D:\Files\Data\Autodesk Revit\00_Templeate\00_Main Project\SP_ForTesting1.txt";
                DefinitionFile dFile = app.OpenSharedParameterFile() ;

                Category wall = doc.Settings.Categories.get_Item(BuiltInCategory.OST_Walls);
                CategorySet cats1 = doc.Application.Create.NewCategorySet();
                cats1.Insert(wall);


                SharedParameterManager spManager = new SharedParameterManager();

                spManager.AplicationRvt = app;
                spManager.FilePathToSharedFile =
                    @"D:\Files\Data\Autodesk Revit\00_Templeate\00_Main Project\";
                spManager.SharedFileName ="SP_ForTesting2.txt";

                spManager.AddExistingSharedParameter("MyGroup1", "MyParam", cats1,
                    BuiltInParameterGroup.PG_IDENTITY_DATA, true);




                TaskDialog.Show("sdfsd", "Done");

                tx.Commit();
            }

            return Result.Succeeded;
        }
        

    }

    class SharedParameterManager
    {

        public string SharedFileName { get; set; }
        public string FilePathToSharedFile { get; set; }

        private string _exsistingSharedParameter=string.Empty;

        public Application AplicationRvt { get; set; }

        public SharedParameterManager()
        {
            
        }

        public SharedParameterManager(Application app, string sharedFileName, string filePathToSharedFile)
        {
                AplicationRvt = app;
                SharedFileName = sharedFileName;
                FilePathToSharedFile = filePathToSharedFile;
                GetExistingSharedParameterFile();
        }

        public void AddExistingSharedParameter(string groupOfSharedParam,
                                                    string name,
                                                    CategorySet cats,
                                                    BuiltInParameterGroup group,
                                                    bool inst)

        {
            try
            {
                AplicationRvt.SharedParametersFilename = GetNewSharedParameterFilePath();
                DefinitionFile defFile = AplicationRvt.OpenSharedParameterFile();
                if (defFile == null) throw new Exception("No SharedParameter File!");

                DefinitionGroups myGroups = defFile.Groups;
                DefinitionGroup myGroup = myGroups.get_Item(groupOfSharedParam);
                ExternalDefinition def = null;
                if (myGroup == null) throw new Exception("The group doesn't exist. Inspect name of group");
                def = myGroup.Definitions.get_Item(name) as ExternalDefinition;
                if (def == null) throw new Exception("The parameter doesnt exist");

                Binding binding = AplicationRvt.Create.NewTypeBinding(cats);
                if (inst) binding = AplicationRvt.Create.NewInstanceBinding(cats);

                BindingMap map = (new UIApplication(AplicationRvt)).ActiveUIDocument.Document.ParameterBindings;
                map.Insert(def, binding, group);
                SetExistingSharedParameterFile();
            }
            finally 
            {
                SetExistingSharedParameterFile();
            }
        }

        private void GetExistingSharedParameterFile()
        {
            _exsistingSharedParameter = AplicationRvt.SharedParametersFilename;
        }
        private void SetExistingSharedParameterFile()
        {
            AplicationRvt.SharedParametersFilename = _exsistingSharedParameter;
        }

        private string GetNewSharedParameterFilePath()
        {
            return FilePathToSharedFile + SharedFileName;
        }
    }




}    

