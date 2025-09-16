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
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
[assembly: InternalsVisibleTo("PX.Objects.Tests")]
[assembly: InternalsVisibleTo("PX.Objects.DBTests")]
[assembly: InternalsVisibleTo("PX.OutlookAddIn")]
[assembly: InternalsVisibleTo("PX.Scenarios")]
[assembly: InternalsVisibleTo("TestBankFeeds")]
[assembly: FilterHeaderDescription]
[assembly: AssemblyTrademark("")]
[assembly: ComVisible(false)]
[assembly: Guid("e06df567-c774-419f-8cfe-326038fc9301")]
[assembly: AssemblyCompany("PX.Objects")]
[assembly: AssemblyConfiguration("Release")]
[assembly: AssemblyCopyright("Copyright © 2005-2025 Acumatica, Inc. All rights reserved.")]
[assembly: AssemblyFileVersion("25.193.0171")]
[assembly: AssemblyInformationalVersion("25.193.0171")]
[assembly: AssemblyProduct("PX.Objects")]
[assembly: AssemblyTitle("PX.Objects")]
[assembly: AssemblyVersion("1.0.0.0")]
