using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.Design;
using System.ComponentModel.Design;
using System.ComponentModel;
using System.Drawing.Design;

namespace avt.DynamicFlashRotator.Net
{
    public class DynamicRotatorDesigner : ControlDesigner
    {
      
        //public override DesignerActionListCollection ActionLists
        //{
        //    get
        //    {
        //        DesignerActionListCollection lists = new DesignerActionListCollection();
        //        lists.AddRange(base.ActionLists);
        //        lists.Add(new RotatorActionList(this));
        //        return lists;
        //    }
        //}

        //internal void EditSlides()
        //{
        //    PropertyDescriptor context = TypeDescriptor.GetProperties(base.Component)["Slides"];
        //    ControlDesigner.InvokeTransactedChange(base.Component, new TransactedChangeCallback(this.EditItemsCallback), context, "Edit Slides", context);
        //}

        //private bool EditItemsCallback(object context)
        //{
        //    IDesignerHost service = (IDesignerHost)this.GetService(typeof(IDesignerHost));
        //    PropertyDescriptor propDesc = (PropertyDescriptor)context;
        //    new SlideCollectionEditor(typeof(SlideCollection)).EditValue(null, propDesc.GetValue(base.Component));//,new TypeDescriptorContext(service, propDesc, base.Component), new WindowsFormsEditorServiceHelper(this), propDesc.GetValue(base.Component));
        //    return true;
        //}




        //private class RotatorActionList : DesignerActionList
        //{
        //    // Fields
        //    DynamicRotatorDesigner _parent;
        //    DynamicRotator _rotator;

        //    // Methods
        //    public RotatorActionList(DynamicRotatorDesigner parent)
        //        : base(parent.Component)
        //    {
        //        this._parent = parent;
        //        _rotator = parent.Component as DynamicRotator;
        //    }

        //    public void ClearRegion()
        //    {
        //        //this._parent.ClearRegion();
        //    }

        //    public void EditSlides()
        //    {
        //        this._parent.EditSlides();
        //    }


        //    public override DesignerActionItemCollection GetSortedActionItems()
        //    {
        //        DesignerActionItemCollection items = new DesignerActionItemCollection();

        //        items.Add(new DesignerActionHeaderItem("Load Predefined Template"));

        //        items.Add(new DesignerActionPropertyItem("Test", "Test", "Some Test"));

        //        foreach (SlideInfo slide in _rotator.Slides) {
        //            items.Add(new DesignerActionPropertyItem("Test", "Test", "Some Test"));
        //        }

        //        items.Add(new DesignerActionPropertyItem("Slides", "Slides", "Some Test"));
                
        //        items.Add(new DesignerActionMethodItem(this, "EditSlides", "900x250", "Load Predefined Template", string.Empty, true));
        //        items.Add(new DesignerActionMethodItem(this, "EditSlides", "Skyscrapper", "Load Predefined Template", string.Empty, true));
        //        items.Add(new DesignerActionMethodItem(this, "EditSlides", "Edit Slides", string.Empty, string.Empty, true));
        //        return items;

        //        //ContentDesignerState showDefaultContent = ContentDesignerState.ShowDefaultContent;
        //        ////if ((this._parent.ContentResolutionService != null) && (this._parent.GetContentDefinition() != null)) {
        //        ////    showDefaultContent = this._parent.ContentResolutionService.GetContentDesignerState(this._parent.GetContentDefinition().ContentPlaceHolderID);
        //        ////}
        //        //if (showDefaultContent == ContentDesignerState.ShowDefaultContent) {
                    
        //        //    return items;
        //        //}
        //        //if (ContentDesignerState.ShowUserContent == showDefaultContent) {
        //        //    items.Add(new DesignerActionMethodItem(this, "ClearRegion", "Content_ClearRegion", string.Empty, string.Empty, true));
        //        //}
        //        //return items;
        //    }

        //    public string Test { get { return ""; } set { } }

        //    [Editor(typeof(avt.DynamicFlashRotator.Net.SlideCollectionEditor), typeof(UITypeEditor))]
        //    public SlideCollection Slides { get { return _rotator.Slides; } }

        //    // Properties
        //    public override bool AutoShow { get { return true; } set { } }
        //}
       
    }
}
