// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.Standalone.GLBudgetLine2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.GL.Standalone;

[PXHidden]
[Serializable]
public class GLBudgetLine2 : GLBudgetLine
{
  public new abstract class branchID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  GLBudgetLine2.branchID>
  {
  }

  public new abstract class ledgerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLBudgetLine2.ledgerID>
  {
  }

  public new abstract class finYear : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLBudgetLine2.finYear>
  {
  }

  public new abstract class groupID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GLBudgetLine2.groupID>
  {
  }

  public new abstract class parentGroupID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    GLBudgetLine2.parentGroupID>
  {
  }
}
