﻿#pragma checksum "..\..\SignUp.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "5F2C0C90CB67345DA3728E7ECEEA2513"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using CarWash_WPF;
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


namespace CarWash_WPF {
    
    
    /// <summary>
    /// SignUp
    /// </summary>
    public partial class SignUp : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\SignUp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSignUp;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\SignUp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblLogin;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\SignUp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblPassword;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\SignUp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textLogin;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\SignUp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox textPassword;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\SignUp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textFio;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\SignUp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblFio;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\SignUp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textEmail;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\SignUp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnClose;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\SignUp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnMinimize;
        
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
            System.Uri resourceLocater = new System.Uri("/CarWash WPF;component/signup.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\SignUp.xaml"
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
            this.btnSignUp = ((System.Windows.Controls.Button)(target));
            
            #line 16 "..\..\SignUp.xaml"
            this.btnSignUp.Click += new System.Windows.RoutedEventHandler(this.btnSignUp_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.lblLogin = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.lblPassword = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.textLogin = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.textPassword = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 6:
            this.textFio = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.lblFio = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.textEmail = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            
            #line 26 "..\..\SignUp.xaml"
            ((System.Windows.Controls.Grid)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Grid_MouseDown);
            
            #line default
            #line hidden
            return;
            case 10:
            this.btnClose = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\SignUp.xaml"
            this.btnClose.Click += new System.Windows.RoutedEventHandler(this.btnClose_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.btnMinimize = ((System.Windows.Controls.Button)(target));
            
            #line 28 "..\..\SignUp.xaml"
            this.btnMinimize.Click += new System.Windows.RoutedEventHandler(this.btnMinimize_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

