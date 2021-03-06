using System;
using System.Collections.Generic;
using Autodesk.Revit.DB;

namespace RevitTestExternalCommand
{
    class SharedParameterBindingManager
    {

        // This class in NOT Working Correctli!!!!
        /*



        public String Name { get; set; }
        public ParameterType Type { get; set; }
        public bool UserModifiable { get; set; }
        public bool UserVisible { get; set; }
        public String Description { get; set; }
        public bool Instance { get; set; }
        public Definition Definition { get; set; }
        public BuiltInParameterGroup ParameterGroup { get; set; }

        List<BuiltInCategory> m_categories = new List<BuiltInCategory>();
        public SharedParameterBindingManager()
        {
            Name = "Invalid";
            Type = ParameterType.Text;
            UserModifiable = true;
            UserVisible = true;
            Description = "";
            Instance = true;
            Definition = null;
            ParameterGroup = BuiltInParameterGroup.PG_IDENTITY_DATA;
        }

        

        public ExternalDefinitionCreationOptions GetCreationOptions()
        {
            ExternalDefinitionCreationOptions options = new ExternalDefinitionCreationOptions(Name, Type);
            options.UserModifiable = UserModifiable;
            options.Visible = UserVisible;
            options.Description = Description;
            return options;
        }

        public void AddCategory(BuiltInCategory category)
        {
            m_categories.Add(category);
        }

        private CategorySet GetCategories(Document doc)
        {
            Categories categories = doc.Settings.Categories;

            CategorySet categorySet = new CategorySet();

            foreach (BuiltInCategory bic in m_categories)
            {
                categorySet.Insert(categories.get_Item(bic));
            }

            return categorySet;
        }

        public void AddBindings(Document doc)
        {
            Binding binding;
            if (Instance)
            {
                binding = new InstanceBinding(GetCategories(doc));
            }
            else
            {
                binding = new TypeBinding(GetCategories(doc));
            }
            // assumes transaction open
            doc.ParameterBindings.Insert(Definition, binding, ParameterGroup);
        }


    */
    }
}