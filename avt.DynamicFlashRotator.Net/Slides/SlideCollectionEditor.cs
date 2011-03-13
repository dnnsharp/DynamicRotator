using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Permissions;
using System.ComponentModel.Design;

namespace avt.DynamicFlashRotator.Net
{
    [SecurityPermission(SecurityAction.Demand, Flags=SecurityPermissionFlag.UnmanagedCode)]
    public class SlideCollectionEditor : CollectionEditor
    {
        // Methods
        public SlideCollectionEditor(Type type)
            : base(type)
        {
            
        }

        protected override bool CanSelectMultipleInstances()
        {
            return false;
        }

        // Properties
        protected override string HelpTopic
        {
            get
            {
                return "net.ComponentModel.CollectionEditor";
            }
        }
    }
}
