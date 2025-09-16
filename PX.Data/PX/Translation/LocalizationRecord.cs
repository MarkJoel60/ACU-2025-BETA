// Decompiled with JetBrains decompiler
// Type: PX.Translation.LocalizationRecord
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Translation;

/// <exclude />
[PXVirtual]
[Serializable]
public class LocalizationRecord : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXUIField(Visible = false, Enabled = false)]
  [PXDBString(32 /*0x20*/, IsKey = true)]
  public virtual 
  #nullable disable
  string Id { get; set; }

  [PXDBString]
  [PXUIField(DisplayName = "Source Value", Enabled = false)]
  public virtual string NeutralValue { get; set; }

  [PXUIField(DisplayName = "Do Not Translate")]
  [PXBool]
  public virtual bool? IsNotLocalized { get; set; }

  [PXUIField(DisplayName = "Localized Value", Enabled = false, Visible = false)]
  [PXString]
  public virtual string LocalizedValue { get; set; }

  public Dictionary<string, object> Values { get; set; }

  public Dictionary<string, System.DateTime?> CreatedValues { get; set; }

  public Dictionary<string, System.DateTime?> LastModifiedValues { get; set; }

  public LocalizationRecord()
  {
    this.Values = new Dictionary<string, object>();
    this.CreatedValues = new Dictionary<string, System.DateTime?>();
    this.LastModifiedValues = new Dictionary<string, System.DateTime?>();
  }

  /// <exclude />
  public abstract class id : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LocalizationRecord.id>
  {
  }

  /// <exclude />
  public abstract class neutralValue : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocalizationRecord.neutralValue>
  {
  }

  /// <exclude />
  public abstract class isNotLocalized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocalizationRecord.isNotLocalized>
  {
  }

  /// <exclude />
  public abstract class localizedValue : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocalizationRecord.localizedValue>
  {
  }
}
