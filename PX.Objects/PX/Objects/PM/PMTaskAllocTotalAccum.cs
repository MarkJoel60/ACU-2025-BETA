// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMTaskAllocTotalAccum
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

[PXHidden]
[TaskAllocTotalAccum]
[ExcludeFromCodeCoverage]
[Serializable]
public class PMTaskAllocTotalAccum : PMTaskAllocTotal
{
  public new abstract class projectID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  PMTaskAllocTotalAccum.projectID>
  {
  }

  public new abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTaskAllocTotalAccum.taskID>
  {
  }

  public new abstract class accountGroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMTaskAllocTotalAccum.accountGroupID>
  {
  }

  public new abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMTaskAllocTotalAccum.inventoryID>
  {
  }

  public new abstract class costCodeID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMTaskAllocTotalAccum.costCodeID>
  {
  }
}
