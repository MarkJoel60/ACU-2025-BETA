// Decompiled with JetBrains decompiler
// Type: PX.SM.CopyCompanySettings
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[Serializable]
public class CopyCompanySettings : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _CompanyID;

  [PXInt(IsKey = false)]
  [PXUIField(DisplayName = "Destination Tenant ID", Visibility = PXUIVisibility.SelectorVisible)]
  [ParentCompanySelector(ExcludeHiddenCopmanies = true)]
  public virtual int? CompanyID
  {
    get => this._CompanyID;
    set => this._CompanyID = value;
  }

  public abstract class companyID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  CopyCompanySettings.companyID>
  {
  }
}
