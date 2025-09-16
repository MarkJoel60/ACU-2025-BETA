// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.UpdateSummary
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CR;

[PXHidden]
[Serializable]
public class UpdateSummary : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [UpdateWizardSummary(IsUnicode = true)]
  [PXUIField(DisplayName = " ", Enabled = false)]
  public virtual 
  #nullable disable
  string Summary { get; set; }

  public abstract class summary : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UpdateSummary.summary>
  {
  }
}
