﻿#pragma checksum "..\..\..\..\..\CIM\UI\WndCimMode.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "F124C203DADF4C97121BF7C0A23CCF964385759F2191142B959C792F2DC0E241"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using DealCIM;
using Panuon.UI;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace DealCIM {
    
    
    /// <summary>
    /// WndCimMode
    /// </summary>
    public partial class WndCimMode : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\..\..\..\CIM\UI\WndCimMode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton tbtnGetCode;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\..\..\CIM\UI\WndCimMode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton tbtnPost;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\..\..\CIM\UI\WndCimMode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton tbtnVerifyChipid;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\..\..\CIM\UI\WndCimMode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton tbtnPassCodeNG;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\..\..\CIM\UI\WndCimMode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton tbtnInsp;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\..\..\CIM\UI\WndCimMode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnConfirm;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\..\..\CIM\UI\WndCimMode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCancel;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/DealCIM;component/cim/ui/wndcimmode.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\CIM\UI\WndCimMode.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 9 "..\..\..\..\..\CIM\UI\WndCimMode.xaml"
            ((DealCIM.WndCimMode)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            
            #line default
            #line hidden
            
            #line 9 "..\..\..\..\..\CIM\UI\WndCimMode.xaml"
            ((DealCIM.WndCimMode)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.tbtnGetCode = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            
            #line 17 "..\..\..\..\..\CIM\UI\WndCimMode.xaml"
            this.tbtnGetCode.Checked += new System.Windows.RoutedEventHandler(this.TbtnGetCode_Checked);
            
            #line default
            #line hidden
            
            #line 17 "..\..\..\..\..\CIM\UI\WndCimMode.xaml"
            this.tbtnGetCode.Unchecked += new System.Windows.RoutedEventHandler(this.TbtnGetCode_Unchecked);
            
            #line default
            #line hidden
            return;
            case 3:
            this.tbtnPost = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            
            #line 20 "..\..\..\..\..\CIM\UI\WndCimMode.xaml"
            this.tbtnPost.Checked += new System.Windows.RoutedEventHandler(this.TbtnPost_Checked);
            
            #line default
            #line hidden
            
            #line 20 "..\..\..\..\..\CIM\UI\WndCimMode.xaml"
            this.tbtnPost.Unchecked += new System.Windows.RoutedEventHandler(this.TbtnPost_Unchecked);
            
            #line default
            #line hidden
            return;
            case 4:
            this.tbtnVerifyChipid = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            
            #line 24 "..\..\..\..\..\CIM\UI\WndCimMode.xaml"
            this.tbtnVerifyChipid.Checked += new System.Windows.RoutedEventHandler(this.TbtnVerifyChipid_Checked);
            
            #line default
            #line hidden
            
            #line 24 "..\..\..\..\..\CIM\UI\WndCimMode.xaml"
            this.tbtnVerifyChipid.Unchecked += new System.Windows.RoutedEventHandler(this.TbtnVerifyChipid_Unchecked);
            
            #line default
            #line hidden
            return;
            case 5:
            this.tbtnPassCodeNG = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            
            #line 28 "..\..\..\..\..\CIM\UI\WndCimMode.xaml"
            this.tbtnPassCodeNG.Checked += new System.Windows.RoutedEventHandler(this.TbtnPassCodeNG_Checked);
            
            #line default
            #line hidden
            
            #line 28 "..\..\..\..\..\CIM\UI\WndCimMode.xaml"
            this.tbtnPassCodeNG.Unchecked += new System.Windows.RoutedEventHandler(this.TbtnPassCodeNG_Unchecked);
            
            #line default
            #line hidden
            return;
            case 6:
            this.tbtnInsp = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            
            #line 36 "..\..\..\..\..\CIM\UI\WndCimMode.xaml"
            this.tbtnInsp.Checked += new System.Windows.RoutedEventHandler(this.tbtnInsp_Checked);
            
            #line default
            #line hidden
            
            #line 36 "..\..\..\..\..\CIM\UI\WndCimMode.xaml"
            this.tbtnInsp.Unchecked += new System.Windows.RoutedEventHandler(this.tbtnInsp_Unchecked);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btnConfirm = ((System.Windows.Controls.Button)(target));
            
            #line 45 "..\..\..\..\..\CIM\UI\WndCimMode.xaml"
            this.btnConfirm.Click += new System.Windows.RoutedEventHandler(this.BtnConfirm_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.btnCancel = ((System.Windows.Controls.Button)(target));
            
            #line 46 "..\..\..\..\..\CIM\UI\WndCimMode.xaml"
            this.btnCancel.Click += new System.Windows.RoutedEventHandler(this.BtnCancel_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

