﻿#pragma checksum "..\..\..\NotifiWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8BDC11F400DAC86B4812A0E28009BAE51E7EBD54"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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
using adminWPF;


namespace adminWPF {
    
    
    /// <summary>
    /// NotifiWindow
    /// </summary>
    public partial class NotifiWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\..\NotifiWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox idTXT;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\NotifiWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox headerTXT;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\NotifiWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox bodyTXT;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\NotifiWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button sendBTN;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\NotifiWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView usersLV;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\NotifiWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker endDatePicker;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\NotifiWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker startDatePicker;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.4.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/adminWPF;component/notifiwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\NotifiWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.4.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.idTXT = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.headerTXT = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.bodyTXT = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.sendBTN = ((System.Windows.Controls.Button)(target));
            
            #line 13 "..\..\..\NotifiWindow.xaml"
            this.sendBTN.Click += new System.Windows.RoutedEventHandler(this.sendBTN_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.usersLV = ((System.Windows.Controls.ListView)(target));
            
            #line 14 "..\..\..\NotifiWindow.xaml"
            this.usersLV.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.usersLV_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.endDatePicker = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 7:
            this.startDatePicker = ((System.Windows.Controls.DatePicker)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

