// Decompiled with JetBrains decompiler
// Type: PX.SM.UPErrors
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Text;

#nullable enable
namespace PX.SM;

[PXCacheName("Update Error")]
[Serializable]
public class UPErrors : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _UpdateID;
  protected int? _ErrorID;
  protected 
  #nullable disable
  string _Message;
  protected string _Stack;
  protected string _Script;
  protected bool? _Skip;
  protected string _Details;

  [PXUIField(DisplayName = "Maintenance ID", Visibility = PXUIVisibility.Invisible)]
  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (UPHistory.updateID))]
  public virtual int? UpdateID
  {
    get => this._UpdateID;
    set => this._UpdateID = value;
  }

  [PXUIField(DisplayName = "Error ID", Enabled = false)]
  [PXDefault]
  [PXDBIdentity(IsKey = true)]
  public virtual int? ErrorID
  {
    get => this._ErrorID;
    set => this._ErrorID = value;
  }

  [PXUIField(DisplayName = "Error", Enabled = false)]
  [PXDBString]
  public virtual string Message
  {
    get => this._Message;
    set => this._Message = value;
  }

  [PXDBString]
  [PXUIField(DisplayName = "Stack")]
  public virtual string Stack
  {
    get => this._Stack;
    set => this._Stack = value;
  }

  [PXDBString]
  public virtual string Script
  {
    get => this._Script;
    set => this._Script = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Skipped", Enabled = true)]
  public virtual bool? Skip
  {
    get => this._Skip;
    set => this._Skip = value;
  }

  [PXUIField(DisplayName = "Details", Enabled = false)]
  [PXString]
  public virtual string Details
  {
    [PXDependsOnFields(new System.Type[] {typeof (UPErrors.message), typeof (UPErrors.stack), typeof (UPErrors.script)})] get
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (!string.IsNullOrEmpty(this.Message))
        stringBuilder.AppendLine(this.Message);
      if (!string.IsNullOrEmpty(this.Stack))
      {
        stringBuilder.AppendLine("---------------");
        stringBuilder.AppendLine(this.Stack);
      }
      if (!string.IsNullOrEmpty(this.Script))
      {
        stringBuilder.AppendLine("---------------");
        stringBuilder.AppendLine(this.Script);
      }
      return stringBuilder.ToString();
    }
  }

  public abstract class updateID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  UPErrors.updateID>
  {
  }

  public abstract class errorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  UPErrors.errorID>
  {
  }

  public abstract class message : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UPErrors.message>
  {
  }

  public abstract class stack : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UPErrors.stack>
  {
  }

  public abstract class script : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UPErrors.script>
  {
  }

  public abstract class skip : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  UPErrors.skip>
  {
  }

  public abstract class details : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UPErrors.details>
  {
  }
}
