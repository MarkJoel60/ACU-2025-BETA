// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.DAC.InventorySearchIndex
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.IN.DAC;

/// <exclude />
[PXHidden]
public class InventorySearchIndex : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true)]
  public virtual int? InventoryID { get; set; }

  [PXDBGuid(false)]
  public virtual Guid? IndexID { get; set; }

  [PXDBText(IsUnicode = true)]
  public virtual 
  #nullable disable
  string ContentIDDesc { get; set; }

  [PXDBText(IsUnicode = true)]
  public virtual string ContentAlternateID { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXInt]
  public virtual int? Rank { get; set; }

  public class PK : PrimaryKeyOf<InventorySearchIndex>.By<InventorySearchIndex.inventoryID>
  {
    public static InventorySearchIndex Find(PXGraph graph, int? inventoryID, PKFindOptions options = 0)
    {
      return (InventorySearchIndex) PrimaryKeyOf<InventorySearchIndex>.By<InventorySearchIndex.inventoryID>.FindBy(graph, (object) inventoryID, options);
    }
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InventorySearchIndex.inventoryID>
  {
  }

  public abstract class indexID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  InventorySearchIndex.indexID>
  {
  }

  public abstract class contentIDDesc : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventorySearchIndex.contentIDDesc>
  {
  }

  public abstract class contentAlternateID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventorySearchIndex.contentAlternateID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  InventorySearchIndex.Tstamp>
  {
  }

  public abstract class rank : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InventorySearchIndex.rank>
  {
  }
}
