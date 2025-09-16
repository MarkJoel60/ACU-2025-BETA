// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSBLOCAddress
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CR;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXBreakInheritance]
[PXProjection(typeof (Select<FSAddress, Where<FSAddress.entityType, Equal<ListField.ACEntityType.BranchLocation>>>))]
[Serializable]
public class FSBLOCAddress : FSAddress
{
  [PXDBString(4, IsFixed = true)]
  [PXDefault("BLOC")]
  [PXUIField(DisplayName = "Entity Type", Visible = false, Enabled = false)]
  public override 
  #nullable disable
  string EntityType { get; set; }

  public new class PK : PrimaryKeyOf<FSBLOCAddress>.By<FSBLOCAddress.addressID>
  {
    public static FSBLOCAddress Find(PXGraph graph, int? addressID, PKFindOptions options = 0)
    {
      return (FSBLOCAddress) PrimaryKeyOf<FSBLOCAddress>.By<FSBLOCAddress.addressID>.FindBy(graph, (object) addressID, options);
    }
  }

  public new static class FK
  {
    public class BusinessAccount : 
      PrimaryKeyOf<BAccount>.By<BAccount.bAccountID>.ForeignKeyOf<FSBLOCAddress>.By<FSBLOCAddress.bAccountID>
    {
    }

    public class Country : 
      PrimaryKeyOf<PX.Objects.CS.Country>.By<PX.Objects.CS.Country.countryID>.ForeignKeyOf<FSBLOCAddress>.By<FSBLOCAddress.countryID>
    {
    }

    public class State : 
      PrimaryKeyOf<PX.Objects.CS.State>.By<PX.Objects.CS.State.countryID, PX.Objects.CS.State.stateID>.ForeignKeyOf<FSBLOCAddress>.By<FSBLOCAddress.countryID, FSBLOCAddress.state>
    {
    }
  }

  public new abstract class addressID : IBqlField, IBqlOperand
  {
  }

  public new abstract class entityType : ListField.ACEntityType
  {
  }

  public new abstract class revisionID : IBqlField, IBqlOperand
  {
  }

  public new abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSBLOCAddress.bAccountID>
  {
  }

  public new abstract class countryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSBLOCAddress.countryID>
  {
  }

  public new abstract class state : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSBLOCAddress.state>
  {
  }
}
