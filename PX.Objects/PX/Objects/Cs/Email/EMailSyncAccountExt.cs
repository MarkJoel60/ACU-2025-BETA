// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.Email.EMailSyncAccountExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.CS.Email;

[Serializable]
public class EMailSyncAccountExt : PXCacheExtension<
#nullable disable
EMailSyncAccount>
{
  [PXDBInt(IsKey = true)]
  [PXUIField]
  [PXSelector(typeof (EPEmployee.bAccountID), SubstituteKey = typeof (EPEmployee.acctCD), DescriptionField = typeof (EPEmployee.acctName))]
  public virtual int? EmployeeID { get; set; }

  [PXString(60, IsUnicode = true)]
  [PXUIField]
  [PXDBScalar(typeof (Search<EPEmployee.acctName, Where<EPEmployee.bAccountID, Equal<EMailSyncAccountExt.employeeID>>>))]
  public virtual string EmployeeCD { get; set; }

  [PXInt]
  [PXUIField]
  [PXDBScalar(typeof (Search<EPEmployee.defContactID, Where<EPEmployee.bAccountID, Equal<EMailSyncAccountExt.employeeID>>>))]
  public virtual int? OwnerID { get; set; }

  /// <summary>
  /// <inheritdoc cref="P:PX.Objects.EP.EPEmployee.VStatus" />
  /// </summary>
  [PXString(1, IsFixed = true)]
  [PXUIField]
  [PXDBScalar(typeof (Search<EPEmployee.vStatus, Where<EPEmployee.bAccountID, Equal<EMailSyncAccountExt.employeeID>>>))]
  [VendorStatus.List]
  public virtual string EmployeeStatus { get; set; }

  public abstract class employeeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EMailSyncAccountExt.employeeID>
  {
  }
}
