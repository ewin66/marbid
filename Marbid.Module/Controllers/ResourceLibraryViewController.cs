using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using Marbid.Module.BusinessObjects.Library;
using System.Collections;

namespace Marbid.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class ResourceLibraryViewController : ViewController
    {
        public ResourceLibraryViewController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }

        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        private void AddCategory_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            ResourceLibrary currentResource = (ResourceLibrary)View.CurrentObject;
            View.ObjectSpace.SetModified(currentResource);
            foreach (ResourceCategory emp in e.PopupWindow.View.SelectedObjects)
            {
                ResourceCategory nEmp = ObjectSpace.GetObjectByKey<ResourceCategory>(emp.Oid);
                currentResource.ResourceCategories.Add(nEmp);
            }
            if (View is DetailView && ((DetailView)View).ViewEditMode == ViewEditMode.View)
            {
                View.ObjectSpace.CommitChanges();
                Frame.View.ObjectSpace.Refresh();
            }
        }

        private void AddCategory_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            ResourceLibrary currentResource = (ResourceLibrary)View.CurrentObject;
            IObjectSpace objectSpace = Application.CreateObjectSpace(typeof(ResourceCategory));
            string resourceCategoryListId = Application.FindLookupListViewId(typeof(ResourceCategory));
            CollectionSourceBase collectionSource = Application.CreateCollectionSource(objectSpace, typeof(ResourceCategory), resourceCategoryListId);
            ListView view = Application.CreateListView(resourceCategoryListId, collectionSource, true);
            //view.CollectionSource.Criteria["Filter1"] = CriteriaOperator.Parse("[Organization] = ?", objectSpace.GetObject<Organization>(currentSchedule.Organization));
            e.Context = TemplateContext.FindPopupWindow;
            e.View = view;
        }

        private void RemoveCategory_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            ResourceLibrary currentResource = (ResourceLibrary)View.CurrentObject;
            View.ObjectSpace.SetModified(currentResource);

            ArrayList arrayList = new ArrayList();

            foreach (TemporaryResourceCategory temp in e.PopupWindow.View.SelectedObjects)
            {
                foreach (ResourceCategory cat in currentResource.ResourceCategories)
                {
                    if (cat.Oid == temp.Category.Oid)
                    {
                        arrayList.Add(cat);
                    }
                }
            }
            foreach (ResourceCategory toDelete in arrayList)
            {
                currentResource.ResourceCategories.Remove(toDelete);
            }

            if (View is DetailView && ((DetailView)View).ViewEditMode == ViewEditMode.View)
            {
                View.ObjectSpace.CommitChanges();
                Frame.View.ObjectSpace.Refresh();
            }
        }

        private void RemoveCategory_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace objectSpace = Application.CreateObjectSpace(typeof(TemporaryResourceCategory));
            string participantListId = Application.FindLookupListViewId(typeof(TemporaryResourceCategory));
            CollectionSource collectionSource = new CollectionSource(objectSpace, typeof(TemporaryResourceCategory));
            ResourceLibrary currentSchedule = (ResourceLibrary)View.CurrentObject;

            foreach (ResourceCategory detail in currentSchedule.ResourceCategories)
            {
                TemporaryResourceCategory tempCategory = objectSpace.CreateObject<TemporaryResourceCategory>();
                tempCategory.Category = objectSpace.GetObjectByKey<ResourceCategory>(detail.Oid);
                collectionSource.Add(tempCategory);
            }


            e.View = Application.CreateListView(participantListId, collectionSource, false);
            e.View.Caption = "Remove Category";
            e.Context = TemplateContext.FindPopupWindow;
        }
    }
}