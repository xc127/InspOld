﻿#pragma checksum "..\..\..\..\..\CIM\UI\PostLotWnd.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "4F65BB011EDE6912F4141BD2FB41F894BD30BB78B744FE0B8A66FB17E0BF613C"
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
    /// PostLotWnd
    /// </summary>
    public partial class PostLotWnd : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 42 "..\..\..\..\..\CIM\UI\PostLotWnd.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label tbFab;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\..\..\CIM\UI\PostLotWnd.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label tbArea;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\..\..\CIM\UI\PostLotWnd.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label tbLine;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\..\..\CIM\UI\PostLotWnd.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label tbOperation;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\..\..\CIM\UI\PostLotWnd.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbUserID;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\..\..\CIM\UI\PostLotWnd.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbModelNo;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\..\..\..\CIM\UI\PostLotWnd.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbLot;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\..\..\..\CIM\UI\PostLotWnd.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Panuon.UI.PUButton btnPostLot;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\..\..\..\CIM\UI\PostLotWnd.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Panuon.UI.PUButton btnCancel;
        
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
            System.Uri resourceLocater = new System.Uri("/DealCIM;component/cim/ui/postlotwnd.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\CIM\UI\PostLotWnd.xaml"
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
            
            #line 9 "..\..\..\..\..\CIM\UI\PostLotWnd.xaml"
            ((DealCIM.PostLotWnd)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            
            #line default
            #line hidden
            
            #line 9 "..\..\..\..\..\CIM\UI\PostLotWnd.xaml"
            ((DealCIM.PostLotWnd)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            
            #line 9 "..\..\..\..\..\CIM\UI\PostLotWnd.xaml"
            ((DealCIM.PostLotWnd)(target)).KeyDown += new System.Windows.Input.KeyEventHandler(this.Window_KeyDown);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 20 "..\..\..\..\..\CIM\UI\PostLotWnd.xaml"
            ((System.Windows.Controls.Label)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Label_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.tbFab = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.tbArea = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.tbLine = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.tbOperation = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.tbUserID = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.tbModelNo = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.tbLot = ((System.Windows.Controls.TextBox)(target));
            return;
            case 10:
            this.btnPostLot = ((Panuon.UI.PUButton)(target));
            
            #line 59 "..\..\..\..\..\CIM\UI\PostLotWnd.xaml"
            this.btnPostLot.Click += new System.Windows.RoutedEventHandler(this.BtnPostLot_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.btnCancel = ((Panuon.UI.PUButton)(target));
            
            #line 60 "..\..\..\..\..\CIM\UI\PostLotWnd.xaml"
            this.btnCancel.Click += new System.Windows.RoutedEventHandler(this.BtnCancel_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
