using PX.Data;
using PX.Objects.EP;
using PX.SM;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

[assembly: Extension]
[assembly: AllowPartiallyTrustedCallers]
[assembly: SecurityRules(SecurityRuleSet.Level1)]
[assembly: PXHidden(Target = typeof (WikiMaintenance))]
[assembly: PXHidden(Target = typeof (Access))]
[assembly: PXHidden(Target = typeof (PreferencesGeneralMaint))]
[assembly: PXHidden(Target = typeof (PreferencesSecurityMaint))]
[assembly: PXHidden(Target = typeof (PreferencesEmail))]
[assembly: InternalsVisibleTo("PX.Objects.Tests")]
[assembly: InternalsVisibleTo("PX.Objects.DBTests")]
[assembly: FilterHeaderDescription]
[assembly: AssemblyTrademark("")]
[assembly: ComVisible(false)]
[assembly: Guid("c39436c6-f66c-4131-8d28-f5155fc6c920")]
[assembly: AssemblyCompany("PX.Objects.FS")]
[assembly: AssemblyConfiguration("Release")]
[assembly: AssemblyCopyright("Copyright © 2005-2025 Acumatica, Inc. All rights reserved.")]
[assembly: AssemblyFileVersion("25.193.0171")]
[assembly: AssemblyInformationalVersion("25.193.0171")]
[assembly: AssemblyProduct("PX.Objects.FS")]
[assembly: AssemblyTitle("PX.Objects.FS")]
[assembly: AssemblyVersion("1.0.0.0")]
