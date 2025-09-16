// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.EMailSyncAccountPreferencesExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.EP;
using PX.SM;

#nullable enable
namespace PX.Objects.CS;

public class EMailSyncAccountPreferencesExt : PXCacheExtension<
#nullable disable
EMailSyncAccountPreferences>
{
  [PXDBInt(IsKey = true)]
  [PXUIField]
  [PXEmployeeSingleSelector]
  public virtual int? EmployeeID { get; set; }

  [PXUIField]
  [PXDBScalar(typeof (Search<EPEmployee.acctName, Where<EPEmployee.bAccountID, Equal<EMailSyncAccountPreferencesExt.employeeID>>>))]
  public virtual string EmployeeCD { get; set; }

  public abstract class employeeID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EMailSyncAccountPreferencesExt.employeeID>
  {
  }
}
