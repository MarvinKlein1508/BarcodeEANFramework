using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;

namespace BarcodeEANFramework
{
    [RunInstaller(true)]
    public partial class RegisterCOM : System.Configuration.Install.Installer
    {
        public RegisterCOM()
        {
            InitializeComponent();
        }

        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public override void Commit(IDictionary savedState)
        {
            base.Commit(savedState);

            // Get the location of regasm
            string regasmPath = System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory() + @"regasm.exe";
            // Get the location of our DLL
            string componentPath = typeof(EAN13).Assembly.Location;
            // Execute regasm
            System.Diagnostics.Process.Start(regasmPath, "-tlb -codebase \"" + componentPath + "\"");
        }

        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public override void Install(IDictionary stateSaver)
        {
            base.Install(stateSaver);
        }

        public override void Uninstall(IDictionary savedState)
        {
            base.Uninstall(savedState);

            // Get the location of regasm
            string regasmPath = System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory() + @"regasm.exe";
            // Get the location of our DLL
            string componentPath = typeof(EAN13).Assembly.Location;
            // Execute regasm
            System.Diagnostics.Process.Start(regasmPath, "/u \"" + componentPath + "\"");
        }
    }
}
