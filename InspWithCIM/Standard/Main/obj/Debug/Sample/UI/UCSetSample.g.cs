﻿#pragma checksum "..\..\..\..\Sample\UI\UCSetSample.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "FE94DDBA95B5A58A1C581372A2BC4B81085EB1B22B74ADD168C167E44C41C3D3"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using Camera;
using HalconDotNet;
using Main;
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


namespace Main {
    
    
    /// <summary>
    /// UCSetSample
    /// </summary>
    public partial class UCSetSample : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 24 "..\..\..\..\Sample\UI\UCSetSample.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ScrollViewer scrollViewer;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\..\Sample\UI\UCSetSample.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgResult;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\..\Sample\UI\UCSetSample.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Camera.BaseUCDisplayCamera ucSetDisplay;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\..\..\Sample\UI\UCSetSample.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblDefect;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\..\..\Sample\UI\UCSetSample.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnLoadImg;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\..\Sample\UI\UCSetSample.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSaveParams;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\..\..\Sample\UI\UCSetSample.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnNextImg;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\..\..\Sample\UI\UCSetSample.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbSide;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\..\..\Sample\UI\UCSetSample.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnTestImg;
        
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
            System.Uri resourceLocater = new System.Uri("/机器视觉控制处理软件;component/sample/ui/ucsetsample.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Sample\UI\UCSetSample.xaml"
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
            
            #line 10 "..\..\..\..\Sample\UI\UCSetSample.xaml"
            ((Main.UCSetSample)(target)).Loaded += new System.Windows.RoutedEventHandler(this.UserControl_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.scrollViewer = ((System.Windows.Controls.ScrollViewer)(target));
            
            #line 26 "..\..\..\..\Sample\UI\UCSetSample.xaml"
            this.scrollViewer.PreviewMouseWheel += new System.Windows.Input.MouseWheelEventHandler(this.ScrollViewer_PreviewMouseWheel);
            
            #line default
            #line hidden
            return;
            case 3:
            this.dgResult = ((System.Windows.Controls.DataGrid)(target));
            
            #line 29 "..\..\..\..\Sample\UI\UCSetSample.xaml"
            this.dgResult.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.DgResult_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 4:
            this.ucSetDisplay = ((Camera.BaseUCDisplayCamera)(target));
            return;
            case 5:
            this.lblDefect = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.btnLoadImg = ((System.Windows.Controls.Button)(target));
            
            #line 57 "..\..\..\..\Sample\UI\UCSetSample.xaml"
            this.btnLoadImg.Click += new System.Windows.RoutedEventHandler(this.BtnLoadImg_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btnSaveParams = ((System.Windows.Controls.Button)(target));
            
            #line 58 "..\..\..\..\Sample\UI\UCSetSample.xaml"
            this.btnSaveParams.Click += new System.Windows.RoutedEventHandler(this.BtnSaveParams_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.btnNextImg = ((System.Windows.Controls.Button)(target));
            
            #line 59 "..\..\..\..\Sample\UI\UCSetSample.xaml"
            this.btnNextImg.Click += new System.Windows.RoutedEventHandler(this.BtnNextImg_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.cbSide = ((System.Windows.Controls.ComboBox)(target));
            
            #line 60 "..\..\..\..\Sample\UI\UCSetSample.xaml"
            this.cbSide.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.CbSide_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 10:
            this.btnTestImg = ((System.Windows.Controls.Button)(target));
            
            #line 64 "..\..\..\..\Sample\UI\UCSetSample.xaml"
            this.btnTestImg.Click += new System.Windows.RoutedEventHandler(this.BtnTestImg_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

