// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.DAC.BranchItem
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;
using System.Diagnostics;

#nullable enable
namespace PX.Objects.GL.DAC;

[PXHidden]
[DebuggerDisplay("BranchItem: ID = {BAccountID,nq}, AcctCD = {AcctCD}, AcctName = {AcctName}, ParentID={ParentBAccountID, nq}")]
[PXBreakInheritance]
[Serializable]
public class BranchItem : PX.Objects.CR.BAccount
{
  [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDefault]
  [PXDimension("COMPANY")]
  [PXUIField]
  public override 
  #nullable disable
  string AcctCD
  {
    get => this._AcctCD;
    set => this._AcctCD = value;
  }

  [PXInt]
  public virtual int? PrimaryGroupID { get; set; }

  [PXString]
  public new string Type { get; set; }

  [PXBool]
  public virtual bool? CanSelect { get; set; } = new bool?(true);

  public new class PK : PrimaryKeyOf<BranchItem>.By<BranchItem.bAccountID>
  {
    public static BranchItem Find(PXGraph graph, int? bAccountID, PKFindOptions options = 0)
    {
      return (BranchItem) PrimaryKeyOf<BranchItem>.By<BranchItem.bAccountID>.FindBy(graph, (object) bAccountID, options);
    }
  }

  public new class UK : PrimaryKeyOf<BranchItem>.By<BranchItem.acctCD>
  {
    public static BranchItem Find(PXGraph graph, string acctCD, PKFindOptions options = 0)
    {
      return (BranchItem) PrimaryKeyOf<BranchItem>.By<BranchItem.acctCD>.FindBy(graph, (object) acctCD, options);
    }
  }

  public new static class FK
  {
    public class ParentBAccount : 
      PrimaryKeyOf<PX.Objects.CR.BAccount>.By<PX.Objects.CR.BAccount.bAccountID>.ForeignKeyOf<BranchItem>.By<BranchItem.parentBAccountID>
    {
    }
  }

  public new abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BranchItem.bAccountID>
  {
  }

  public new abstract class acctCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BranchItem.acctCD>
  {
  }

  public new abstract class acctName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BranchItem.acctName>
  {
  }

  public new abstract class parentBAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PX.Objects.CR.BAccount.parentBAccountID>
  {
  }

  public abstract class primaryGroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BranchItem.primaryGroupID>
  {
  }

  public new abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  BranchItem.noteID>
  {
  }

  public new abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BranchItem.type>
  {
    public const string Group = "group";
    public const string Organization = "organization";
    public const string Branch = "branch";

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new string[3]
        {
          "group",
          "organization",
          "branch"
        }, new string[3]
        {
          "Organization Group",
          "Organization",
          "Branch"
        })
      {
      }
    }
  }

  public abstract class canSelect : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BranchItem.canSelect>
  {
  }
}
