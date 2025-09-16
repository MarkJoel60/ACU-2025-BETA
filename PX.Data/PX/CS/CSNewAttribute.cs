// Decompiled with JetBrains decompiler
// Type: PX.CS.CSNewAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Collections;
using System.Linq;

#nullable enable
namespace PX.CS;

public class CSNewAttribute : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _AttributeID;
  protected string _OldAttributeID;
  protected int? _column;
  protected int? _row;

  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXDefault]
  [PXUIField(DisplayName = "Attribute ID", Visibility = PXUIVisibility.SelectorVisible)]
  [CSNewAttribute.attributeID.AttributeIDAsciiSelector]
  public virtual string AttributeID
  {
    get => this._AttributeID;
    set => this._AttributeID = value;
  }

  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXDefault]
  [PXUIField(DisplayName = "Attribute ID", Visibility = PXUIVisibility.Invisible)]
  public virtual string OldAttributeID
  {
    get => this._OldAttributeID;
    set => this._OldAttributeID = value;
  }

  [PXDBInt(MinValue = 1, MaxValue = 12)]
  [PXDefault(1)]
  [PXUIField(DisplayName = "Column")]
  public virtual int? Column
  {
    get => this._column;
    set => this._column = value;
  }

  [PXDBInt(MinValue = 1, MaxValue = 1000)]
  [PXDefault(1)]
  [PXUIField(DisplayName = "Row")]
  public virtual int? Row
  {
    get => this._row;
    set => this._row = value;
  }

  public abstract class attributeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CSNewAttribute.attributeID>
  {
    public class AttributeIDAsciiSelectorAttribute : PXCustomSelectorAttribute
    {
      public AttributeIDAsciiSelectorAttribute()
        : base(typeof (CSAttribute.attributeID))
      {
      }

      public IEnumerable GetRecords()
      {
        foreach (PXResult<CSAttribute> pxResult in PXSelectBase<CSAttribute, PXSelect<CSAttribute>.Config>.Select(this._Graph))
        {
          CSAttribute record = (CSAttribute) pxResult;
          if (record.AttributeID.All<char>((Func<char, bool>) (_ => _ <= '\u007F')))
            yield return (object) record;
        }
      }
    }
  }

  public abstract class oldAttributeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CSNewAttribute.oldAttributeID>
  {
  }

  public abstract class column : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CSNewAttribute.column>
  {
  }

  public abstract class row : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CSNewAttribute.row>
  {
  }
}
