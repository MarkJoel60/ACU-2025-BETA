// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_LogActions
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.FS;

public abstract class ListField_LogActions : IBqlField, IBqlOperand
{
  public class ListAtrribute : PXStringListAttribute
  {
    public ListAtrribute()
      : base(new ID.LogActions().ID_LIST, new ID.LogActions().TX_LIST)
    {
    }
  }

  public class StartListAttribute : PXStringListAttribute
  {
    public StartListAttribute()
      : base(new ID.StartLogActions().ID_LIST, new ID.StartLogActions().TX_LIST)
    {
    }
  }

  public class PCRListAttribute : PXStringListAttribute
  {
    public PCRListAttribute()
      : base(new ID.PCRLogActions().ID_LIST, new ID.PCRLogActions().TX_LIST)
    {
    }
  }

  public class Start : BqlType<IBqlString, string>.Constant<
  #nullable disable
  ListField_LogActions.Start>
  {
    public Start()
      : base("ST")
    {
    }
  }

  public class Complete : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_LogActions.Complete>
  {
    public Complete()
      : base("CP")
    {
    }
  }

  public class Resume : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_LogActions.Resume>
  {
    public Resume()
      : base("RE")
    {
    }
  }

  public class Pause : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_LogActions.Pause>
  {
    public Pause()
      : base("PA")
    {
    }
  }
}
